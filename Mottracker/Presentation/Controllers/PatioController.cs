using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Patio;
using Mottracker.Docs.Samples;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/v1/patio")]
    [ApiController]
    public class PatioController : ControllerBase
    {
        private readonly IPatioApplicationService _applicationService;

        public PatioController(IPatioApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(
           Summary = "Lista pátios com paginação",
           Description = "Retorna uma lista paginada de pátios cadastrados no sistema. " +
                        "Este endpoint suporta paginação para otimizar a performance com grandes volumes de dados. " +
                        "Os pátios são retornados com informações completas e links HATEOAS para navegação."
        )]
        [SwaggerResponse(statusCode: 200, description: "Lista de pátios retornada com sucesso", type: typeof(IEnumerable<PatioResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum pátio encontrado")]
        [SwaggerResponse(statusCode: 400, description: "Parâmetros de paginação inválidos")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        [SwaggerResponseExample(statusCode: 200, typeof(PatioResponseListSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public IActionResult Get(
            [FromQuery, SwaggerParameter("Número de registros a pular (padrão: 0)", Required = false)] int Deslocamento = 0, 
            [FromQuery, SwaggerParameter("Número de registros a retornar (padrão: 3, máximo: 100)", Required = false)] int RegistrosRetornado = 3)
        {
            var result = _applicationService.ObterTodosPatios();

            if (result is not null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(p => new
                    {
                        p.IdPatio,
                        p.NomePatio,
                        p.MotosTotaisPatio,
                        p.MotosDisponiveisPatio,
                        p.DataPatio,
                        p.MotosPatioAtual,
                        p.CamerasPatio,
                        p.LayoutPatio,
                        p.EnderecoPatio,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "Patio", new { id = p.IdPatio }, Request.Scheme),
                            put = Url.Action(nameof(Put), "Patio", new { id = p.IdPatio }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "Patio", new { id = p.IdPatio }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(Get), "Patio", null, Request.Scheme),
                        create = Url.Action(nameof(Post), "Patio", null, Request.Scheme),
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
            Summary = "Obtém pátio por ID",
            Description = "Retorna os dados completos de um pátio específico baseado no ID fornecido. " +
                        "Inclui informações detalhadas do pátio e links HATEOAS para operações relacionadas."
        )]
        [SwaggerResponse(statusCode: 200, description: "Pátio encontrado com sucesso", type: typeof(PatioResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "Pátio não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetById(
            [FromRoute, SwaggerParameter("ID único do pátio", Required = true)] int id)
        {
            var result = _applicationService.ObterPatioPorId(id);

            if (result is not null)
            {
                var hateaos = new
                {
                    data = result,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Patio", new { id }),
                        get = Url.Action(nameof(Get), "Patio", null),
                        put = Url.Action(nameof(Put), "Patio", new { id }),
                        delete = Url.Action(nameof(Delete), "Patio", new { id }),
                    }
                };

                return Ok(hateaos);
            }

            return NotFound();
        }

        [HttpGet("nome/{nome}")]
        [SwaggerOperation(
            Summary = "Obtém pátios por nome",
            Description = "Retorna uma lista paginada de pátios filtrados por nome específico. " +
                        "Útil para encontrar todos os pátios que contenham palavras-chave no nome."
        )]
        [SwaggerResponse(statusCode: 200, description: "Pátios encontrados com sucesso", type: typeof(IEnumerable<PatioResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum pátio encontrado para o nome especificado")]
        [SwaggerResponse(statusCode: 400, description: "Nome é obrigatório e não pode estar vazio")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByNome(
            [FromRoute, SwaggerParameter("Nome do pátio para busca", Required = true)] string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return BadRequest("Parâmetro 'nome' é obrigatório.");

            var result = _applicationService.ObterPatiosPorNomeContendo(nome);

            if (result != null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(p => new
                    {
                        p.IdPatio,
                        p.NomePatio,
                        p.MotosTotaisPatio,
                        p.MotosDisponiveisPatio,
                        p.DataPatio,
                        p.MotosPatioAtual,
                        p.CamerasPatio,
                        p.LayoutPatio,
                        p.EnderecoPatio,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "Patio", new { id = p.IdPatio }, Request.Scheme),
                            put = Url.Action(nameof(Put), "Patio", new { id = p.IdPatio }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "Patio", new { id = p.IdPatio }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(GetByNome), "Patio", new { nome }, Request.Scheme),
                        get = Url.Action(nameof(Get), "Patio", null, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NoContent();
        }

        [HttpGet("motos-disponiveis/{quantidade}")]
        [SwaggerOperation(
            Summary = "Obtém pátios com motos disponíveis",
            Description = "Retorna uma lista paginada de pátios com quantidade de motos disponíveis maior que o especificado. " +
                        "Útil para encontrar pátios com capacidade disponível."
        )]
        [SwaggerResponse(statusCode: 200, description: "Pátios encontrados com sucesso", type: typeof(IEnumerable<PatioResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum pátio encontrado com a quantidade especificada")]
        [SwaggerResponse(statusCode: 400, description: "Quantidade deve ser maior ou igual a zero")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByMotosDisponiveis(
            [FromRoute, SwaggerParameter("Quantidade mínima de motos disponíveis", Required = true)] int quantidade)
        {
            if (quantidade < 0)
                return BadRequest("Quantidade deve ser maior ou igual a zero.");

            var result = _applicationService.ObterPatiosComMotosDisponiveisAcimaDe(quantidade);

            if (result != null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(p => new
                    {
                        p.IdPatio,
                        p.NomePatio,
                        p.MotosTotaisPatio,
                        p.MotosDisponiveisPatio,
                        p.DataPatio,
                        p.MotosPatioAtual,
                        p.CamerasPatio,
                        p.LayoutPatio,
                        p.EnderecoPatio,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "Patio", new { id = p.IdPatio }, Request.Scheme),
                            put = Url.Action(nameof(Put), "Patio", new { id = p.IdPatio }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "Patio", new { id = p.IdPatio }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(GetByMotosDisponiveis), "Patio", new { quantidade }, Request.Scheme),
                        get = Url.Action(nameof(Get), "Patio", null, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NoContent();
        }

        [HttpGet("data-posterior")]
        [SwaggerOperation(
            Summary = "Obtém pátios por data posterior",
            Description = "Retorna uma lista paginada de pátios com data posterior à data especificada. " +
                        "Útil para encontrar pátios criados após uma determinada data."
        )]
        [SwaggerResponse(statusCode: 200, description: "Pátios encontrados com sucesso", type: typeof(IEnumerable<PatioResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum pátio encontrado para a data especificada")]
        [SwaggerResponse(statusCode: 400, description: "Data é obrigatória")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByDataPosterior(
            [FromQuery, SwaggerParameter("Data de referência", Required = true)] DateTime data)
        {
            var result = _applicationService.ObterPatiosPorDataPosterior(data);

            if (result != null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(p => new
                    {
                        p.IdPatio,
                        p.NomePatio,
                        p.MotosTotaisPatio,
                        p.MotosDisponiveisPatio,
                        p.DataPatio,
                        p.MotosPatioAtual,
                        p.CamerasPatio,
                        p.LayoutPatio,
                        p.EnderecoPatio,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "Patio", new { id = p.IdPatio }, Request.Scheme),
                            put = Url.Action(nameof(Put), "Patio", new { id = p.IdPatio }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "Patio", new { id = p.IdPatio }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(GetByDataPosterior), "Patio", new { data }, Request.Scheme),
                        get = Url.Action(nameof(Get), "Patio", null, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NoContent();
        }

        [HttpGet("data-anterior")]
        [SwaggerOperation(
            Summary = "Obtém pátios por data anterior",
            Description = "Retorna uma lista paginada de pátios com data anterior à data especificada. " +
                        "Útil para encontrar pátios criados antes de uma determinada data."
        )]
        [SwaggerResponse(statusCode: 200, description: "Pátios encontrados com sucesso", type: typeof(IEnumerable<PatioResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum pátio encontrado para a data especificada")]
        [SwaggerResponse(statusCode: 400, description: "Data é obrigatória")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByDataAnterior(
            [FromQuery, SwaggerParameter("Data de referência", Required = true)] DateTime data)
        {
            var result = _applicationService.ObterPatiosPorDataAnterior(data);

            if (result != null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(p => new
                    {
                        p.IdPatio,
                        p.NomePatio,
                        p.MotosTotaisPatio,
                        p.MotosDisponiveisPatio,
                        p.DataPatio,
                        p.MotosPatioAtual,
                        p.CamerasPatio,
                        p.LayoutPatio,
                        p.EnderecoPatio,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "Patio", new { id = p.IdPatio }, Request.Scheme),
                            put = Url.Action(nameof(Put), "Patio", new { id = p.IdPatio }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "Patio", new { id = p.IdPatio }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(GetByDataAnterior), "Patio", new { data }, Request.Scheme),
                        get = Url.Action(nameof(Get), "Patio", null, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NoContent();
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria novo pátio",
            Description = "Cria um novo pátio no sistema com os dados fornecidos. " +
                        "Valida se todos os campos obrigatórios estão preenchidos e se não há duplicatas. " +
                        "Retorna os dados do pátio criado incluindo o ID gerado."
        )]
        [SwaggerRequestExample(typeof(PatioRequestDto), typeof(PatioRequestDtoSample))]
        [SwaggerResponse(statusCode: 201, description: "Pátio criado com sucesso", type: typeof(PatioResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "Dados inválidos - campos obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível criar o pátio - dados inválidos ou duplicados")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult Post(
            [FromBody, SwaggerParameter("Dados do pátio a ser criado", Required = true)] PatioRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosPatio(entity);

                if (result is not null)
                    return CreatedAtAction(nameof(GetById), new { id = result.IdPatio }, result);

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
            Summary = "Atualiza pátio existente",
            Description = "Atualiza os dados de um pátio existente baseado no ID fornecido. " +
                        "Valida se o pátio existe e se os novos dados são válidos. " +
                        "Retorna os dados atualizados do pátio."
        )]
        [SwaggerRequestExample(typeof(PatioRequestDto), typeof(PatioRequestDtoSample))]
        [SwaggerResponse(statusCode: 200, description: "Pátio atualizado com sucesso", type: typeof(PatioResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido ou dados obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 404, description: "Pátio não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível atualizar o pátio - dados inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult Put(
            [FromRoute, SwaggerParameter("ID único do pátio a ser atualizado", Required = true)] int id, 
            [FromBody, SwaggerParameter("Novos dados do pátio", Required = true)] PatioRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosPatio(id, entity);

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
            Summary = "Remove pátio",
            Description = "Remove permanentemente um pátio do sistema baseado no ID fornecido. " +
                        "Esta operação é irreversível e remove todos os dados associados ao pátio."
        )]
        [SwaggerResponse(statusCode: 204, description: "Pátio removido com sucesso")]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "Pátio não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível remover o pátio")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult Delete(
            [FromRoute, SwaggerParameter("ID único do pátio a ser removido", Required = true)] int id)
        {
            var result = _applicationService.DeletarDadosPatio(id);

            if (result is not null)
                return NoContent();

            return NotFound();
        }
    }
}
