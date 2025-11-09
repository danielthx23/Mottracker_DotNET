using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.LayoutPatio;
using Mottracker.Docs.Samples;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/v1/layout-patio")]
    [ApiController]
    public class LayoutPatioController : ControllerBase
    {
        private readonly ILayoutPatioUseCase _useCase;

        public LayoutPatioController(ILayoutPatioUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        [SwaggerOperation(
           Summary = "Lista layouts de pátio com paginação",
           Description = "Retorna uma lista paginada de layouts de pátio cadastrados no sistema. " +
                        "Este endpoint suporta paginação para otimizar a performance com grandes volumes de dados. " +
                        "Os layouts são retornados com informações completas e links HATEOAS para navegação."
        )]
        [SwaggerResponse(statusCode: 200, description: "Lista de layouts retornada com sucesso", type: typeof(IEnumerable<LayoutPatioResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum layout encontrado")]
        [SwaggerResponse(statusCode: 400, description: "Parâmetros de paginação inválidos")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        [SwaggerResponseExample(statusCode: 200, typeof(LayoutPatioResponseListSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(
            [FromQuery, SwaggerParameter("Número de registros a pular (padrão: 0)", Required = false)] int Deslocamento = 0, 
            [FromQuery, SwaggerParameter("Número de registros a retornar (padrão: 3, máximo: 100)", Required = false)] int RegistrosRetornado = 3)
        {
            var result = await _useCase.ObterTodosLayoutsPatiosAsync(Deslocamento, RegistrosRetornado);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Data?.Select(l => new
                {
                    l.IdLayoutPatio,
                    l.Descricao,
                    l.DataCriacao,
                    l.Largura,
                    l.Comprimento,
                    l.Altura,
                    l.PatioLayoutPatio,
                    l.QrCodesLayoutPatio,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "LayoutPatio", new { id = l.IdLayoutPatio }, Request.Scheme),
                        put = Url.Action(nameof(Put), "LayoutPatio", new { id = l.IdLayoutPatio }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "LayoutPatio", new { id = l.IdLayoutPatio }, Request.Scheme),
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(Get), "LayoutPatio", null, Request.Scheme),
                    create = Url.Action(nameof(Post), "LayoutPatio", null, Request.Scheme),
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
            Summary = "Obtém layout de pátio por ID",
            Description = "Retorna os dados completos de um layout de pátio específico baseado no ID fornecido. " +
                        "Inclui informações detalhadas do layout e links HATEOAS para operações relacionadas."
        )]
        [SwaggerResponse(statusCode: 200, description: "Layout encontrado com sucesso", type: typeof(LayoutPatioResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "Layout não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> GetById(
            [FromRoute, SwaggerParameter("ID único do layout de pátio", Required = true)] int id)
        {
            var result = await _useCase.ObterLayoutPatioPorIdAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value,
                links = new
                {
                    self = Url.Action(nameof(GetById), "LayoutPatio", new { id }),
                    get = Url.Action(nameof(Get), "LayoutPatio", null),
                    put = Url.Action(nameof(Put), "LayoutPatio", new { id }),
                    delete = Url.Action(nameof(Delete), "LayoutPatio", new { id }),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
        }

        [HttpGet("patio/{patioId}")]
        [SwaggerOperation(
            Summary = "Obtém layouts por pátio",
            Description = "Retorna uma lista paginada de layouts filtrados por pátio específico. " +
                        "Útil para encontrar todos os layouts de um determinado pátio."
        )]
        [SwaggerResponse(statusCode: 200, description: "Layouts encontrados com sucesso", type: typeof(IEnumerable<LayoutPatioResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum layout encontrado para o pátio especificado")]
        [SwaggerResponse(statusCode: 400, description: "ID do pátio é obrigatório e deve ser maior que zero")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> GetByPatioId(
            [FromRoute, SwaggerParameter("ID único do pátio", Required = true)] int patioId)
        {
            if (patioId <= 0)
                return BadRequest("O parâmetro patioId deve ser maior que zero.");

            var result = await _useCase.ObterLayoutPatioPorPatioIdAsync(patioId);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Select(l => new
                {
                    l.IdLayoutPatio,
                    l.Descricao,
                    l.DataCriacao,
                    l.Largura,
                    l.Comprimento,
                    l.Altura,
                    l.PatioLayoutPatio,
                    l.QrCodesLayoutPatio,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "LayoutPatio", new { id = l.IdLayoutPatio }, Request.Scheme),
                        put = Url.Action(nameof(Put), "LayoutPatio", new { id = l.IdLayoutPatio }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "LayoutPatio", new { id = l.IdLayoutPatio }, Request.Scheme),
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(GetByPatioId), "LayoutPatio", new { patioId }, Request.Scheme),
                    get = Url.Action(nameof(Get), "LayoutPatio", null, Request.Scheme),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
        }

        [HttpGet("data-criacao")]
        [SwaggerOperation(
            Summary = "Obtém layouts por data de criação",
            Description = "Retorna uma lista de layouts filtrados por data de criação específica. " +
                        "Útil para encontrar layouts criados em uma data específica."
        )]
        [SwaggerResponse(statusCode: 200, description: "Layouts encontrados com sucesso", type: typeof(IEnumerable<LayoutPatioResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum layout encontrado para a data especificada")]
        [SwaggerResponse(statusCode: 400, description: "Data é obrigatória")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> GetByDataCriacao(
            [FromQuery, SwaggerParameter("Data de criação", Required = true)] DateTime dataCriacao)
        {
            if (dataCriacao == default)
                return BadRequest("O parâmetro dataCriacao é obrigatório.");

            var result = await _useCase.ObterLayoutPatioPorDataCriacaoAsync(dataCriacao);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Select(l => new
                {
                    l.IdLayoutPatio,
                    l.Descricao,
                    l.DataCriacao,
                    l.Largura,
                    l.Comprimento,
                    l.Altura,
                    l.PatioLayoutPatio,
                    l.QrCodesLayoutPatio,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "LayoutPatio", new { id = l.IdLayoutPatio }, Request.Scheme),
                        put = Url.Action(nameof(Put), "LayoutPatio", new { id = l.IdLayoutPatio }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "LayoutPatio", new { id = l.IdLayoutPatio }, Request.Scheme),
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(GetByDataCriacao), "LayoutPatio", new { dataCriacao }, Request.Scheme),
                    get = Url.Action(nameof(Get), "LayoutPatio", null, Request.Scheme),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria novo layout de pátio",
            Description = "Cria um novo layout de pátio no sistema com os dados fornecidos. " +
                        "Valida se todos os campos obrigatórios estão preenchidos e se não há duplicatas. " +
                        "Retorna os dados do layout criado incluindo o ID gerado."
        )]
        [SwaggerRequestExample(typeof(LayoutPatioRequestDto), typeof(LayoutPatioRequestDtoSample))]
        [SwaggerResponse(statusCode: 201, description: "Layout criado com sucesso", type: typeof(LayoutPatioResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "Dados inválidos - campos obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível criar o layout - dados inválidos ou duplicados")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> Post(
            [FromBody, SwaggerParameter("Dados do layout de pátio a ser criado", Required = true)] LayoutPatioRequestDto entity)
        {
            var result = await _useCase.SalvarDadosLayoutPatioAsync(entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualiza layout de pátio existente",
            Description = "Atualiza os dados de um layout de pátio existente baseado no ID fornecido. " +
                        "Valida se o layout existe e se os novos dados são válidos. " +
                        "Retorna os dados atualizados do layout."
        )]
        [SwaggerRequestExample(typeof(LayoutPatioRequestDto), typeof(LayoutPatioRequestDtoSample))]
        [SwaggerResponse(statusCode: 200, description: "Layout atualizado com sucesso", type: typeof(LayoutPatioResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido ou dados obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 404, description: "Layout não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível atualizar o layout - dados inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> Put(
            [FromRoute, SwaggerParameter("ID único do layout de pátio a ser atualizado", Required = true)] int id, 
            [FromBody, SwaggerParameter("Novos dados do layout de pátio", Required = true)] LayoutPatioRequestDto entity)
        {
            var result = await _useCase.EditarDadosLayoutPatioAsync(id, entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remove layout de pátio",
            Description = "Remove permanentemente um layout de pátio do sistema baseado no ID fornecido. " +
                        "Esta operação é irreversível e remove todos os dados associados ao layout."
        )]
        [SwaggerResponse(statusCode: 204, description: "Layout removido com sucesso")]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "Layout não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível remover o layout")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> Delete(
            [FromRoute, SwaggerParameter("ID único do layout de pátio a ser removido", Required = true)] int id)
        {
            var result = await _useCase.DeletarDadosLayoutPatioAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, result.Value);
        }
    }
}
