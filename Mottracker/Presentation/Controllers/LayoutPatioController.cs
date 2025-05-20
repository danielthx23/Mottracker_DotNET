using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.LayoutPatio;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LayoutPatioController : ControllerBase
    {
        private readonly ILayoutPatioApplicationService _applicationService;

        public LayoutPatioController(ILayoutPatioApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos os layouts de pátio", Description = "Retorna todos os layouts de pátio cadastrados.")]
        [SwaggerResponse(200, "Layouts retornados com sucesso", typeof(IEnumerable<LayoutPatioResponseDto>))]
        [SwaggerResponse(204, "Nenhum layout encontrado")]
        [SwaggerResponse(400, "Erro ao obter os layouts")]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodosLayoutsPatios();

            if (result is not null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém layout de pátio por ID", Description = "Retorna os dados de um layout de pátio específico.")]
        [SwaggerResponse(200, "Layout retornado com sucesso", typeof(LayoutPatioResponseDto))]
        [SwaggerResponse(404, "Layout não encontrado")]
        [SwaggerResponse(400, "Erro ao obter o layout")]
        public IActionResult GetById(int id)
        {
            var result = _applicationService.ObterLayoutPatioPorId(id);

            if (result is not null)
                return Ok(result);

            return NotFound();
        }

        [HttpGet("porPatio")]
        [SwaggerOperation(Summary = "Obtém layouts por ID do pátio", Description = "Retorna os layouts vinculados a um pátio específico.")]
        [SwaggerResponse(200, "Layouts retornados com sucesso", typeof(IEnumerable<LayoutPatioResponseDto>))]
        [SwaggerResponse(204, "Nenhum layout encontrado")]
        [SwaggerResponse(400, "Erro ao obter os layouts")]
        public IActionResult GetByPatioId([FromQuery] long patioId)
        {
            if (patioId <= 0)
                return BadRequest("O parâmetro patioId deve ser maior que zero.");

            var result = _applicationService.ObterLayoutsPorIdPatio(patioId);

            if (result is not null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("porDataCriacao")]
        [SwaggerOperation(Summary = "Obtém layouts por intervalo de data de criação", Description = "Retorna os layouts criados entre duas datas.")]
        [SwaggerResponse(200, "Layouts retornados com sucesso", typeof(IEnumerable<LayoutPatioResponseDto>))]
        [SwaggerResponse(204, "Nenhum layout encontrado")]
        [SwaggerResponse(400, "Erro ao obter os layouts")]
        public IActionResult GetByDataCriacao([FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim)
        {
            if (dataInicio == default || dataFim == default)
                return BadRequest("Os parâmetros dataInicio e dataFim são obrigatórios.");

            if (dataInicio > dataFim)
                return BadRequest("A data de início não pode ser maior que a data fim.");

            var result = _applicationService.ObterLayoutsPorDataCriacaoEntre(dataInicio, dataFim);

            if (result is not null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo layout de pátio", Description = "Salva um novo layout de pátio no sistema.")]
        [SwaggerResponse(201, "Layout criado com sucesso", typeof(LayoutPatioResponseDto))]
        [SwaggerResponse(400, "Erro ao salvar o layout")]
        public IActionResult Post([FromBody] LayoutPatioRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosLayoutPatio(entity);

                if (result is not null)
                    return CreatedAtAction(nameof(GetById), new { id = result.IdLayoutPatio }, result);

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
        [SwaggerOperation(Summary = "Atualiza um layout de pátio", Description = "Edita os dados de um layout de pátio já existente.")]
        [SwaggerResponse(200, "Layout atualizado com sucesso", typeof(LayoutPatioResponseDto))]
        [SwaggerResponse(404, "Layout não encontrado")]
        [SwaggerResponse(400, "Erro ao atualizar o layout")]
        public IActionResult Put(int id, [FromBody] LayoutPatioRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosLayoutPatio(id, entity);

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
        [SwaggerOperation(Summary = "Remove um layout de pátio", Description = "Deleta um layout de pátio pelo ID fornecido.")]
        [SwaggerResponse(204, "Layout deletado com sucesso")]
        [SwaggerResponse(404, "Layout não encontrado")]
        [SwaggerResponse(400, "Erro ao deletar o layout")]
        public IActionResult Delete(int id)
        {
            var result = _applicationService.DeletarDadosLayoutPatio(id);

            if (result is not null)
                return NoContent();

            return NotFound();
        }
    }
}
