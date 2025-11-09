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
        private readonly IEnderecoUseCase _useCase;

        public EnderecoController(IEnderecoUseCase useCase)
        {
            _useCase = useCase;
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
        public async Task<IActionResult> Get(
            [FromQuery, SwaggerParameter("Número de registros a pular (padrão: 0)", Required = false)] int Deslocamento = 0, 
            [FromQuery, SwaggerParameter("Número de registros a retornar (padrão: 3, máximo: 100)", Required = false)] int RegistrosRetornado = 3)
        {
            var result = await _useCase.ObterTodosEnderecosAsync(Deslocamento, RegistrosRetornado);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Data?.Select(e => new
                {
                    e.IdEndereco,
                    e.CEP,
                    e.Logradouro,
                    e.Numero,
                    e.Complemento,
                    e.Bairro,
                    e.Cidade,
                    e.Estado,
                    e.Referencia,
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
                    result.Value?.Deslocamento,
                    result.Value?.RegistrosRetornado,
                    result.Value?.TotalRegistros
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
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
        public async Task<IActionResult> GetById(
            [FromRoute, SwaggerParameter("ID único do endereço", Required = true)] int id)
        {
            var result = await _useCase.ObterEnderecoPorIdAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value,
                links = new
                {
                    self = Url.Action(nameof(GetById), "Endereco", new { id }),
                    get = Url.Action(nameof(Get), "Endereco", null),
                    put = Url.Action(nameof(Put), "Endereco", new { id }),
                    delete = Url.Action(nameof(Delete), "Endereco", new { id }),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
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
        public async Task<IActionResult> GetByCep(
            [FromRoute, SwaggerParameter("CEP do endereço para busca", Required = true)] string cep)
        {
            var result = await _useCase.ObterEnderecoPorCepAsync(cep);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Select<EnderecoResponseDto, object>(e => new
                {
                    e.IdEndereco,
                    e.CEP,
                    e.Logradouro,
                    e.Numero,
                    e.Complemento,
                    e.Bairro,
                    e.Cidade,
                    e.Estado,
                    e.Referencia,
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
                    self = Url.Action(nameof(GetByCep), "Endereco", new { cep }, Request.Scheme),
                    get = Url.Action(nameof(Get), "Endereco", null, Request.Scheme),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
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
        public async Task<IActionResult> GetByEstado(
            [FromRoute, SwaggerParameter("Estado para busca", Required = true)] string estado)
        {
            var result = await _useCase.ObterEnderecoPorEstadoAsync(estado);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Select<EnderecoResponseDto, object>(e => new
                {
                    e.IdEndereco,
                    e.CEP,
                    e.Logradouro,
                    e.Numero,
                    e.Complemento,
                    e.Bairro,
                    e.Cidade,
                    e.Estado,
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

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
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
        public async Task<IActionResult> GetByCidade(
            [FromRoute, SwaggerParameter("Cidade para busca", Required = true)] string cidade)
        {
            var result = await _useCase.ObterEnderecoPorCidadeAsync(cidade);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Select<EnderecoResponseDto, object>(e => new
                {
                    e.IdEndereco,
                    e.CEP,
                    e.Logradouro,
                    e.Numero,
                    e.Complemento,
                    e.Bairro,
                    e.Cidade,
                    e.Estado,
                    e.Referencia,
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

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
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
        public async Task<IActionResult> GetByCidadeEstado(
            [FromQuery, SwaggerParameter("Cidade para busca", Required = true)] string cidade, 
            [FromQuery, SwaggerParameter("Estado para busca", Required = true)] string estado)
        {
            var result = await _useCase.ObterEnderecoPorCidadeEstadoAsync(cidade, estado);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Select<EnderecoResponseDto, object>(e => new
                {
                    e.IdEndereco,
                    e.CEP,
                    e.Logradouro,
                    e.Numero,
                    e.Complemento,
                    e.Bairro,
                    e.Cidade,
                    e.Estado,
                    e.Referencia,
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

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
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
        public async Task<IActionResult> GetByBairro(
            [FromRoute, SwaggerParameter("Bairro para busca", Required = true)] string bairro)
        {
            var result = await _useCase.ObterEnderecoPorBairroAsync(bairro);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Select<EnderecoResponseDto, object>(e => new
                {
                    e.IdEndereco,
                    e.CEP,
                    e.Logradouro,
                    e.Numero,
                    e.Complemento,
                    e.Bairro,
                    e.Cidade,
                    e.Estado,
                    e.Referencia,
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

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
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
        public async Task<IActionResult> GetByLogradouroContendo(
            [FromQuery, SwaggerParameter("Logradouro para busca", Required = true)] string logradouro)
        {
            var result = await _useCase.ObterEnderecoPorLogradouroContendoAsync(logradouro);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Select<EnderecoResponseDto, object>(e => new
                {
                    e.IdEndereco,
                    e.CEP,
                    e.Logradouro,
                    e.Numero,
                    e.Complemento,
                    e.Bairro,
                    e.Cidade,
                    e.Estado,
                    e.Referencia,
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

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
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
        public async Task<IActionResult> GetByPatioId(
            [FromRoute, SwaggerParameter("ID único do pátio", Required = true)] int patioId)
        {
            var result = await _useCase.ObterEnderecoPorPatioIdAsync(patioId);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Select<EnderecoResponseDto, object>(e => new
                {
                    e.IdEndereco,
                    e.CEP,
                    e.Logradouro,
                    e.Numero,
                    e.Complemento,
                    e.Bairro,
                    e.Cidade,
                    e.Estado,
                    e.Referencia,
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
                    self = Url.Action(nameof(GetByPatioId), "Endereco", new { patioId }, Request.Scheme),
                    get = Url.Action(nameof(Get), "Endereco", null, Request.Scheme),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
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
        public async Task<IActionResult> Post(
            [FromBody, SwaggerParameter("Dados do endereço a ser criado", Required = true)] EnderecoRequestDto entity)
        {
            var result = await _useCase.SalvarEnderecoAsync(entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value,
                links = new
                {
                    self = Url.Action(nameof(GetById), "Endereco", new { id = result.Value.IdEndereco }, Request.Scheme),
                    get = Url.Action(nameof(Get), "Endereco", null, Request.Scheme),
                    put = Url.Action(nameof(Put), "Endereco", new { id = result.Value.IdEndereco }, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Endereco", new { id = result.Value.IdEndereco }, Request.Scheme),
                }
            };

            return StatusCode(result.StatusCode, hateaos);
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
        public async Task<IActionResult> Put(
            [FromRoute, SwaggerParameter("ID único do endereço a ser atualizado", Required = true)] int id, 
            [FromBody, SwaggerParameter("Novos dados do endereço", Required = true)] EnderecoRequestDto entity)
        {
            var result = await _useCase.EditarEnderecoAsync(id, entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value,
                links = new
                {
                    self = Url.Action(nameof(GetById), "Endereco", new { id }, Request.Scheme),
                    get = Url.Action(nameof(Get), "Endereco", null, Request.Scheme),
                    put = Url.Action(nameof(Put), "Endereco", new { id }, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Endereco", new { id }, Request.Scheme),
                }
            };

            return StatusCode(result.StatusCode, hateaos);
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
        public async Task<IActionResult> Delete(
            [FromRoute, SwaggerParameter("ID único do endereço a ser removido", Required = true)] int id)
        {
            var result = await _useCase.DeletarEnderecoAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, result.Error);
        }
    }
}