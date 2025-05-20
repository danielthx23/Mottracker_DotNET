using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Telefone;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelefoneController : ControllerBase
    {
        private readonly ITelefoneApplicationService _applicationService;

        public TelefoneController(ITelefoneApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos os telefones", Description = "Retorna todos os registros de telefones cadastrados.")]
        [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<TelefoneResponseDto>))]
        [SwaggerResponse(204, "Nenhum telefone encontrado")]
        [SwaggerResponse(400, "Erro ao obter os dados")]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodosTelefones();

            if (result is not null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém telefone por ID", Description = "Retorna os dados de um telefone específico.")]
        [SwaggerResponse(200, "Telefone retornado com sucesso", typeof(TelefoneResponseDto))]
        [SwaggerResponse(404, "Telefone não encontrado")]
        [SwaggerResponse(400, "Erro ao obter o dado")]
        public IActionResult GetById(int id)
        {
            var result = _applicationService.ObterTelefonePorId(id);

            if (result is not null)
                return Ok(result);

            return NotFound();
        }

        [HttpGet("por-numero/{numero}")]
        [SwaggerOperation(Summary = "Lista telefones por número", Description = "Retorna os telefones que possuem o número informado.")]
        [SwaggerResponse(200, "Telefones encontrados", typeof(IEnumerable<TelefoneResponseDto>))]
        [SwaggerResponse(204, "Nenhum telefone encontrado")]
        public IActionResult GetByNumero(string numero)
        {
            var result = _applicationService.ObterTelefonesPorNumero(numero);

            if (result is not null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("por-usuario/{usuarioId}")]
        [SwaggerOperation(Summary = "Lista telefones por ID de usuário", Description = "Retorna os telefones associados ao usuário informado.")]
        [SwaggerResponse(200, "Telefones encontrados", typeof(IEnumerable<TelefoneResponseDto>))]
        [SwaggerResponse(204, "Nenhum telefone encontrado")]
        public IActionResult GetByUsuarioId(long usuarioId)
        {
            var result = _applicationService.ObterTelefonesPorUsuarioId(usuarioId);

            if (result is not null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("por-tipo/{tipo}")]
        [SwaggerOperation(Summary = "Lista telefones por tipo", Description = "Retorna os telefones do tipo especificado.")]
        [SwaggerResponse(200, "Telefones encontrados", typeof(IEnumerable<TelefoneResponseDto>))]
        [SwaggerResponse(204, "Nenhum telefone encontrado")]
        public IActionResult GetByTipo(string tipo)
        {
            var result = _applicationService.ObterTelefonesPorTipo(tipo);

            if (result is not null && result.Any())
                return Ok(result);

            return NoContent();
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo telefone", Description = "Salva um novo registro de telefone.")]
        [SwaggerResponse(201, "Telefone salvo com sucesso", typeof(TelefoneResponseDto))]
        [SwaggerResponse(400, "Erro ao salvar o dado")]
        public IActionResult Post([FromBody] TelefoneRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosTelefone(entity);

                if (result is not null)
                    return CreatedAtAction(nameof(GetById), new { id = result.IdTelefone }, result);

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
        [SwaggerOperation(Summary = "Atualiza um telefone", Description = "Edita os dados de um telefone existente.")]
        [SwaggerResponse(200, "Telefone atualizado com sucesso", typeof(TelefoneResponseDto))]
        [SwaggerResponse(404, "Telefone não encontrado")]
        [SwaggerResponse(400, "Erro ao atualizar o dado")]
        public IActionResult Put(int id, [FromBody] TelefoneRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosTelefone(id, entity);

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
        [SwaggerOperation(Summary = "Deleta um telefone", Description = "Remove um telefone do sistema com base no ID.")]
        [SwaggerResponse(204, "Telefone deletado com sucesso")]
        [SwaggerResponse(404, "Telefone não encontrado")]
        [SwaggerResponse(400, "Erro ao deletar o dado")]
        public IActionResult Delete(int id)
        {
            var result = _applicationService.DeletarDadosTelefone(id);

            if (result is not null)
                return NoContent();

            return NotFound();
        }
    }
}
