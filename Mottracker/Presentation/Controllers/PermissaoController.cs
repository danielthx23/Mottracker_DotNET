using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Permissao;
using Mottracker.Docs.Samples;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/v1/permissao")]
    [ApiController]
    public class PermissaoController : ControllerBase
    {
        private readonly IPermissaoUseCase _useCase;

        public PermissaoController(IPermissaoUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        [SwaggerOperation(
           Summary = "Lista permissões com paginação",
           Description = "Retorna uma lista paginada de permissões cadastradas no sistema. " +
                        "Este endpoint suporta paginação para otimizar a performance com grandes volumes de dados. " +
                        "As permissões são retornadas com informações completas e links HATEOAS para navegação."
        )]
        [SwaggerResponse(statusCode: 200, description: "Lista de permissões retornada com sucesso", type: typeof(IEnumerable<PermissaoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhuma permissão encontrada")]
        [SwaggerResponse(statusCode: 400, description: "Parâmetros de paginação inválidos")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        [SwaggerResponseExample(statusCode: 200, typeof(PermissaoResponseListSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(
            [FromQuery, SwaggerParameter("Número de registros a pular (padrão: 0)", Required = false)] int Deslocamento = 0, 
            [FromQuery, SwaggerParameter("Número de registros a retornar (padrão: 3, máximo: 100)", Required = false)] int RegistrosRetornado = 3)
        {
            var result = await _useCase.ObterTodosPermissoesAsync(Deslocamento, RegistrosRetornado);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Data.Select(p => new
                {
                    p.IdPermissao,
                    p.NomePermissao,
                    p.Descricao,
                    p.UsuarioPermissoes,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Permissao", new { id = p.IdPermissao }, Request.Scheme),
                        put = Url.Action(nameof(Put), "Permissao", new { id = p.IdPermissao }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Permissao", new { id = p.IdPermissao }, Request.Scheme),
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(Get), "Permissao", null, Request.Scheme),
                    create = Url.Action(nameof(Post), "Permissao", null, Request.Scheme),
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
            Summary = "Obtém permissão por ID",
            Description = "Retorna os dados completos de uma permissão específica baseada no ID fornecido. " +
                        "Inclui informações detalhadas da permissão e links HATEOAS para operações relacionadas."
        )]
        [SwaggerResponse(statusCode: 200, description: "Permissão encontrada com sucesso", type: typeof(PermissaoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "Permissão não encontrada para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> GetById(
            [FromRoute, SwaggerParameter("ID único da permissão", Required = true)] int id)
        {
            var result = await _useCase.ObterPermissaoPorIdAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value,
                links = new
                {
                    self = Url.Action(nameof(GetById), "Permissao", new { id }),
                    get = Url.Action(nameof(Get), "Permissao", null),
                    put = Url.Action(nameof(Put), "Permissao", new { id }),
                    delete = Url.Action(nameof(Delete), "Permissao", new { id }),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
        }

        [HttpGet("nome/{nome}")]
        [SwaggerOperation(
            Summary = "Obtém permissões por nome",
            Description = "Retorna uma lista paginada de permissões filtradas por nome específico. " +
                        "Útil para encontrar todas as permissões que contenham palavras-chave no nome."
        )]
        [SwaggerResponse(statusCode: 200, description: "Permissões encontradas com sucesso", type: typeof(IEnumerable<PermissaoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhuma permissão encontrada para o nome especificado")]
        [SwaggerResponse(statusCode: 400, description: "Nome é obrigatório e não pode estar vazio")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> GetByNome(
            [FromRoute, SwaggerParameter("Nome da permissão para busca", Required = true)] string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return BadRequest("O parâmetro 'nome' é obrigatório.");

            var result = await _useCase.ObterPermissaoPorNomeAsync(nome);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Select(p => new
                {
                    p.IdPermissao,
                    p.NomePermissao,
                    p.Descricao,
                    p.UsuarioPermissoes,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Permissao", new { id = p.IdPermissao }, Request.Scheme),
                        put = Url.Action(nameof(Put), "Permissao", new { id = p.IdPermissao }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Permissao", new { id = p.IdPermissao }, Request.Scheme),
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(GetByNome), "Permissao", new { nome }, Request.Scheme),
                    get = Url.Action(nameof(Get), "Permissao", null, Request.Scheme),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
        }

        [HttpGet("descricao/{descricao}")]
        [SwaggerOperation(
            Summary = "Obtém permissões por descrição",
            Description = "Retorna uma lista paginada de permissões filtradas por descrição específica. " +
                        "Útil para encontrar todas as permissões que contenham palavras-chave na descrição."
        )]
        [SwaggerResponse(statusCode: 200, description: "Permissões encontradas com sucesso", type: typeof(IEnumerable<PermissaoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhuma permissão encontrada para a descrição especificada")]
        [SwaggerResponse(statusCode: 400, description: "Descrição é obrigatória e não pode estar vazia")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> GetByDescricao(
            [FromRoute, SwaggerParameter("Descrição da permissão para busca", Required = true)] string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                return BadRequest("O parâmetro 'descricao' é obrigatório.");

            var result = await _useCase.ObterPermissaoPorDescricaoAsync(descricao);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Select(p => new
                {
                    p.IdPermissao,
                    p.NomePermissao,
                    p.Descricao,
                    p.UsuarioPermissoes,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Permissao", new { id = p.IdPermissao }, Request.Scheme),
                        put = Url.Action(nameof(Put), "Permissao", new { id = p.IdPermissao }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Permissao", new { id = p.IdPermissao }, Request.Scheme),
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(GetByDescricao), "Permissao", new { descricao }, Request.Scheme),
                    get = Url.Action(nameof(Get), "Permissao", null, Request.Scheme),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria nova permissão",
            Description = "Cria uma nova permissão no sistema com os dados fornecidos. " +
                        "Valida se todos os campos obrigatórios estão preenchidos e se não há duplicatas. " +
                        "Retorna os dados da permissão criada incluindo o ID gerado."
        )]
        [SwaggerRequestExample(typeof(PermissaoRequestDto), typeof(PermissaoRequestDtoSample))]
        [SwaggerResponse(statusCode: 201, description: "Permissão criada com sucesso", type: typeof(PermissaoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "Dados inválidos - campos obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível criar a permissão - dados inválidos ou duplicados")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> Post(
            [FromBody, SwaggerParameter("Dados da permissão a ser criada", Required = true)] PermissaoRequestDto entity)
        {
            var result = await _useCase.SalvarDadosPermissaoAsync(entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualiza permissão existente",
            Description = "Atualiza os dados de uma permissão existente baseada no ID fornecido. " +
                        "Valida se a permissão existe e se os novos dados são válidos. " +
                        "Retorna os dados atualizados da permissão."
        )]
        [SwaggerRequestExample(typeof(PermissaoRequestDto), typeof(PermissaoRequestDtoSample))]
        [SwaggerResponse(statusCode: 200, description: "Permissão atualizada com sucesso", type: typeof(PermissaoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido ou dados obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 404, description: "Permissão não encontrada para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível atualizar a permissão - dados inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> Put(
            [FromRoute, SwaggerParameter("ID único da permissão a ser atualizada", Required = true)] int id, 
            [FromBody, SwaggerParameter("Novos dados da permissão", Required = true)] PermissaoRequestDto entity)
        {
            var result = await _useCase.EditarDadosPermissaoAsync(id, entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remove permissão",
            Description = "Remove permanentemente uma permissão do sistema baseada no ID fornecido. " +
                        "Esta operação é irreversível e remove todos os dados associados à permissão."
        )]
        [SwaggerResponse(statusCode: 204, description: "Permissão removida com sucesso")]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "Permissão não encontrada para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível remover a permissão")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> Delete(
            [FromRoute, SwaggerParameter("ID único da permissão a ser removida", Required = true)] int id)
        {
            var result = await _useCase.DeletarDadosPermissaoAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, result.Value);
        }
    }
}
