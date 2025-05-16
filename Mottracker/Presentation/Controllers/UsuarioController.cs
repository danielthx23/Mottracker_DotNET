using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Mottracker.Application.Dtos.Telefone;
using Mottracker.Application.Dtos.Usuario;

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
        [SwaggerResponse(400, "Erro ao obter os dados")]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodosUsuarios();
            if (result is not null)
                return Ok(result);

            return BadRequest("Não foi possível obter os dados.");
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém usuário por ID", Description = "Retorna os dados de um usuário específico.")]
        [SwaggerResponse(200, "Usuário retornado com sucesso", typeof(UsuarioResponseDto))]
        [SwaggerResponse(400, "Erro ao obter o dado")]
        public IActionResult GetById(int id)
        {
            var result = _applicationService.ObterUsuarioPorId(id);
            if (result is not null)
                return Ok(result);

            return BadRequest("Não foi possível obter os dados.");
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
                if (result is not null)
                    return Ok(result);

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
        [SwaggerResponse(400, "Erro ao atualizar o dado")]
        public IActionResult Put(int id, [FromBody] UsuarioRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosUsuario(id, entity);
                if (result is not null)
                    return Ok(result);

                return BadRequest("Não foi possível atualizar os dados.");
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
        [SwaggerResponse(200, "Usuário deletado com sucesso", typeof(UsuarioResponseDto))]
        [SwaggerResponse(400, "Erro ao deletar o dado")]
        public IActionResult Delete(int id)
        {
            var result = _applicationService.DeletarDadosUsuario(id);
            if (result is not null)
                return Ok(result);

            return BadRequest("Não foi possível deletar os dados.");
        }
    }
}
