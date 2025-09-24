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
        private readonly IMotoApplicationService _applicationService;

        public MotoController(IMotoApplicationService applicationService)
        {
            _applicationService = applicationService;
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
        public IActionResult Get(
            [FromQuery, SwaggerParameter("Número de registros a pular (padrão: 0)", Required = false)] int Deslocamento = 0, 
            [FromQuery, SwaggerParameter("Número de registros a retornar (padrão: 3, máximo: 100)", Required = false)] int RegistrosRetornado = 3)
        {
            var result = _applicationService.ObterTodasMotos();

            if (result is not null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(m => new
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
                        Deslocamento,
                        RegistrosRetornado,
                        TotalRegistros = result.Count()
                    }
                };

                return Ok(hateaos);
            }

            return NoContent();
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
        public IActionResult GetById(
            [FromRoute, SwaggerParameter("ID único da moto", Required = true)] int id)
        {
            var result = _applicationService.ObterMotoPorId(id);

            if (result is not null)
            {
                var hateaos = new
                {
                    data = result,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Moto", new { id }),
                        get = Url.Action(nameof(Get), "Moto", null),
                        put = Url.Action(nameof(Put), "Moto", new { id }),
                        delete = Url.Action(nameof(Delete), "Moto", new { id }),
                    }
                };

                return Ok(hateaos);
            }

            return NotFound();
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
        public IActionResult GetByPlaca(
            [FromRoute, SwaggerParameter("Placa da moto para busca", Required = true)] string placa)
        {
            var result = _applicationService.ObterMotoPorPlaca(placa);

            if (result is not null)
            {
                var hateaos = new
                {
                    data = result,
                    links = new
                    {
                        self = Url.Action(nameof(GetByPlaca), "Moto", new { placa }, Request.Scheme),
                        get = Url.Action(nameof(Get), "Moto", null, Request.Scheme),
                        put = Url.Action(nameof(Put), "Moto", new { id = result.IdMoto }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Moto", new { id = result.IdMoto }, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NotFound();
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
        public IActionResult GetByEstado(
            [FromRoute, SwaggerParameter("Estado da moto (Retirada, NoPatio, NoPatioErrado, NaoDevolvida)", Required = true)] string estado)
        {
            if (!System.Enum.TryParse<Estados>(estado, true, out var estadoEnum))
                return BadRequest("Estado inválido.");

            var result = _applicationService.ObterMotosPorEstado(estadoEnum);

            if (result is not null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(m => new
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

                return Ok(hateaos);
            }

            return NoContent();
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
        public IActionResult GetByContratoId(
            [FromRoute, SwaggerParameter("ID único do contrato", Required = true)] long contratoId)
        {
            var result = _applicationService.ObterMotosPorContratoId(contratoId);

            if (result is not null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(m => new
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

                return Ok(hateaos);
            }

            return NoContent();
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
        public IActionResult Post(
            [FromBody, SwaggerParameter("Dados da moto a ser criada", Required = true)] MotoRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosMoto(entity);

                if (result is not null)
                    return CreatedAtAction(nameof(GetById), new { id = result.IdMoto }, result);

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
        public IActionResult Put(
            [FromRoute, SwaggerParameter("ID único da moto a ser atualizada", Required = true)] int id, 
            [FromBody, SwaggerParameter("Novos dados da moto", Required = true)] MotoRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosMoto(id, entity);

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
        public IActionResult Delete(
            [FromRoute, SwaggerParameter("ID único da moto a ser removida", Required = true)] int id)
        {
            var result = _applicationService.DeletarDadosMoto(id);

            if (result is not null)
                return NoContent();

            return NotFound();
        }
    }
}
