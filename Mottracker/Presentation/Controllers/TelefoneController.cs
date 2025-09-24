using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Telefone;
using Mottracker.Docs.Samples;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/v1/telefone")]
    [ApiController]
    public class TelefoneController : ControllerBase
    {
        private readonly ITelefoneApplicationService _applicationService;

        public TelefoneController(ITelefoneApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(
           Summary = "Lista telefones com paginação",
           Description = "Retorna uma lista paginada de telefones cadastrados no sistema. " +
                        "Este endpoint suporta paginação para otimizar a performance com grandes volumes de dados. " +
                        "Os telefones são retornados com informações completas e links HATEOAS para navegação."
        )]
        [SwaggerResponse(statusCode: 200, description: "Lista de telefones retornada com sucesso", type: typeof(IEnumerable<TelefoneResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum telefone encontrado")]
        [SwaggerResponse(statusCode: 400, description: "Parâmetros de paginação inválidos")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        [SwaggerResponseExample(statusCode: 200, typeof(TelefoneResponseListSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public IActionResult Get(
            [FromQuery, SwaggerParameter("Número de registros a pular (padrão: 0)", Required = false)] int Deslocamento = 0, 
            [FromQuery, SwaggerParameter("Número de registros a retornar (padrão: 3, máximo: 100)", Required = false)] int RegistrosRetornado = 3)
        {
            var result = _applicationService.ObterTodosTelefones();

            if (result is not null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(t => new
                    {
                        t.IdTelefone,
                        t.NumeroTelefone,
                        t.TipoTelefone,
                        t.UsuarioTelefone,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "Telefone", new { id = t.IdTelefone }, Request.Scheme),
                            put = Url.Action(nameof(Put), "Telefone", new { id = t.IdTelefone }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "Telefone", new { id = t.IdTelefone }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(Get), "Telefone", null, Request.Scheme),
                        create = Url.Action(nameof(Post), "Telefone", null, Request.Scheme),
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
            Summary = "Obtém telefone por ID",
            Description = "Retorna os dados completos de um telefone específico baseado no ID fornecido. " +
                        "Inclui informações detalhadas do telefone e links HATEOAS para operações relacionadas."
        )]
        [SwaggerResponse(statusCode: 200, description: "Telefone encontrado com sucesso", type: typeof(TelefoneResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "Telefone não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetById(
            [FromRoute, SwaggerParameter("ID único do telefone", Required = true)] int id)
        {
            var result = _applicationService.ObterTelefonePorId(id);

            if (result is not null)
            {
                var hateaos = new
                {
                    data = result,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Telefone", new { id }),
                        get = Url.Action(nameof(Get), "Telefone", null),
                        put = Url.Action(nameof(Put), "Telefone", new { id }),
                        delete = Url.Action(nameof(Delete), "Telefone", new { id }),
                    }
                };

                return Ok(hateaos);
            }

            return NotFound();
        }

        [HttpGet("numero/{numero}")]
        [SwaggerOperation(
            Summary = "Obtém telefones por número",
            Description = "Retorna uma lista paginada de telefones filtrados por número específico. " +
                        "Útil para encontrar todos os telefones com um determinado número."
        )]
        [SwaggerResponse(statusCode: 200, description: "Telefones encontrados com sucesso", type: typeof(IEnumerable<TelefoneResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum telefone encontrado para o número especificado")]
        [SwaggerResponse(statusCode: 400, description: "Número é obrigatório e não pode estar vazio")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByNumero(
            [FromRoute, SwaggerParameter("Número do telefone para busca", Required = true)] string numero)
        {
            var result = _applicationService.ObterTelefonesPorNumero(numero);

            if (result is not null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(t => new
                    {
                        t.IdTelefone,
                        t.NumeroTelefone,
                        t.TipoTelefone,
                        t.UsuarioTelefone,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "Telefone", new { id = t.IdTelefone }, Request.Scheme),
                            put = Url.Action(nameof(Put), "Telefone", new { id = t.IdTelefone }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "Telefone", new { id = t.IdTelefone }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(GetByNumero), "Telefone", new { numero }, Request.Scheme),
                        get = Url.Action(nameof(Get), "Telefone", null, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NoContent();
        }

        [HttpGet("usuario/{usuarioId}")]
        [SwaggerOperation(
            Summary = "Obtém telefones por usuário",
            Description = "Retorna uma lista paginada de telefones filtrados por usuário específico. " +
                        "Útil para encontrar todos os telefones de um determinado usuário."
        )]
        [SwaggerResponse(statusCode: 200, description: "Telefones encontrados com sucesso", type: typeof(IEnumerable<TelefoneResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum telefone encontrado para o usuário especificado")]
        [SwaggerResponse(statusCode: 400, description: "ID do usuário é obrigatório")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByUsuarioId(
            [FromRoute, SwaggerParameter("ID único do usuário", Required = true)] long usuarioId)
        {
            var result = _applicationService.ObterTelefonesPorUsuarioId(usuarioId);

            if (result is not null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(t => new
                    {
                        t.IdTelefone,
                        t.NumeroTelefone,
                        t.TipoTelefone,
                        t.UsuarioTelefone,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "Telefone", new { id = t.IdTelefone }, Request.Scheme),
                            put = Url.Action(nameof(Put), "Telefone", new { id = t.IdTelefone }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "Telefone", new { id = t.IdTelefone }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(GetByUsuarioId), "Telefone", new { usuarioId }, Request.Scheme),
                        get = Url.Action(nameof(Get), "Telefone", null, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NoContent();
        }

        [HttpGet("tipo/{tipo}")]
        [SwaggerOperation(
            Summary = "Obtém telefones por tipo",
            Description = "Retorna uma lista paginada de telefones filtrados por tipo específico. " +
                        "Útil para encontrar todos os telefones de um determinado tipo (celular, fixo, etc.)."
        )]
        [SwaggerResponse(statusCode: 200, description: "Telefones encontrados com sucesso", type: typeof(IEnumerable<TelefoneResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum telefone encontrado para o tipo especificado")]
        [SwaggerResponse(statusCode: 400, description: "Tipo é obrigatório e não pode estar vazio")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByTipo(
            [FromRoute, SwaggerParameter("Tipo do telefone para busca", Required = true)] string tipo)
        {
            var result = _applicationService.ObterTelefonesPorTipo(tipo);

            if (result is not null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(t => new
                    {
                        t.IdTelefone,
                        t.NumeroTelefone,
                        t.TipoTelefone,
                        t.UsuarioTelefone,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "Telefone", new { id = t.IdTelefone }, Request.Scheme),
                            put = Url.Action(nameof(Put), "Telefone", new { id = t.IdTelefone }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "Telefone", new { id = t.IdTelefone }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(GetByTipo), "Telefone", new { tipo }, Request.Scheme),
                        get = Url.Action(nameof(Get), "Telefone", null, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NoContent();
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria novo telefone",
            Description = "Cria um novo telefone no sistema com os dados fornecidos. " +
                        "Valida se todos os campos obrigatórios estão preenchidos e se não há duplicatas. " +
                        "Retorna os dados do telefone criado incluindo o ID gerado."
        )]
        [SwaggerRequestExample(typeof(TelefoneRequestDto), typeof(TelefoneRequestDtoSample))]
        [SwaggerResponse(statusCode: 201, description: "Telefone criado com sucesso", type: typeof(TelefoneResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "Dados inválidos - campos obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível criar o telefone - dados inválidos ou duplicados")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult Post(
            [FromBody, SwaggerParameter("Dados do telefone a ser criado", Required = true)] TelefoneRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosTelefone(entity);

                if (result is not null)
                    return CreatedAtAction(nameof(GetById), new { id = result.IdTelefone }, result);

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
            Summary = "Atualiza telefone existente",
            Description = "Atualiza os dados de um telefone existente baseado no ID fornecido. " +
                        "Valida se o telefone existe e se os novos dados são válidos. " +
                        "Retorna os dados atualizados do telefone."
        )]
        [SwaggerRequestExample(typeof(TelefoneRequestDto), typeof(TelefoneRequestDtoSample))]
        [SwaggerResponse(statusCode: 200, description: "Telefone atualizado com sucesso", type: typeof(TelefoneResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido ou dados obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 404, description: "Telefone não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível atualizar o telefone - dados inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult Put(
            [FromRoute, SwaggerParameter("ID único do telefone a ser atualizado", Required = true)] int id, 
            [FromBody, SwaggerParameter("Novos dados do telefone", Required = true)] TelefoneRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosTelefone(id, entity);

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
            Summary = "Remove telefone",
            Description = "Remove permanentemente um telefone do sistema baseado no ID fornecido. " +
                        "Esta operação é irreversível e remove todos os dados associados ao telefone."
        )]
        [SwaggerResponse(statusCode: 204, description: "Telefone removido com sucesso")]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "Telefone não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível remover o telefone")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult Delete(
            [FromRoute, SwaggerParameter("ID único do telefone a ser removido", Required = true)] int id)
        {
            var result = _applicationService.DeletarDadosTelefone(id);

            if (result is not null)
                return NoContent();

            return NotFound();
        }
    }
}
