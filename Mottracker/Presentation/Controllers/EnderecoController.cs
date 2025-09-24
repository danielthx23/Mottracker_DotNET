using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Endereco;
using Mottracker.Docs.Samples;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/v1/endereco")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoApplicationService _applicationService;

        public EnderecoController(IEnderecoApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(
           Summary = "Lista endereços com paginação",
           Description = "Retorna uma lista paginada de endereços cadastrados no sistema. " +
                        "Este endpoint suporta paginação para otimizar a performance com grandes volumes de dados. " +
                        "Os endereços são retornados com informações completas e links HATEOAS para navegação."
        )]
        [SwaggerResponse(statusCode: 200, description: "Lista de endereços retornada com sucesso", type: typeof(IEnumerable<EnderecoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum endereço encontrado")]
        [SwaggerResponse(statusCode: 400, description: "Parâmetros de paginação inválidos")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        [SwaggerResponseExample(statusCode: 200, typeof(EnderecoResponseListSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public IActionResult Get(
            [FromQuery, SwaggerParameter("Número de registros a pular (padrão: 0)", Required = false)] int Deslocamento = 0, 
            [FromQuery, SwaggerParameter("Número de registros a retornar (padrão: 3, máximo: 100)", Required = false)] int RegistrosRetornado = 3)
        {
            var result = _applicationService.ObterTodosEnderecos();

            if (result is not null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(e => new
                    {
                        e.IdEndereco,
                        e.CepEndereco,
                        e.LogradouroEndereco,
                        e.NumeroEndereco,
                        e.ComplementoEndereco,
                        e.BairroEndereco,
                        e.CidadeEndereco,
                        e.EstadoEndereco,
                        e.PatioEndereco,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "Endereco", new { id = e.IdEndereco }, Request.Scheme),
                            put = Url.Action(nameof(Put), "Endereco", new { id = e.IdEndereco }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "Endereco", new { id = e.IdEndereco }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(Get), "Endereco", null, Request.Scheme),
                        create = Url.Action(nameof(Post), "Endereco", null, Request.Scheme),
                    },
                    pagina = new
                    {
                        Deslocamento,
                        RegistrosRetornado,
                        TotalRegistros = result.Count()
                    }
                };

                return Ok(hateaos);
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obtém endereço por ID",
            Description = "Retorna os dados completos de um endereço específico baseado no ID fornecido. " +
                        "Inclui informações detalhadas do endereço e links HATEOAS para operações relacionadas."
        )]
        [SwaggerResponse(statusCode: 200, description: "Endereço encontrado com sucesso", type: typeof(EnderecoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "Endereço não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetById(
            [FromRoute, SwaggerParameter("ID único do endereço", Required = true)] int id)
        {
            var result = _applicationService.ObterEnderecoPorId(id);

            if (result is not null)
            {
                var hateaos = new
                {
                    data = result,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Endereco", new { id }),
                        get = Url.Action(nameof(Get), "Endereco", null),
                        put = Url.Action(nameof(Put), "Endereco", new { id }),
                        delete = Url.Action(nameof(Delete), "Endereco", new { id }),
                    }
                };

                return Ok(hateaos);
            }

            return NotFound();
        }

        [HttpGet("cep/{cep}")]
        [SwaggerOperation(
            Summary = "Obtém endereço por CEP",
            Description = "Retorna os dados completos de um endereço específico baseado no CEP fornecido. " +
                        "Útil para busca rápida por CEP."
        )]
        [SwaggerResponse(statusCode: 200, description: "Endereço encontrado com sucesso", type: typeof(EnderecoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "CEP é obrigatório e deve ser válido")]
        [SwaggerResponse(statusCode: 404, description: "Endereço não encontrado para o CEP fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByCep(
            [FromRoute, SwaggerParameter("CEP do endereço para busca", Required = true)] string cep)
        {
            var result = _applicationService.ObterEnderecoPorCep(cep);

            if (result is not null)
            {
                var hateaos = new
                {
                    data = result,
                    links = new
                    {
                        self = Url.Action(nameof(GetByCep), "Endereco", new { cep }, Request.Scheme),
                        get = Url.Action(nameof(Get), "Endereco", null, Request.Scheme),
                        put = Url.Action(nameof(Put), "Endereco", new { id = result.IdEndereco }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Endereco", new { id = result.IdEndereco }, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NotFound();
        }

        [HttpGet("estado/{estado}")]
        [SwaggerOperation(
            Summary = "Obtém endereços por estado",
            Description = "Retorna uma lista paginada de endereços filtrados por estado específico. " +
                        "Útil para encontrar todos os endereços em um determinado estado."
        )]
        [SwaggerResponse(statusCode: 200, description: "Endereços encontrados com sucesso", type: typeof(IEnumerable<EnderecoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum endereço encontrado para o estado especificado")]
        [SwaggerResponse(statusCode: 400, description: "Estado é obrigatório e não pode estar vazio")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByEstado(
            [FromRoute, SwaggerParameter("Estado para busca", Required = true)] string estado)
        {
            var result = _applicationService.ObterEnderecosPorEstado(estado);

            if (result != null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(e => new
                    {
                        e.IdEndereco,
                        e.CepEndereco,
                        e.LogradouroEndereco,
                        e.NumeroEndereco,
                        e.ComplementoEndereco,
                        e.BairroEndereco,
                        e.CidadeEndereco,
                        e.EstadoEndereco,
                        e.PatioEndereco,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "Endereco", new { id = e.IdEndereco }, Request.Scheme),
                            put = Url.Action(nameof(Put), "Endereco", new { id = e.IdEndereco }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "Endereco", new { id = e.IdEndereco }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(GetByEstado), "Endereco", new { estado }, Request.Scheme),
                        get = Url.Action(nameof(Get), "Endereco", null, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NoContent();
        }

        [HttpGet("cidade/{cidade}")]
        [SwaggerOperation(
            Summary = "Obtém endereços por cidade",
            Description = "Retorna uma lista paginada de endereços filtrados por cidade específica. " +
                        "Útil para encontrar todos os endereços em uma determinada cidade."
        )]
        [SwaggerResponse(statusCode: 200, description: "Endereços encontrados com sucesso", type: typeof(IEnumerable<EnderecoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum endereço encontrado para a cidade especificada")]
        [SwaggerResponse(statusCode: 400, description: "Cidade é obrigatória e não pode estar vazia")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByCidade(
            [FromRoute, SwaggerParameter("Cidade para busca", Required = true)] string cidade)
        {
            var result = _applicationService.ObterEnderecosPorCidade(cidade);

            if (result != null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(e => new
                    {
                        e.IdEndereco,
                        e.CepEndereco,
                        e.LogradouroEndereco,
                        e.NumeroEndereco,
                        e.ComplementoEndereco,
                        e.BairroEndereco,
                        e.CidadeEndereco,
                        e.EstadoEndereco,
                        e.PatioEndereco,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "Endereco", new { id = e.IdEndereco }, Request.Scheme),
                            put = Url.Action(nameof(Put), "Endereco", new { id = e.IdEndereco }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "Endereco", new { id = e.IdEndereco }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(GetByCidade), "Endereco", new { cidade }, Request.Scheme),
                        get = Url.Action(nameof(Get), "Endereco", null, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NoContent();
        }

        [HttpGet("cidade-estado")]
        [SwaggerOperation(
            Summary = "Obtém endereços por cidade e estado",
            Description = "Retorna uma lista paginada de endereços filtrados por cidade e estado específicos. " +
                        "Útil para encontrar todos os endereços em uma determinada cidade e estado."
        )]
        [SwaggerResponse(statusCode: 200, description: "Endereços encontrados com sucesso", type: typeof(IEnumerable<EnderecoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum endereço encontrado para a cidade e estado especificados")]
        [SwaggerResponse(statusCode: 400, description: "Cidade e estado são obrigatórios")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByCidadeEstado(
            [FromQuery, SwaggerParameter("Cidade para busca", Required = true)] string cidade, 
            [FromQuery, SwaggerParameter("Estado para busca", Required = true)] string estado)
        {
            var result = _applicationService.ObterEnderecosPorCidadeEEstado(cidade, estado);

            if (result != null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(e => new
                    {
                        e.IdEndereco,
                        e.CepEndereco,
                        e.LogradouroEndereco,
                        e.NumeroEndereco,
                        e.ComplementoEndereco,
                        e.BairroEndereco,
                        e.CidadeEndereco,
                        e.EstadoEndereco,
                        e.PatioEndereco,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "Endereco", new { id = e.IdEndereco }, Request.Scheme),
                            put = Url.Action(nameof(Put), "Endereco", new { id = e.IdEndereco }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "Endereco", new { id = e.IdEndereco }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(GetByCidadeEstado), "Endereco", new { cidade, estado }, Request.Scheme),
                        get = Url.Action(nameof(Get), "Endereco", null, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NoContent();
        }

        [HttpGet("bairro/{bairro}")]
        [SwaggerOperation(
            Summary = "Obtém endereços por bairro",
            Description = "Retorna uma lista paginada de endereços filtrados por bairro específico. " +
                        "Útil para encontrar todos os endereços em um determinado bairro."
        )]
        [SwaggerResponse(statusCode: 200, description: "Endereços encontrados com sucesso", type: typeof(IEnumerable<EnderecoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum endereço encontrado para o bairro especificado")]
        [SwaggerResponse(statusCode: 400, description: "Bairro é obrigatório e não pode estar vazio")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByBairro(
            [FromRoute, SwaggerParameter("Bairro para busca", Required = true)] string bairro)
        {
            var result = _applicationService.ObterEnderecosPorBairro(bairro);

            if (result != null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(e => new
                    {
                        e.IdEndereco,
                        e.CepEndereco,
                        e.LogradouroEndereco,
                        e.NumeroEndereco,
                        e.ComplementoEndereco,
                        e.BairroEndereco,
                        e.CidadeEndereco,
                        e.EstadoEndereco,
                        e.PatioEndereco,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "Endereco", new { id = e.IdEndereco }, Request.Scheme),
                            put = Url.Action(nameof(Put), "Endereco", new { id = e.IdEndereco }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "Endereco", new { id = e.IdEndereco }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(GetByBairro), "Endereco", new { bairro }, Request.Scheme),
                        get = Url.Action(nameof(Get), "Endereco", null, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NoContent();
        }

        [HttpGet("logradouro")]
        [SwaggerOperation(
            Summary = "Obtém endereços por logradouro",
            Description = "Retorna uma lista paginada de endereços filtrados por logradouro contendo o texto especificado. " +
                        "Útil para busca parcial por logradouro."
        )]
        [SwaggerResponse(statusCode: 200, description: "Endereços encontrados com sucesso", type: typeof(IEnumerable<EnderecoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum endereço encontrado para o logradouro especificado")]
        [SwaggerResponse(statusCode: 400, description: "Logradouro é obrigatório e não pode estar vazio")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByLogradouroContendo(
            [FromQuery, SwaggerParameter("Logradouro para busca", Required = true)] string logradouro)
        {
            var result = _applicationService.ObterEnderecosPorLogradouroContendo(logradouro);

            if (result != null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(e => new
                    {
                        e.IdEndereco,
                        e.CepEndereco,
                        e.LogradouroEndereco,
                        e.NumeroEndereco,
                        e.ComplementoEndereco,
                        e.BairroEndereco,
                        e.CidadeEndereco,
                        e.EstadoEndereco,
                        e.PatioEndereco,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "Endereco", new { id = e.IdEndereco }, Request.Scheme),
                            put = Url.Action(nameof(Put), "Endereco", new { id = e.IdEndereco }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "Endereco", new { id = e.IdEndereco }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(GetByLogradouroContendo), "Endereco", new { logradouro }, Request.Scheme),
                        get = Url.Action(nameof(Get), "Endereco", null, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NoContent();
        }

        [HttpGet("patio/{patioId}")]
        [SwaggerOperation(
            Summary = "Obtém endereço por pátio",
            Description = "Retorna o endereço vinculado a um pátio específico baseado no ID do pátio fornecido. " +
                        "Útil para encontrar o endereço de um pátio específico."
        )]
        [SwaggerResponse(statusCode: 200, description: "Endereço encontrado com sucesso", type: typeof(EnderecoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID do pátio é obrigatório")]
        [SwaggerResponse(statusCode: 404, description: "Endereço não encontrado para o pátio especificado")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByPatioId(
            [FromRoute, SwaggerParameter("ID único do pátio", Required = true)] long patioId)
        {
            var result = _applicationService.ObterEnderecoPorIdPatio(patioId);

            if (result is not null)
            {
                var hateaos = new
                {
                    data = result,
                    links = new
                    {
                        self = Url.Action(nameof(GetByPatioId), "Endereco", new { patioId }, Request.Scheme),
                        get = Url.Action(nameof(Get), "Endereco", null, Request.Scheme),
                        put = Url.Action(nameof(Put), "Endereco", new { id = result.IdEndereco }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Endereco", new { id = result.IdEndereco }, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NotFound();
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria novo endereço",
            Description = "Cria um novo endereço no sistema com os dados fornecidos. " +
                        "Valida se todos os campos obrigatórios estão preenchidos e se não há duplicatas. " +
                        "Retorna os dados do endereço criado incluindo o ID gerado."
        )]
        [SwaggerRequestExample(typeof(EnderecoRequestDto), typeof(EnderecoRequestDtoSample))]
        [SwaggerResponse(statusCode: 201, description: "Endereço criado com sucesso", type: typeof(EnderecoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "Dados inválidos - campos obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível criar o endereço - dados inválidos ou duplicados")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult Post(
            [FromBody, SwaggerParameter("Dados do endereço a ser criado", Required = true)] EnderecoRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosEndereco(entity);

                if (result is not null)
                    return CreatedAtAction(nameof(GetById), new { id = result.IdEndereco }, result);

                return BadRequest("Não foi possível salvar os dados.");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    Status = HttpStatusCode.BadRequest
                });
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualiza endereço existente",
            Description = "Atualiza os dados de um endereço existente baseado no ID fornecido. " +
                        "Valida se o endereço existe e se os novos dados são válidos. " +
                        "Retorna os dados atualizados do endereço."
        )]
        [SwaggerRequestExample(typeof(EnderecoRequestDto), typeof(EnderecoRequestDtoSample))]
        [SwaggerResponse(statusCode: 200, description: "Endereço atualizado com sucesso", type: typeof(EnderecoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido ou dados obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 404, description: "Endereço não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível atualizar o endereço - dados inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult Put(
            [FromRoute, SwaggerParameter("ID único do endereço a ser atualizado", Required = true)] int id, 
            [FromBody, SwaggerParameter("Novos dados do endereço", Required = true)] EnderecoRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosEndereco(id, entity);

                if (result is not null)
                    return Ok(result);

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    Status = HttpStatusCode.BadRequest
                });
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remove endereço",
            Description = "Remove permanentemente um endereço do sistema baseado no ID fornecido. " +
                        "Esta operação é irreversível e remove todos os dados associados ao endereço."
        )]
        [SwaggerResponse(statusCode: 204, description: "Endereço removido com sucesso")]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "Endereço não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível remover o endereço")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult Delete(
            [FromRoute, SwaggerParameter("ID único do endereço a ser removido", Required = true)] int id)
        {
            var result = _applicationService.DeletarDadosEndereco(id);

            if (result is not null)
                return NoContent();

            return NotFound();
        }
    }
}