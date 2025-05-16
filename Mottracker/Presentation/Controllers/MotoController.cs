using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Mottracker.Application.Dtos;
using Mottracker.Application.Dtos.Moto;

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
        [SwaggerResponse(400, "Erro ao obter as motos")]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodasMotos();
            if (result is not null)
                return Ok(result);

            return BadRequest("Não foi possível obter os dados.");
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém moto por ID", Description = "Retorna os dados de uma moto específica.")]
        [SwaggerResponse(200, "Moto retornada com sucesso", typeof(MotoResponseDto))]
        [SwaggerResponse(400, "Erro ao obter a moto")]
        public IActionResult GetById(int id)
        {
            var result = _applicationService.ObterMotoPorId(id);
            if (result is not null)
                return Ok(result);

            return BadRequest("Não foi possível obter os dados.");
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
        [SwaggerOperation(Summary = "Atualiza uma moto", Description = "Edita os dados de uma moto já existente.")]
        [SwaggerResponse(200, "Moto atualizada com sucesso", typeof(MotoResponseDto))]
        [SwaggerResponse(400, "Erro ao atualizar a moto")]
        public IActionResult Put(int id, [FromBody] MotoRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosMoto(id, entity);
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
        [SwaggerOperation(Summary = "Remove uma moto", Description = "Deleta uma moto pelo ID fornecido.")]
        [SwaggerResponse(200, "Moto deletada com sucesso", typeof(MotoResponseDto))]
        [SwaggerResponse(400, "Erro ao deletar a moto")]
        public IActionResult Delete(int id)
        {
            var result = _applicationService.DeletarDadosMoto(id);
            if (result is not null)
                return Ok(result);

            return BadRequest("Não foi possível deletar os dados.");
        }
    }
}
