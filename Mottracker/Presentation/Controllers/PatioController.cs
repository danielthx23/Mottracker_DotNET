using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatioController : ControllerBase
    {
        private readonly IPatioApplicationService _applicationService;

        public PatioController(IPatioApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos os pátios", Description = "Retorna todos os pátios cadastrados.")]
        [SwaggerResponse(200, "Pátios retornados com sucesso", typeof(IEnumerable<PatioEntity>))]
        [SwaggerResponse(400, "Erro ao obter os pátios")]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodosPatios();
            if (result is not null)
                return Ok(result);

            return BadRequest("Não foi possível obter os dados.");
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém pátio por ID", Description = "Retorna os dados de um pátio específico.")]
        [SwaggerResponse(200, "Pátio retornado com sucesso", typeof(PatioEntity))]
        [SwaggerResponse(400, "Erro ao obter o pátio")]
        public IActionResult GetById(int id)
        {
            var result = _applicationService.ObterPatioPorId(id);
            if (result is not null)
                return Ok(result);

            return BadRequest("Não foi possível obter os dados.");
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo pátio", Description = "Salva um novo pátio no sistema.")]
        [SwaggerResponse(201, "Pátio criado com sucesso", typeof(PatioEntity))]
        [SwaggerResponse(400, "Erro ao salvar o pátio")]
        public IActionResult Post([FromBody] PatioEntity entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosPatio(entity);
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
        [SwaggerOperation(Summary = "Atualiza um pátio", Description = "Edita os dados de um pátio existente.")]
        [SwaggerResponse(200, "Pátio atualizado com sucesso", typeof(PatioEntity))]
        [SwaggerResponse(400, "Erro ao atualizar o pátio")]
        public IActionResult Put(int id, [FromBody] PatioEntity entity)
        {
            try
            {
                var result = _applicationService.EditarDadosPatio(id, entity);
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
        [SwaggerOperation(Summary = "Deleta um pátio", Description = "Remove um pátio com base no ID fornecido.")]
        [SwaggerResponse(200, "Pátio deletado com sucesso", typeof(PatioEntity))]
        [SwaggerResponse(400, "Erro ao deletar o pátio")]
        public IActionResult Delete(int id)
        {
            var result = _applicationService.DeletarDadosPatio(id);
            if (result is not null)
                return Ok(result);

            return BadRequest("Não foi possível deletar os dados.");
        }
    }
}
