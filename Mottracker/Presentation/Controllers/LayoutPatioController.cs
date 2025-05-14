using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
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
        [SwaggerResponse(200, "Layouts retornados com sucesso", typeof(IEnumerable<LayoutPatioEntity>))]
        [SwaggerResponse(400, "Erro ao obter os layouts")]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodosLayoutsPatios();
            if (result is not null)
                return Ok(result);

            return BadRequest("Não foi possível obter os dados.");
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém layout de pátio por ID", Description = "Retorna os dados de um layout de pátio específico.")]
        [SwaggerResponse(200, "Layout retornado com sucesso", typeof(LayoutPatioEntity))]
        [SwaggerResponse(400, "Erro ao obter o layout")]
        public IActionResult GetById(int id)
        {
            var result = _applicationService.ObterLayoutPatioPorId(id);
            if (result is not null)
                return Ok(result);

            return BadRequest("Não foi possível obter os dados.");
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo layout de pátio", Description = "Salva um novo layout de pátio no sistema.")]
        [SwaggerResponse(201, "Layout criado com sucesso", typeof(LayoutPatioEntity))]
        [SwaggerResponse(400, "Erro ao salvar o layout")]
        public IActionResult Post([FromBody] LayoutPatioEntity entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosLayoutPatio(entity);
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
        [SwaggerOperation(Summary = "Atualiza um layout de pátio", Description = "Edita os dados de um layout de pátio já existente.")]
        [SwaggerResponse(200, "Layout atualizado com sucesso", typeof(LayoutPatioEntity))]
        [SwaggerResponse(400, "Erro ao atualizar o layout")]
        public IActionResult Put(int id, [FromBody] LayoutPatioEntity entity)
        {
            try
            {
                var result = _applicationService.EditarDadosLayoutPatio(id, entity);
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
        [SwaggerOperation(Summary = "Remove um layout de pátio", Description = "Deleta um layout de pátio pelo ID fornecido.")]
        [SwaggerResponse(200, "Layout deletado com sucesso", typeof(LayoutPatioEntity))]
        [SwaggerResponse(400, "Erro ao deletar o layout")]
        public IActionResult Delete(int id)
        {
            var result = _applicationService.DeletarDadosLayoutPatio(id);
            if (result is not null)
                return Ok(result);

            return BadRequest("Não foi possível deletar os dados.");
        }
    }
}
