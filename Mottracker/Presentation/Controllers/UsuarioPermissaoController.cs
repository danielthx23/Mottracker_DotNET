using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.UsuarioPermissao;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioPermissaoController : ControllerBase
    {
        private readonly IUsuarioPermissaoApplicationService _applicationService;

        public UsuarioPermissaoController(IUsuarioPermissaoApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todas as permissões de usuários", Description = "Retorna todos os registros de permissões de usuários cadastrados.")]
        [SwaggerResponse(200, "Lista de permissões retornada com sucesso", typeof(IEnumerable<UsuarioPermissaoResponseDto>))]
        [SwaggerResponse(204, "Nenhuma permissão encontrada")]
        [SwaggerResponse(400, "Erro ao obter os dados")]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodosUsuarioPermissoes();

            if (result != null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("usuario/{usuarioId}/permissao/{permissaoId}")]
        [SwaggerOperation(Summary = "Obtém permissão de usuário por ID composto", Description = "Retorna os dados de uma permissão de usuário específica.")]
        [SwaggerResponse(200, "Permissão de usuário retornada com sucesso", typeof(UsuarioPermissaoResponseDto))]
        [SwaggerResponse(404, "Permissão de usuário não encontrada")]
        [SwaggerResponse(400, "Erro ao obter o dado")]
        public IActionResult GetById(int usuarioId, int permissaoId)
        {
            var result = _applicationService.ObterUsuarioPermissaoPorId(usuarioId, permissaoId);

            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova permissão de usuário", Description = "Salva um novo registro de permissão de usuário.")]
        [SwaggerResponse(201, "Permissão de usuário salva com sucesso", typeof(UsuarioPermissaoResponseDto))]
        [SwaggerResponse(400, "Erro ao salvar a permissão de usuário")]
        public IActionResult Post([FromBody] UsuarioPermissaoRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosUsuarioPermissao(entity);

                if (result != null)
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
        [SwaggerOperation(Summary = "Atualiza uma permissão de usuário", Description = "Edita os dados de uma permissão de usuário existente.")]
        [SwaggerResponse(200, "Permissão de usuário atualizada com sucesso", typeof(UsuarioPermissaoResponseDto))]
        [SwaggerResponse(404, "Permissão de usuário não encontrada")]
        [SwaggerResponse(400, "Erro ao atualizar a permissão de usuário")]
        public IActionResult Put(int usuarioId, int permissaoId, [FromBody] UsuarioPermissaoRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosUsuarioPermissao(usuarioId, permissaoId, entity);

                if (result != null)
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
        [SwaggerOperation(Summary = "Deleta uma permissão de usuário", Description = "Remove uma permissão de usuário do sistema com base no ID composto.")]
        [SwaggerResponse(204, "Permissão de usuário deletada com sucesso")]
        [SwaggerResponse(404, "Permissão de usuário não encontrada")]
        [SwaggerResponse(400, "Erro ao deletar a permissão de usuário")]
        public IActionResult Delete(int usuarioId, int permissaoId)
        {
            var result = _applicationService.DeletarDadosUsuarioPermissao(usuarioId, permissaoId);

            if (result != null)
                return NoContent();

            return NotFound();
        }
    }
}
