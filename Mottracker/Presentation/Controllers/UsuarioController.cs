using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Usuario;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioApplicationService _applicationService;

        public UsuarioController(IUsuarioApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos os usuários", Description = "Retorna todos os registros de usuários cadastrados.")]
        [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<UsuarioResponseDto>))]
        [SwaggerResponse(204, "Nenhum usuário encontrado")]
        [SwaggerResponse(400, "Erro ao obter os dados")]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodosUsuarios();

            if (result != null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém usuário por ID", Description = "Retorna os dados de um usuário específico.")]
        [SwaggerResponse(200, "Usuário retornado com sucesso", typeof(UsuarioResponseDto))]
        [SwaggerResponse(404, "Usuário não encontrado")]
        [SwaggerResponse(400, "Erro ao obter o dado")]
        public IActionResult GetById(int id)
        {
            var result = _applicationService.ObterUsuarioPorId(id);

            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo usuário", Description = "Salva um novo registro de usuário.")]
        [SwaggerResponse(201, "Usuário salvo com sucesso", typeof(UsuarioResponseDto))]
        [SwaggerResponse(400, "Erro ao salvar o dado")]
        public IActionResult Post([FromBody] UsuarioRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosUsuario(entity);

                if (result != null)
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
        [SwaggerOperation(Summary = "Atualiza um usuário", Description = "Edita os dados de um usuário existente.")]
        [SwaggerResponse(200, "Usuário atualizado com sucesso", typeof(UsuarioResponseDto))]
        [SwaggerResponse(404, "Usuário não encontrado")]
        [SwaggerResponse(400, "Erro ao atualizar o dado")]
        public IActionResult Put(int id, [FromBody] UsuarioRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosUsuario(id, entity);

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

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleta um usuário", Description = "Remove um usuário do sistema com base no ID.")]
        [SwaggerResponse(204, "Usuário deletado com sucesso")]
        [SwaggerResponse(404, "Usuário não encontrado")]
        [SwaggerResponse(400, "Erro ao deletar o dado")]
        public IActionResult Delete(int id)
        {
            var result = _applicationService.DeletarDadosUsuario(id);

            if (result != null)
                return NoContent();

            return NotFound();
        }
    }
}
