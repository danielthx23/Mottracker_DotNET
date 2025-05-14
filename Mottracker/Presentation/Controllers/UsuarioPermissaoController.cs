using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
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
        [SwaggerResponse(200, "Lista de permissões retornada com sucesso", typeof(IEnumerable<UsuarioPermissaoEntity>))]
        [SwaggerResponse(400, "Erro ao obter os dados")]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodosUsuarioPermissoes();
            if (result is not null)
                return Ok(result);

            return BadRequest("Não foi possível obter os dados.");
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém permissão de usuário por ID", Description = "Retorna os dados de uma permissão de usuário específica.")]
        [SwaggerResponse(200, "Permissão de usuário retornada com sucesso", typeof(UsuarioPermissaoEntity))]
        [SwaggerResponse(400, "Erro ao obter o dado")]
        public IActionResult GetById(int id)
        {
            var result = _applicationService.ObterUsuarioPermissaoPorId(id);
            if (result is not null)
                return Ok(result);

            return BadRequest("Não foi possível obter os dados.");
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova permissão de usuário", Description = "Salva um novo registro de permissão de usuário.")]
        [SwaggerResponse(201, "Permissão de usuário salva com sucesso", typeof(UsuarioPermissaoEntity))]
        [SwaggerResponse(400, "Erro ao salvar a permissão de usuário")]
        public IActionResult Post([FromBody] UsuarioPermissaoEntity entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosUsuarioPermissao(entity);
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
        [SwaggerOperation(Summary = "Atualiza uma permissão de usuário", Description = "Edita os dados de uma permissão de usuário existente.")]
        [SwaggerResponse(200, "Permissão de usuário atualizada com sucesso", typeof(UsuarioPermissaoEntity))]
        [SwaggerResponse(400, "Erro ao atualizar a permissão de usuário")]
        public IActionResult Put(int id, [FromBody] UsuarioPermissaoEntity entity)
        {
            try
            {
                var result = _applicationService.EditarDadosUsuarioPermissao(id, entity);
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
        [SwaggerOperation(Summary = "Deleta uma permissão de usuário", Description = "Remove uma permissão de usuário do sistema com base no ID.")]
        [SwaggerResponse(200, "Permissão de usuário deletada com sucesso", typeof(UsuarioPermissaoEntity))]
        [SwaggerResponse(400, "Erro ao deletar a permissão de usuário")]
        public IActionResult Delete(int id)
        {
            var result = _applicationService.DeletarDadosUsuarioPermissao(id);
            if (result is not null)
                return Ok(result);

            return BadRequest("Não foi possível deletar os dados.");
        }
    }
}
