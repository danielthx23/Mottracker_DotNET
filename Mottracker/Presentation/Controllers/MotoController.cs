using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Moto;
using Mottracker.Docs.Samples;
using Mottracker.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/v1/moto")]
    [ApiController]
    public class MotoController : ControllerBase
    {
        private readonly IMotoUseCase _useCase;

        public MotoController(IMotoUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        [SwaggerOperation(
           Summary = "Lista motos com paginação",
           Description = "Retorna uma lista paginada de motos cadastradas no sistema. " +
                        "Este endpoint suporta paginação para otimizar a performance com grandes volumes de dados. " +
                        "As motos são retornadas com informações completas e links HATEOAS para navegação."
        )]
        [SwaggerResponse(statusCode: 200, description: "Lista de motos retornada com sucesso", type: typeof(IEnumerable<MotoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhuma moto encontrada")]
        [SwaggerResponse(statusCode: 400, description: "Parâmetros de paginação inválidos")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        [SwaggerResponseExample(statusCode: 200, typeof(MotoResponseListSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(
            [FromQuery, SwaggerParameter("Número de registros a pular (padrão: 0)", Required = false)] int Deslocamento = 0, 
            [FromQuery, SwaggerParameter("Número de registros a retornar (padrão: 3, máximo: 100)", Required = false)] int RegistrosRetornado = 3)
        {
            var result = await _useCase.ObterTodasMotosAsync(Deslocamento, RegistrosRetornado);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Data?.Select(m => new
                {
                    m.IdMoto,
                    m.PlacaMoto,
                    m.ModeloMoto,
                    m.AnoMoto,
                    m.IdentificadorMoto,
                    m.QuilometragemMoto,
                    m.EstadoMoto,
                    m.CondicoesMoto,
                    m.MotoPatioOrigemId,
                    m.ContratoMoto,
                    m.MotoPatioAtual,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Moto", new { id = m.IdMoto }, Request.Scheme),
                        put = Url.Action(nameof(Put), "Moto", new { id = m.IdMoto }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Moto", new { id = m.IdMoto }, Request.Scheme),
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(Get), "Moto", null, Request.Scheme),
                    create = Url.Action(nameof(Post), "Moto", null, Request.Scheme),
                },
                pagina = new
                {
                    result.Value?.Deslocamento,
                    result.Value?.RegistrosRetornado,
                    result.Value?.TotalRegistros
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obtém moto por ID",
            Description = "Retorna os dados completos de uma moto específica baseada no ID fornecido. " +
                        "Inclui informações detalhadas da moto e links HATEOAS para operações relacionadas."
        )]
        [SwaggerResponse(statusCode: 200, description: "Moto encontrada com sucesso", type: typeof(MotoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "Moto não encontrada para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> GetById(
            [FromRoute, SwaggerParameter("ID único da moto", Required = true)] int id)
        {
            var result = await _useCase.ObterMotoPorIdAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value,
                links = new
                {
                    self = Url.Action(nameof(GetById), "Moto", new { id }),
                    get = Url.Action(nameof(Get), "Moto", null),
                    put = Url.Action(nameof(Put), "Moto", new { id }),
                    delete = Url.Action(nameof(Delete), "Moto", new { id }),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
        }

        [HttpGet("placa/{placa}")]
        [SwaggerOperation(
            Summary = "Obtém moto por placa",
            Description = "Retorna os dados completos de uma moto específica baseada na placa fornecida. " +
                        "Útil para busca rápida por placa da moto."
        )]
        [SwaggerResponse(statusCode: 200, description: "Moto encontrada com sucesso", type: typeof(MotoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "Placa é obrigatória e deve ser válida")]
        [SwaggerResponse(statusCode: 404, description: "Moto não encontrada para a placa fornecida")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> GetByPlaca(
            [FromRoute, SwaggerParameter("Placa da moto para busca", Required = true)] string placa)
        {
            var result = await _useCase.ObterMotoPorPlacaAsync(placa);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value,
                links = new
                {
                    self = Url.Action(nameof(GetByPlaca), "Moto", new { placa }, Request.Scheme),
                    get = Url.Action(nameof(Get), "Moto", null, Request.Scheme),
                    put = Url.Action(nameof(Put), "Moto", new { id = result.Value?.IdMoto }, Request.Scheme),
                    delete = Url.Action(nameof(Delete), "Moto", new { id = result.Value?.IdMoto }, Request.Scheme),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
        }

        [HttpGet("estado/{estado}")]
        [SwaggerOperation(
            Summary = "Obtém motos por estado",
            Description = "Retorna uma lista paginada de motos filtradas por estado específico. " +
                        "Útil para encontrar todas as motos com um determinado estado (Retirada, NoPatio, NoPatioErrado, NaoDevolvida)."
        )]
        [SwaggerResponse(statusCode: 200, description: "Motos encontradas com sucesso", type: typeof(IEnumerable<MotoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhuma moto encontrada para o estado especificado")]
        [SwaggerResponse(statusCode: 400, description: "Estado é obrigatório e deve ser válido")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> GetByEstado(
            [FromRoute, SwaggerParameter("Estado da moto (Retirada, NoPatio, NoPatioErrado, NaoDevolvida)", Required = true)] string estado)
        {
            if (!System.Enum.TryParse<Estados>(estado, true, out var estadoEnum))
                return BadRequest("Estado inválido.");

            var result = await _useCase.ObterMotosPorEstadoAsync(estadoEnum);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Data.Select(m => new
                {
                    m.IdMoto,
                    m.PlacaMoto,
                    m.ModeloMoto,
                    m.AnoMoto,
                    m.IdentificadorMoto,
                    m.QuilometragemMoto,
                    m.EstadoMoto,
                    m.CondicoesMoto,
                    m.MotoPatioOrigemId,
                    m.ContratoMoto,
                    m.MotoPatioAtual,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Moto", new { id = m.IdMoto }, Request.Scheme),
                        put = Url.Action(nameof(Put), "Moto", new { id = m.IdMoto }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Moto", new { id = m.IdMoto }, Request.Scheme),
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(GetByEstado), "Moto", new { estado }, Request.Scheme),
                    get = Url.Action(nameof(Get), "Moto", null, Request.Scheme),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
        }

        [HttpGet("contrato/{contratoId}")]
        [SwaggerOperation(
            Summary = "Obtém motos por contrato",
            Description = "Retorna uma lista paginada de motos filtradas por contrato específico. " +
                        "Útil para encontrar todas as motos associadas a um determinado contrato."
        )]
        [SwaggerResponse(statusCode: 200, description: "Motos encontradas com sucesso", type: typeof(IEnumerable<MotoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhuma moto encontrada para o contrato especificado")]
        [SwaggerResponse(statusCode: 400, description: "ID do contrato é obrigatório")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> GetByContratoId(
            [FromRoute, SwaggerParameter("ID único do contrato", Required = true)] long contratoId)
        {
            var result = await _useCase.ObterMotoPorContratoIdAsync(contratoId);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Select(m => new
                {
                    m.IdMoto,
                    m.PlacaMoto,
                    m.ModeloMoto,
                    m.AnoMoto,
                    m.IdentificadorMoto,
                    m.QuilometragemMoto,
                    m.EstadoMoto,
                    m.CondicoesMoto,
                    m.MotoPatioOrigemId,
                    m.ContratoMoto,
                    m.MotoPatioAtual,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Moto", new { id = m.IdMoto }, Request.Scheme),
                        put = Url.Action(nameof(Put), "Moto", new { id = m.IdMoto }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Moto", new { id = m.IdMoto }, Request.Scheme),
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(GetByContratoId), "Moto", new { contratoId }, Request.Scheme),
                    get = Url.Action(nameof(Get), "Moto", null, Request.Scheme),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria nova moto",
            Description = "Cria uma nova moto no sistema com os dados fornecidos. " +
                        "Valida se todos os campos obrigatórios estão preenchidos e se não há duplicatas. " +
                        "Retorna os dados da moto criada incluindo o ID gerado."
        )]
        [SwaggerRequestExample(typeof(MotoRequestDto), typeof(MotoRequestDtoSample))]
        [SwaggerResponse(statusCode: 201, description: "Moto criada com sucesso", type: typeof(MotoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "Dados inválidos - campos obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível criar a moto - dados inválidos ou duplicados")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> Post(
            [FromBody, SwaggerParameter("Dados da moto a ser criada", Required = true)] MotoRequestDto entity)
        {
            var result = await _useCase.SalvarDadosMotoAsync(entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualiza moto existente",
            Description = "Atualiza os dados de uma moto existente baseada no ID fornecido. " +
                        "Valida se a moto existe e se os novos dados são válidos. " +
                        "Retorna os dados atualizados da moto."
        )]
        [SwaggerRequestExample(typeof(MotoRequestDto), typeof(MotoRequestDtoSample))]
        [SwaggerResponse(statusCode: 200, description: "Moto atualizada com sucesso", type: typeof(MotoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido ou dados obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 404, description: "Moto não encontrada para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível atualizar a moto - dados inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> Put(
            [FromRoute, SwaggerParameter("ID único da moto a ser atualizada", Required = true)] int id, 
            [FromBody, SwaggerParameter("Novos dados da moto", Required = true)] MotoRequestDto entity)
        {
            var result = await _useCase.EditarDadosMotoAsync(id, entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remove moto",
            Description = "Remove permanentemente uma moto do sistema baseada no ID fornecido. " +
                        "Esta operação é irreversível e remove todos os dados associados à moto."
        )]
        [SwaggerResponse(statusCode: 204, description: "Moto removida com sucesso")]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "Moto não encontrada para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível remover a moto")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> Delete(
            [FromRoute, SwaggerParameter("ID único da moto a ser removida", Required = true)] int id)
        {
            var result = await _useCase.DeletarDadosMotoAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, result.Value);
        }
    }
}
