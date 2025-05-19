using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Moto;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoController : ControllerBase
    {
        private readonly IMotoApplicationService _applicationService;

        public MotoController(IMotoApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todas as motos", Description = "Retorna todas as motos cadastradas.")]
        [SwaggerResponse(200, "Motos retornadas com sucesso", typeof(IEnumerable<MotoResponseDto>))]
        [SwaggerResponse(204, "Nenhuma moto encontrada")]
        [SwaggerResponse(400, "Erro ao obter as motos")]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodasMotos();

            if (result is not null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém moto por ID", Description = "Retorna os dados de uma moto específica.")]
        [SwaggerResponse(200, "Moto retornada com sucesso", typeof(MotoResponseDto))]
        [SwaggerResponse(404, "Moto não encontrada")]
        [SwaggerResponse(400, "Erro ao obter a moto")]
        public IActionResult GetById(int id)
        {
            var result = _applicationService.ObterMotoPorId(id);

            if (result is not null)
                return Ok(result);

            return NotFound();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova moto", Description = "Salva uma nova moto no sistema.")]
        [SwaggerResponse(201, "Moto criada com sucesso", typeof(MotoResponseDto))]
        [SwaggerResponse(400, "Erro ao salvar a moto")]
        public IActionResult Post([FromBody] MotoRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosMoto(entity);

                if (result is not null)
                    return CreatedAtAction(nameof(GetById), new { id = result.IdMoto }, result);

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
        [SwaggerOperation(Summary = "Atualiza uma moto", Description = "Edita os dados de uma moto já existente.")]
        [SwaggerResponse(200, "Moto atualizada com sucesso", typeof(MotoResponseDto))]
        [SwaggerResponse(404, "Moto não encontrada")]
        [SwaggerResponse(400, "Erro ao atualizar a moto")]
        public IActionResult Put(int id, [FromBody] MotoRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosMoto(id, entity);

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
        [SwaggerOperation(Summary = "Remove uma moto", Description = "Deleta uma moto pelo ID fornecido.")]
        [SwaggerResponse(204, "Moto deletada com sucesso")]
        [SwaggerResponse(404, "Moto não encontrada")]
        [SwaggerResponse(400, "Erro ao deletar a moto")]
        public IActionResult Delete(int id)
        {
            var result = _applicationService.DeletarDadosMoto(id);

            if (result is not null)
                return NoContent();

            return NotFound();
        }
    }
}
