using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Mottracker.Application.Dtos;
using Mottracker.Application.Dtos.Permissao;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissaoController : ControllerBase
    {
        private readonly IPermissaoApplicationService _applicationService;

        public PermissaoController(IPermissaoApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todas as permissões", Description = "Retorna todas as permissões cadastradas.")]
        [SwaggerResponse(200, "Permissões retornadas com sucesso", typeof(IEnumerable<PermissaoResponseDto>))]
        [SwaggerResponse(204, "Nenhuma permissão encontrada")]
        [SwaggerResponse(400, "Erro ao obter as permissões")]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodosPermissoes();

            if (result is not null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém permissão por ID", Description = "Retorna os dados de uma permissão específica.")]
        [SwaggerResponse(200, "Permissão retornada com sucesso", typeof(PermissaoResponseDto))]
        [SwaggerResponse(404, "Permissão não encontrada")]
        [SwaggerResponse(400, "Erro ao obter a permissão")]
        public IActionResult GetById(int id)
        {
            var result = _applicationService.ObterPermissaoPorId(id);

            if (result is not null)
                return Ok(result);

            return NotFound();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova permissão", Description = "Salva uma nova permissão no sistema.")]
        [SwaggerResponse(201, "Permissão criada com sucesso", typeof(PermissaoResponseDto))]
        [SwaggerResponse(400, "Erro ao salvar a permissão")]
        public IActionResult Post([FromBody] PermissaoRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosPermissao(entity);

                if (result is not null)
                    return CreatedAtAction(nameof(GetById), new { id = result.IdPermissao }, result);

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
        [SwaggerOperation(Summary = "Atualiza uma permissão", Description = "Edita os dados de uma permissão existente.")]
        [SwaggerResponse(200, "Permissão atualizada com sucesso", typeof(PermissaoResponseDto))]
        [SwaggerResponse(404, "Permissão não encontrada")]
        [SwaggerResponse(400, "Erro ao atualizar a permissão")]
        public IActionResult Put(int id, [FromBody] PermissaoRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosPermissao(id, entity);

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
        [SwaggerOperation(Summary = "Deleta uma permissão", Description = "Remove uma permissão com base no ID fornecido.")]
        [SwaggerResponse(204, "Permissão deletada com sucesso")]
        [SwaggerResponse(404, "Permissão não encontrada")]
        [SwaggerResponse(400, "Erro ao deletar a permissão")]
        public IActionResult Delete(int id)
        {
            var result = _applicationService.DeletarDadosPermissao(id);

            if (result is not null)
                return NoContent();

            return NotFound();
        }
    }
}
