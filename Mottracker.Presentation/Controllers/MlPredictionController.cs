using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Mottracker.Application.Dtos.MlPrediction;
using Mottracker.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Mottracker.Presentation.Controllers;

[Route("api/v{version:apiVersion}/ml/prediction")]
[ApiController]
[ApiVersion("1.0")]
public class MlPredictionController : ControllerBase
{
    private readonly IMlPredictionService _mlPredictionService;

    public MlPredictionController(IMlPredictionService mlPredictionService)
    {
        _mlPredictionService = mlPredictionService;
    }

    [HttpPost("moto-demand")]
    [Authorize]
    [EnableRateLimiting("rateLimitePolicy")]
    [SwaggerOperation(
        Summary = "Prevê demanda de motos disponíveis",
        Description = "Utiliza ML.NET para prever a quantidade de motos disponíveis em um pátio " +
                    "baseado no número total de motos e na data fornecida. " +
                    "O modelo considera padrões sazonais e dias da semana. " +
                    "Requer autenticação JWT."
    )]
    [SwaggerResponse(statusCode: 200, description: "Previsão realizada com sucesso", type: typeof(MotoDemandPredictionResponseDto))]
    [SwaggerResponse(statusCode: 400, description: "Dados de entrada inválidos")]
    [SwaggerResponse(statusCode: 401, description: "Não autorizado - token JWT inválido ou ausente")]
    [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
    public IActionResult PredictMotoDemand(
        [FromBody, SwaggerParameter("Dados para previsão de demanda", Required = true)] 
        MotoDemandPredictionRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var data = request.Data ?? DateTime.Now;
            var previsao = _mlPredictionService.PredictMotoDemand(request.TotalMotos, data);
            var percentual = (previsao / request.TotalMotos) * 100;

            var response = new MotoDemandPredictionResponseDto
            {
                TotalMotos = request.TotalMotos,
                Data = data,
                MotosDisponiveisPrevistas = (float)Math.Round(previsao, 2),
                PercentualDisponibilidade = (float)Math.Round(percentual, 2),
                Observacao = percentual > 80 
                    ? "Alta disponibilidade prevista" 
                    : percentual < 30 
                        ? "Baixa disponibilidade prevista - considere reposição" 
                        : "Disponibilidade moderada"
            };

            var hateaos = new
            {
                data = response,
                links = new
                {
                    self = Url.Action(nameof(PredictMotoDemand), "MlPrediction", null, Request.Scheme)
                }
            };

            return Ok(hateaos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Erro ao processar previsão", message = ex.Message });
        }
    }
}

