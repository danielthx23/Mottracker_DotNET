using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Camera;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Mottracker.Domain.Enums;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CameraController : ControllerBase
    {
        private readonly ICameraApplicationService _applicationService;

        public CameraController(ICameraApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todas as câmeras", Description = "Retorna todas as câmeras cadastradas no sistema.")]
        [SwaggerResponse(200, "Lista de câmeras retornada com sucesso", typeof(List<CameraResponseDto>))]
        [SwaggerResponse(204, "Nenhuma câmera encontrada")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao obter as câmeras")]
        [ProducesResponseType(typeof(IEnumerable<CameraResponseDto>), 200)]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodasCameras();

            if (result is not null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém câmera por ID", Description = "Retorna os dados de uma câmera específica.")]
        [SwaggerResponse(200, "Câmera retornada com sucesso", typeof(CameraResponseDto))]
        [SwaggerResponse(404, "Câmera não encontrada")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao obter a câmera")]
        [ProducesResponseType(typeof(CameraResponseDto), 200)]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _applicationService.ObterCameraPorId(id);
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

        [HttpGet("por-nome")]
        [SwaggerOperation(Summary = "Obtém câmeras por nome", Description = "Retorna todas as câmeras que correspondem ao nome informado.")]
        [SwaggerResponse(200, "Câmeras retornadas com sucesso", typeof(IEnumerable<CameraResponseDto>))]
        [SwaggerResponse(204, "Nenhuma câmera encontrada com o nome especificado")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao obter as câmeras")]
        [ProducesResponseType(typeof(IEnumerable<CameraResponseDto>), 200)]
        public IActionResult GetByNome([FromQuery] string nome)
        {
            try
            {
                var result = _applicationService.ObterCameraPorNome(nome);

                if (result is not null && result.Any())
                    return Ok(result);

                return NoContent();
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

        [HttpGet("por-status")]
        [SwaggerOperation(Summary = "Obtém câmeras por status", Description = "Retorna todas as câmeras que possuem o status informado.")]
        [SwaggerResponse(200, "Câmeras retornadas com sucesso", typeof(IEnumerable<CameraResponseDto>))]
        [SwaggerResponse(204, "Nenhuma câmera encontrada com o status especificado")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao obter as câmeras")]
        [ProducesResponseType(typeof(IEnumerable<CameraResponseDto>), 200)]
        public IActionResult GetByStatus([FromQuery] CameraStatus status)
        {
            try
            {
                var result = _applicationService.ObterCameraPorStatus(status);

                if (result is not null && result.Any())
                    return Ok(result);

                return NoContent();
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
        [SwaggerOperation(Summary = "Salva uma nova câmera", Description = "Cria uma nova câmera no sistema.")]
        [SwaggerResponse(201, "Câmera criada com sucesso", typeof(CameraResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao salvar a câmera")]
        [ProducesResponseType(typeof(CameraResponseDto), 201)]
        public IActionResult Post([FromBody] CameraRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosCamera(entity);

                if (result is not null)
                    return CreatedAtAction(nameof(GetById), new { id = result.IdCamera }, result);

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
        [SwaggerOperation(Summary = "Atualiza uma câmera", Description = "Atualiza os dados de uma câmera existente.")]
        [SwaggerResponse(200, "Câmera atualizada com sucesso", typeof(CameraResponseDto))]
        [SwaggerResponse(404, "Câmera não encontrada")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao atualizar a câmera")]
        [ProducesResponseType(typeof(CameraResponseDto), 200)]
        public IActionResult Put(int id, [FromBody] CameraRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosCamera(id, entity);

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
        [SwaggerOperation(Summary = "Deleta uma câmera", Description = "Remove uma câmera do sistema com base no ID.")]
        [SwaggerResponse(204, "Câmera deletada com sucesso")]
        [SwaggerResponse(404, "Câmera não encontrada")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao deletar a câmera")]
        public IActionResult Delete(int id)
        {
            var result = _applicationService.DeletarDadosCamera(id);

            if (result is not null)
                return NoContent();

            return NotFound();
        }
    }
}