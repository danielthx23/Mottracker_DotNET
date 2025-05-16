using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using System.Net;
using Mottracker.Application.Dtos;
using Mottracker.Application.Dtos.Camera;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao obter as câmeras")]
        [ProducesResponseType(typeof(IEnumerable<CameraResponseDto>), 200)]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodasCameras();

            if (result is not null)
                return Ok(result);

            return BadRequest("Não foi possível obter os dados.");
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém câmera por ID", Description = "Retorna os dados de uma câmera específica.")]
        [SwaggerResponse(200, "Câmera retornada com sucesso", typeof(CameraResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao obter a câmera")]
        [ProducesResponseType(typeof(CameraResponseDto), 200)]
        public IActionResult GetById(int id)
        {
            var result = _applicationService.ObterCameraPorId(id);

            if (result is not null)
                return Ok(result);

            return BadRequest("Não foi possível obter os dados.");
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
        [SwaggerOperation(Summary = "Atualiza uma câmera", Description = "Atualiza os dados de uma câmera existente.")]
        [SwaggerResponse(200, "Câmera atualizada com sucesso", typeof(CameraResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao atualizar a câmera")]
        [ProducesResponseType(typeof(CameraResponseDto), 200)]
        public IActionResult Put(int id, [FromBody] CameraRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosCamera(id, entity);

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
        [SwaggerOperation(Summary = "Deleta uma câmera", Description = "Remove uma câmera do sistema com base no ID.")]
        [SwaggerResponse(200, "Câmera deletada com sucesso", typeof(CameraResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao deletar a câmera")]
        [ProducesResponseType(typeof(CameraResponseDto), 200)]
        public IActionResult Delete(int id)
        {
            var result = _applicationService.DeletarDadosCamera(id);

            if (result is not null)
                return Ok(result);

            return BadRequest("Não foi possível deletar os dados.");
        }
    }
}
