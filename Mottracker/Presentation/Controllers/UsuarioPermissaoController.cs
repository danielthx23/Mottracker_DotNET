using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.UsuarioPermissao;
using Mottracker.Docs.Samples;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/v1/usuario-permissao")]
    [ApiController]
    public class UsuarioPermissaoController : ControllerBase
    {
        private readonly IUsuarioPermissaoApplicationService _applicationService;

        public UsuarioPermissaoController(IUsuarioPermissaoApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(
           Summary = "Lista permissões de usuários com paginação",
           Description = "Retorna uma lista paginada de permissões de usuários cadastradas no sistema. " +
                        "Este endpoint suporta paginação para otimizar a performance com grandes volumes de dados. " +
                        "As permissões são retornadas com informações completas e links HATEOAS para navegação."
        )]
        [SwaggerResponse(statusCode: 200, description: "Lista de permissões retornada com sucesso", type: typeof(IEnumerable<UsuarioPermissaoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhuma permissão encontrada")]
        [SwaggerResponse(statusCode: 400, description: "Parâmetros de paginação inválidos")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        [SwaggerResponseExample(statusCode: 200, typeof(UsuarioPermissaoResponseListSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public IActionResult Get(
            [FromQuery, SwaggerParameter("Número de registros a pular (padrão: 0)", Required = false)] int Deslocamento = 0, 
            [FromQuery, SwaggerParameter("Número de registros a retornar (padrão: 3, máximo: 100)", Required = false)] int RegistrosRetornado = 3)
        {
            var result = _applicationService.ObterTodosUsuarioPermissoes();

            if (result is not null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(up => new
                    {
                        up.IdUsuario,
                        up.IdPermissao,
                        up.UsuarioPermissao,
                        up.PermissaoUsuarioPermissao,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "UsuarioPermissao", new { usuarioId = up.IdUsuario, permissaoId = up.IdPermissao }, Request.Scheme),
                            put = Url.Action(nameof(Put), "UsuarioPermissao", new { usuarioId = up.IdUsuario, permissaoId = up.IdPermissao }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "UsuarioPermissao", new { usuarioId = up.IdUsuario, permissaoId = up.IdPermissao }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(Get), "UsuarioPermissao", null, Request.Scheme),
                        create = Url.Action(nameof(Post), "UsuarioPermissao", null, Request.Scheme),
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

        [HttpGet("usuario/{usuarioId}/permissao/{permissaoId}")]
        [SwaggerOperation(
            Summary = "Obtém permissão de usuário por ID composto",
            Description = "Retorna os dados completos de uma permissão de usuário específica baseada nos IDs fornecidos. " +
                        "Inclui informações detalhadas da permissão e links HATEOAS para operações relacionadas."
        )]
        [SwaggerResponse(statusCode: 200, description: "Permissão de usuário encontrada com sucesso", type: typeof(UsuarioPermissaoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "IDs inválidos - devem ser números positivos")]
        [SwaggerResponse(statusCode: 404, description: "Permissão de usuário não encontrada para os IDs fornecidos")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetById(
            [FromRoute, SwaggerParameter("ID único do usuário", Required = true)] int usuarioId, 
            [FromRoute, SwaggerParameter("ID único da permissão", Required = true)] int permissaoId)
        {
            var result = _applicationService.ObterUsuarioPermissaoPorId(usuarioId, permissaoId);

            if (result is not null)
            {
                var hateaos = new
                {
                    data = result,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "UsuarioPermissao", new { usuarioId, permissaoId }),
                        get = Url.Action(nameof(Get), "UsuarioPermissao", null),
                        put = Url.Action(nameof(Put), "UsuarioPermissao", new { usuarioId, permissaoId }),
                        delete = Url.Action(nameof(Delete), "UsuarioPermissao", new { usuarioId, permissaoId }),
                    }
                };

                return Ok(hateaos);
            }

            return NotFound();
        }

        [HttpGet("usuario/{usuarioId}")]
        [SwaggerOperation(
            Summary = "Obtém permissões por usuário",
            Description = "Retorna uma lista paginada de permissões filtradas por usuário específico. " +
                        "Útil para encontrar todas as permissões de um determinado usuário."
        )]
        [SwaggerResponse(statusCode: 200, description: "Permissões encontradas com sucesso", type: typeof(IEnumerable<UsuarioPermissaoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhuma permissão encontrada para o usuário especificado")]
        [SwaggerResponse(statusCode: 400, description: "ID do usuário é obrigatório")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByUsuarioId(
            [FromRoute, SwaggerParameter("ID único do usuário", Required = true)] long usuarioId)
        {
            var result = _applicationService.ObterUsuarioPermissoesPorUsuarioId(usuarioId);

            if (result is not null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(up => new
                    {
                        up.IdUsuario,
                        up.IdPermissao,
                        up.UsuarioPermissao,
                        up.PermissaoUsuarioPermissao,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "UsuarioPermissao", new { usuarioId = up.IdUsuario, permissaoId = up.IdPermissao }, Request.Scheme),
                            put = Url.Action(nameof(Put), "UsuarioPermissao", new { usuarioId = up.IdUsuario, permissaoId = up.IdPermissao }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "UsuarioPermissao", new { usuarioId = up.IdUsuario, permissaoId = up.IdPermissao }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(GetByUsuarioId), "UsuarioPermissao", new { usuarioId }, Request.Scheme),
                        get = Url.Action(nameof(Get), "UsuarioPermissao", null, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NoContent();
        }

        [HttpGet("permissao/{permissaoId}")]
        [SwaggerOperation(
            Summary = "Obtém usuários por permissão",
            Description = "Retorna uma lista paginada de usuários filtrados por permissão específica. " +
                        "Útil para encontrar todos os usuários que possuem uma determinada permissão."
        )]
        [SwaggerResponse(statusCode: 200, description: "Usuários encontrados com sucesso", type: typeof(IEnumerable<UsuarioPermissaoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum usuário encontrado para a permissão especificada")]
        [SwaggerResponse(statusCode: 400, description: "ID da permissão é obrigatório")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByPermissaoId(
            [FromRoute, SwaggerParameter("ID único da permissão", Required = true)] long permissaoId)
        {
            var result = _applicationService.ObterUsuarioPermissoesPorPermissaoId(permissaoId);

            if (result is not null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(up => new
                    {
                        up.IdUsuario,
                        up.IdPermissao,
                        up.UsuarioPermissao,
                        up.PermissaoUsuarioPermissao,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "UsuarioPermissao", new { usuarioId = up.IdUsuario, permissaoId = up.IdPermissao }, Request.Scheme),
                            put = Url.Action(nameof(Put), "UsuarioPermissao", new { usuarioId = up.IdUsuario, permissaoId = up.IdPermissao }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "UsuarioPermissao", new { usuarioId = up.IdUsuario, permissaoId = up.IdPermissao }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(GetByPermissaoId), "UsuarioPermissao", new { permissaoId }, Request.Scheme),
                        get = Url.Action(nameof(Get), "UsuarioPermissao", null, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NoContent();
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria nova permissão de usuário",
            Description = "Cria uma nova permissão de usuário no sistema com os dados fornecidos. " +
                        "Valida se todos os campos obrigatórios estão preenchidos e se não há duplicatas. " +
                        "Retorna os dados da permissão criada incluindo os IDs gerados."
        )]
        [SwaggerRequestExample(typeof(UsuarioPermissaoRequestDto), typeof(UsuarioPermissaoRequestDtoSample))]
        [SwaggerResponse(statusCode: 201, description: "Permissão de usuário criada com sucesso", type: typeof(UsuarioPermissaoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "Dados inválidos - campos obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível criar a permissão - dados inválidos ou duplicados")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult Post(
            [FromBody, SwaggerParameter("Dados da permissão de usuário a ser criada", Required = true)] UsuarioPermissaoRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosUsuarioPermissao(entity);

                if (result is not null)
                    return CreatedAtAction(nameof(GetById), new { usuarioId = result.IdUsuario, permissaoId = result.IdPermissao }, result);

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

        [HttpPut("usuario/{usuarioId}/permissao/{permissaoId}")]
        [SwaggerOperation(
            Summary = "Atualiza permissão de usuário existente",
            Description = "Atualiza os dados de uma permissão de usuário existente baseada nos IDs fornecidos. " +
                        "Valida se a permissão existe e se os novos dados são válidos. " +
                        "Retorna os dados atualizados da permissão."
        )]
        [SwaggerRequestExample(typeof(UsuarioPermissaoRequestDto), typeof(UsuarioPermissaoRequestDtoSample))]
        [SwaggerResponse(statusCode: 200, description: "Permissão de usuário atualizada com sucesso", type: typeof(UsuarioPermissaoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "IDs inválidos ou dados obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 404, description: "Permissão de usuário não encontrada para os IDs fornecidos")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível atualizar a permissão - dados inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult Put(
            [FromRoute, SwaggerParameter("ID único do usuário", Required = true)] int usuarioId, 
            [FromRoute, SwaggerParameter("ID único da permissão", Required = true)] int permissaoId, 
            [FromBody, SwaggerParameter("Novos dados da permissão de usuário", Required = true)] UsuarioPermissaoRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosUsuarioPermissao(usuarioId, permissaoId, entity);

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

        [HttpDelete("usuario/{usuarioId}/permissao/{permissaoId}")]
        [SwaggerOperation(
            Summary = "Remove permissão de usuário",
            Description = "Remove permanentemente uma permissão de usuário do sistema baseada nos IDs fornecidos. " +
                        "Esta operação é irreversível e remove todos os dados associados à permissão."
        )]
        [SwaggerResponse(statusCode: 204, description: "Permissão de usuário removida com sucesso")]
        [SwaggerResponse(statusCode: 400, description: "IDs inválidos - devem ser números positivos")]
        [SwaggerResponse(statusCode: 404, description: "Permissão de usuário não encontrada para os IDs fornecidos")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível remover a permissão")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult Delete(
            [FromRoute, SwaggerParameter("ID único do usuário", Required = true)] int usuarioId, 
            [FromRoute, SwaggerParameter("ID único da permissão", Required = true)] int permissaoId)
        {
            var result = _applicationService.DeletarDadosUsuarioPermissao(usuarioId, permissaoId);

            if (result is not null)
                return NoContent();

            return NotFound();
        }
    }
}
