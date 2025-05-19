using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.QrCodePonto;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QrCodePontoController : ControllerBase
    {
        private readonly IQrCodePontoApplicationService _applicationService;

        public QrCodePontoController(IQrCodePontoApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos os QR Codes de ponto", Description = "Retorna todos os registros de QR Code de ponto cadastrados.")]
        [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<QrCodePontoResponseDto>))]
        [SwaggerResponse(204, "Nenhum QR Code encontrado")]
        [SwaggerResponse(400, "Erro ao obter os dados")]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodosQrCodePontos();

            if (result is not null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém QR Code de ponto por ID", Description = "Retorna os dados de um QR Code de ponto específico.")]
        [SwaggerResponse(200, "QR Code retornado com sucesso", typeof(QrCodePontoResponseDto))]
        [SwaggerResponse(404, "QR Code não encontrado")]
        [SwaggerResponse(400, "Erro ao obter o dado")]
        public IActionResult GetById(int id)
        {
            var result = _applicationService.ObterQrCodePontoPorId(id);

            if (result is not null)
                return Ok(result);

            return NotFound();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo QR Code de ponto", Description = "Salva um novo registro de QR Code de ponto.")]
        [SwaggerResponse(201, "QR Code salvo com sucesso", typeof(QrCodePontoResponseDto))]
        [SwaggerResponse(400, "Erro ao salvar o dado")]
        public IActionResult Post([FromBody] QrCodePontoRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosQrCodePonto(entity);

                if (result is not null)
                    return CreatedAtAction(nameof(GetById), new { id = result.IdQrCodePonto }, result);

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
        [SwaggerOperation(Summary = "Atualiza um QR Code de ponto", Description = "Edita os dados de um QR Code de ponto existente.")]
        [SwaggerResponse(200, "QR Code atualizado com sucesso", typeof(QrCodePontoResponseDto))]
        [SwaggerResponse(404, "QR Code não encontrado")]
        [SwaggerResponse(400, "Erro ao atualizar o dado")]
        public IActionResult Put(int id, [FromBody] QrCodePontoRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosQrCodePonto(id, entity);

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
        [SwaggerOperation(Summary = "Deleta um QR Code de ponto", Description = "Remove um QR Code de ponto do sistema com base no ID.")]
        [SwaggerResponse(204, "QR Code deletado com sucesso")]
        [SwaggerResponse(404, "QR Code não encontrado")]
        [SwaggerResponse(400, "Erro ao deletar o dado")]
        public IActionResult Delete(int id)
        {
            var result = _applicationService.DeletarDadosQrCodePonto(id);

            if (result is not null)
                return NoContent();

            return NotFound();
        }
    }
}






















































































