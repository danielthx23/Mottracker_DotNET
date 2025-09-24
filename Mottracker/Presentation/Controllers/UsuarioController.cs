using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Usuario;
using Mottracker.Docs.Samples;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioApplicationService _applicationService;

        public UsuarioController(IUsuarioApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(
           Summary = "Lista usuários com paginação",
           Description = "Retorna uma lista paginada de usuários cadastrados no sistema. " +
                        "Este endpoint suporta paginação para otimizar a performance com grandes volumes de dados. " +
                        "Os usuários são retornados com informações completas e links HATEOAS para navegação."
        )]
        [SwaggerResponse(statusCode: 200, description: "Lista de usuários retornada com sucesso", type: typeof(IEnumerable<UsuarioResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum usuário encontrado")]
        [SwaggerResponse(statusCode: 400, description: "Parâmetros de paginação inválidos")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        [SwaggerResponseExample(statusCode: 200, typeof(UsuarioResponseListSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public IActionResult Get(
            [FromQuery, SwaggerParameter("Número de registros a pular (padrão: 0)", Required = false)] int Deslocamento = 0, 
            [FromQuery, SwaggerParameter("Número de registros a retornar (padrão: 3, máximo: 100)", Required = false)] int RegistrosRetornado = 3)
        {
            var result = _applicationService.ObterTodosUsuarios();

            if (result is not null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(u => new
                    {
                        u.IdUsuario,
                        u.NomeUsuario,
                        u.CPFUsuario,
                        u.SenhaUsuario,
                        u.CNHUsuario,
                        u.EmailUsuario,
                        u.TokenUsuario,
                        u.DataNascimentoUsuario,
                        u.CriadoEmUsuario,
                        u.ContratoUsuario,
                        u.Telefones,
                        u.UsuarioPermissoes,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "Usuario", new { id = u.IdUsuario }, Request.Scheme),
                            put = Url.Action(nameof(Put), "Usuario", new { id = u.IdUsuario }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "Usuario", new { id = u.IdUsuario }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(Get), "Usuario", null, Request.Scheme),
                        create = Url.Action(nameof(Post), "Usuario", null, Request.Scheme),
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
            Summary = "Obtém usuário por ID",
            Description = "Retorna os dados completos de um usuário específico baseado no ID fornecido. " +
                        "Inclui informações detalhadas do usuário e links HATEOAS para operações relacionadas."
        )]
        [SwaggerResponse(statusCode: 200, description: "Usuário encontrado com sucesso", type: typeof(UsuarioResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "Usuário não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetById(
            [FromRoute, SwaggerParameter("ID único do usuário", Required = true)] int id)
        {
            var result = _applicationService.ObterUsuarioPorId(id);

            if (result is not null)
            {
                var hateaos = new
                {
                    data = result,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Usuario", new { id }),
                        get = Url.Action(nameof(Get), "Usuario", null),
                        put = Url.Action(nameof(Put), "Usuario", new { id }),
                        delete = Url.Action(nameof(Delete), "Usuario", new { id }),
                    }
                };

                return Ok(hateaos);
            }

            return NotFound();
        }

        [HttpGet("email/{email}")]
        [SwaggerOperation(
            Summary = "Obtém usuário por e-mail",
            Description = "Retorna os dados completos de um usuário específico baseado no e-mail fornecido. " +
                        "Útil para autenticação e busca por e-mail."
        )]
        [SwaggerResponse(statusCode: 200, description: "Usuário encontrado com sucesso", type: typeof(UsuarioResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "E-mail é obrigatório e deve ser válido")]
        [SwaggerResponse(statusCode: 404, description: "Usuário não encontrado para o e-mail fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByEmail(
            [FromRoute, SwaggerParameter("E-mail do usuário para busca", Required = true)] string email)
        {
            try
            {
                var result = _applicationService.ObterUsuarioPorEmail(email);

                if (result is not null)
                {
                    var hateaos = new
                    {
                        data = result,
                        links = new
                        {
                            self = Url.Action(nameof(GetByEmail), "Usuario", new { email }, Request.Scheme),
                            get = Url.Action(nameof(Get), "Usuario", null, Request.Scheme),
                            put = Url.Action(nameof(Put), "Usuario", new { id = result.IdUsuario }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "Usuario", new { id = result.IdUsuario }, Request.Scheme),
                        }
                    };

                    return Ok(hateaos);
                }

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

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria novo usuário",
            Description = "Cria um novo usuário no sistema com os dados fornecidos. " +
                        "Valida se todos os campos obrigatórios estão preenchidos e se não há duplicatas. " +
                        "Retorna os dados do usuário criado incluindo o ID gerado."
        )]
        [SwaggerRequestExample(typeof(UsuarioRequestDto), typeof(UsuarioRequestDtoSample))]
        [SwaggerResponse(statusCode: 201, description: "Usuário criado com sucesso", type: typeof(UsuarioResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "Dados inválidos - campos obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível criar o usuário - dados inválidos ou duplicados")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult Post(
            [FromBody, SwaggerParameter("Dados do usuário a ser criado", Required = true)] UsuarioRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosUsuario(entity);

                if (result is not null)
                    return CreatedAtAction(nameof(GetById), new { id = result.IdUsuario }, result);

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
            Summary = "Atualiza usuário existente",
            Description = "Atualiza os dados de um usuário existente baseado no ID fornecido. " +
                        "Valida se o usuário existe e se os novos dados são válidos. " +
                        "Retorna os dados atualizados do usuário."
        )]
        [SwaggerRequestExample(typeof(UsuarioRequestDto), typeof(UsuarioRequestDtoSample))]
        [SwaggerResponse(statusCode: 200, description: "Usuário atualizado com sucesso", type: typeof(UsuarioResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido ou dados obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 404, description: "Usuário não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível atualizar o usuário - dados inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult Put(
            [FromRoute, SwaggerParameter("ID único do usuário a ser atualizado", Required = true)] int id, 
            [FromBody, SwaggerParameter("Novos dados do usuário", Required = true)] UsuarioRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosUsuario(id, entity);

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
            Summary = "Remove usuário",
            Description = "Remove permanentemente um usuário do sistema baseado no ID fornecido. " +
                        "Esta operação é irreversível e remove todos os dados associados ao usuário."
        )]
        [SwaggerResponse(statusCode: 204, description: "Usuário removido com sucesso")]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "Usuário não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível remover o usuário")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult Delete(
            [FromRoute, SwaggerParameter("ID único do usuário a ser removido", Required = true)] int id)
        {
            var result = _applicationService.DeletarDadosUsuario(id);

            if (result is not null)
                return NoContent();

            return NotFound();
        }
    }
}
