using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Contrato;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoApplicationService _applicationService;

        public ContratoController(IContratoApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos os contratos", Description = "Retorna todos os contratos cadastrados.")]
        [SwaggerResponse(200, "Lista de contratos retornada com sucesso", typeof(IEnumerable<ContratoResponseDto>))]
        [SwaggerResponse(204, "Nenhum contrato encontrado")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao obter os contratos")]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodosContratos();

            if (result is not null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém contrato por ID", Description = "Retorna os dados de um contrato específico.")]
        [SwaggerResponse(200, "Contrato retornado com sucesso", typeof(ContratoResponseDto))]
        [SwaggerResponse(404, "Contrato não encontrado")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao obter o contrato")]
        public IActionResult GetById(int id)
        {
            var result = _applicationService.ObterContratoPorId(id);

            if (result is not null)
                return Ok(result);

            return NotFound();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo contrato", Description = "Salva um novo contrato no sistema.")]
        [SwaggerResponse(201, "Contrato criado com sucesso", typeof(ContratoResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao salvar o contrato")]
        public IActionResult Post([FromBody] ContratoRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosContrato(entity);

                if (result is not null)
                    return CreatedAtAction(nameof(GetById), new { id = result.IdContrato }, result);

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
        [SwaggerOperation(Summary = "Atualiza contrato existente", Description = "Edita os dados de um contrato já existente.")]
        [SwaggerResponse(200, "Contrato atualizado com sucesso", typeof(ContratoResponseDto))]
        [SwaggerResponse(404, "Contrato não encontrado")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao atualizar o contrato")]
        public IActionResult Put(int id, [FromBody] ContratoRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosContrato(id, entity);

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
        [SwaggerOperation(Summary = "Remove um contrato", Description = "Deleta um contrato pelo ID fornecido.")]
        [SwaggerResponse(204, "Contrato deletado com sucesso")]
        [SwaggerResponse(404, "Contrato não encontrado")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao deletar o contrato")]
        public IActionResult Delete(int id)
        {
            var result = _applicationService.DeletarDadosContrato(id);

            if (result is not null)
                return NoContent();

            return NotFound();
        }
    }
}