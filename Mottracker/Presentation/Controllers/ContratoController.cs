using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Contrato;
using Mottracker.Docs.Samples;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/v1/contrato")]
    [ApiController]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoUseCase _useCase;

        public ContratoController(IContratoUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        [SwaggerOperation(
           Summary = "Lista contratos com paginação",
           Description = "Retorna uma lista paginada de contratos cadastrados no sistema. " +
                        "Este endpoint suporta paginação para otimizar a performance com grandes volumes de dados. " +
                        "Os contratos são retornados com informações completas e links HATEOAS para navegação."
        )]
        [SwaggerResponse(statusCode: 200, description: "Lista de contratos retornada com sucesso", type: typeof(IEnumerable<ContratoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum contrato encontrado")]
        [SwaggerResponse(statusCode: 400, description: "Parâmetros de paginação inválidos")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        [SwaggerResponseExample(statusCode: 200, typeof(ContratoResponseListSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public async Task<IActionResult> Get(
            [FromQuery, SwaggerParameter("Número de registros a pular (padrão: 0)", Required = false)] int Deslocamento = 0, 
            [FromQuery, SwaggerParameter("Número de registros a retornar (padrão: 3, máximo: 100)", Required = false)] int RegistrosRetornado = 3)
        {
            var result = await _useCase.ObterTodosContratosAsync(Deslocamento, RegistrosRetornado);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Data?.Select(c => new
                {
                    c.IdContrato,
                    c.ClausulasContrato,
                    c.DataDeEntradaContrato,
                    c.HorarioDeDevolucaoContrato,
                    c.DataDeExpiracaoContrato,
                    c.RenovacaoAutomaticaContrato,
                    c.DataUltimaRenovacaoContrato,
                    c.NumeroRenovacoesContrato,
                    c.AtivoContrato,
                    c.ValorToralContrato,
                    c.QuantidadeParcelas,
                    c.UsuarioContrato,
                    c.MotoContrato,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Contrato", new { id = c.IdContrato }, Request.Scheme),
                        put = Url.Action(nameof(Put), "Contrato", new { id = c.IdContrato }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Contrato", new { id = c.IdContrato }, Request.Scheme),
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(Get), "Contrato", null, Request.Scheme),
                    create = Url.Action(nameof(Post), "Contrato", null, Request.Scheme),
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
            Summary = "Obtém contrato por ID",
            Description = "Retorna os dados completos de um contrato específico baseado no ID fornecido. " +
                        "Inclui informações detalhadas do contrato e links HATEOAS para operações relacionadas."
        )]
        [SwaggerResponse(statusCode: 200, description: "Contrato encontrado com sucesso", type: typeof(ContratoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "Contrato não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> GetById(
            [FromRoute, SwaggerParameter("ID único do contrato", Required = true)] int id)
        {
            var result = await _useCase.ObterContratoPorIdAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value,
                links = new
                {
                    self = Url.Action(nameof(GetById), "Contrato", new { id }),
                    get = Url.Action(nameof(Get), "Contrato", null),
                    put = Url.Action(nameof(Put), "Contrato", new { id }),
                    delete = Url.Action(nameof(Delete), "Contrato", new { id }),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
        }

        [HttpGet("ativo/{ativoContrato}")]
        [SwaggerOperation(
            Summary = "Obtém contratos por status ativo",
            Description = "Retorna uma lista paginada de contratos filtrados por status ativo. " +
                        "Útil para encontrar todos os contratos ativos ou inativos no sistema."
        )]
        [SwaggerResponse(statusCode: 200, description: "Contratos encontrados com sucesso", type: typeof(IEnumerable<ContratoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum contrato encontrado para o status especificado")]
        [SwaggerResponse(statusCode: 400, description: "Status é obrigatório")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> GetByAtivo(
            [FromRoute, SwaggerParameter("Status ativo do contrato (1 = Ativo, 0 = Inativo)", Required = true)] int ativoContrato)
        {
            var result = await _useCase.ObterPorAtivoContratoAsync(ativoContrato);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Select(c => new
                {
                    c.IdContrato,
                    c.ClausulasContrato,
                    c.DataDeEntradaContrato,
                    c.HorarioDeDevolucaoContrato,
                    c.DataDeExpiracaoContrato,
                    c.RenovacaoAutomaticaContrato,
                    c.DataUltimaRenovacaoContrato,
                    c.NumeroRenovacoesContrato,
                    c.AtivoContrato,
                    c.ValorToralContrato,
                    c.QuantidadeParcelas,
                    c.UsuarioContrato,
                    c.MotoContrato,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Contrato", new { id = c.IdContrato }, Request.Scheme),
                        put = Url.Action(nameof(Put), "Contrato", new { id = c.IdContrato }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Contrato", new { id = c.IdContrato }, Request.Scheme),
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(GetByAtivo), "Contrato", new { ativoContrato }, Request.Scheme),
                    get = Url.Action(nameof(Get), "Contrato", null, Request.Scheme),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
        }

        [HttpGet("usuario/{usuarioId}")]
        [SwaggerOperation(
            Summary = "Obtém contratos por usuário",
            Description = "Retorna uma lista paginada de contratos filtrados por usuário específico. " +
                        "Útil para encontrar todos os contratos de um determinado usuário."
        )]
        [SwaggerResponse(statusCode: 200, description: "Contratos encontrados com sucesso", type: typeof(IEnumerable<ContratoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum contrato encontrado para o usuário especificado")]
        [SwaggerResponse(statusCode: 400, description: "ID do usuário é obrigatório")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> GetByUsuarioId(
            [FromRoute, SwaggerParameter("ID único do usuário", Required = true)] long usuarioId)
        {
            var result = await _useCase.ObterPorUsuarioIdAsync(usuarioId);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Select(c => new
                {
                    c.IdContrato,
                    c.ClausulasContrato,
                    c.DataDeEntradaContrato,
                    c.HorarioDeDevolucaoContrato,
                    c.DataDeExpiracaoContrato,
                    c.RenovacaoAutomaticaContrato,
                    c.DataUltimaRenovacaoContrato,
                    c.NumeroRenovacoesContrato,
                    c.AtivoContrato,
                    c.ValorToralContrato,
                    c.QuantidadeParcelas,
                    c.UsuarioContrato,
                    c.MotoContrato,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Contrato", new { id = c.IdContrato }, Request.Scheme),
                        put = Url.Action(nameof(Put), "Contrato", new { id = c.IdContrato }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Contrato", new { id = c.IdContrato }, Request.Scheme),
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(GetByUsuarioId), "Contrato", new { usuarioId }, Request.Scheme),
                    get = Url.Action(nameof(Get), "Contrato", null, Request.Scheme),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
        }

        [HttpGet("moto/{motoId}")]
        [SwaggerOperation(
            Summary = "Obtém contratos por moto",
            Description = "Retorna uma lista paginada de contratos filtrados por moto específica. " +
                        "Útil para encontrar todos os contratos relacionados a uma determinada moto."
        )]
        [SwaggerResponse(statusCode: 200, description: "Contratos encontrados com sucesso", type: typeof(IEnumerable<ContratoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum contrato encontrado para a moto especificada")]
        [SwaggerResponse(statusCode: 400, description: "ID da moto é obrigatório")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> GetByMotoId(
            [FromRoute, SwaggerParameter("ID único da moto", Required = true)] long motoId)
        {
            var result = await _useCase.ObterPorMotoIdAsync(motoId);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Select(c => new
                {
                    c.IdContrato,
                    c.ClausulasContrato,
                    c.DataDeEntradaContrato,
                    c.HorarioDeDevolucaoContrato,
                    c.DataDeExpiracaoContrato,
                    c.RenovacaoAutomaticaContrato,
                    c.DataUltimaRenovacaoContrato,
                    c.NumeroRenovacoesContrato,
                    c.AtivoContrato,
                    c.ValorToralContrato,
                    c.QuantidadeParcelas,
                    c.UsuarioContrato,
                    c.MotoContrato,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Contrato", new { id = c.IdContrato }, Request.Scheme),
                        put = Url.Action(nameof(Put), "Contrato", new { id = c.IdContrato }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Contrato", new { id = c.IdContrato }, Request.Scheme),
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(GetByMotoId), "Contrato", new { motoId }, Request.Scheme),
                    get = Url.Action(nameof(Get), "Contrato", null, Request.Scheme),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
        }

        [HttpGet("nao-expirados")]
        [SwaggerOperation(
            Summary = "Obtém contratos não expirados",
            Description = "Retorna uma lista paginada de contratos que ainda não expiraram. " +
                        "Útil para encontrar todos os contratos ativos no sistema."
        )]
        [SwaggerResponse(statusCode: 200, description: "Contratos encontrados com sucesso", type: typeof(IEnumerable<ContratoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum contrato não expirado encontrado")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> GetContratosNaoExpirados()
        {
            var dataAtual = DateTime.UtcNow;
            var result = await _useCase.ObterContratosNaoExpiradosAsync(dataAtual);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Select(c => new
                {
                    c.IdContrato,
                    c.ClausulasContrato,
                    c.DataDeEntradaContrato,
                    c.HorarioDeDevolucaoContrato,
                    c.DataDeExpiracaoContrato,
                    c.RenovacaoAutomaticaContrato,
                    c.DataUltimaRenovacaoContrato,
                    c.NumeroRenovacoesContrato,
                    c.AtivoContrato,
                    c.ValorToralContrato,
                    c.QuantidadeParcelas,
                    c.UsuarioContrato,
                    c.MotoContrato,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Contrato", new { id = c.IdContrato }, Request.Scheme),
                        put = Url.Action(nameof(Put), "Contrato", new { id = c.IdContrato }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Contrato", new { id = c.IdContrato }, Request.Scheme),
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(GetContratosNaoExpirados), "Contrato", null, Request.Scheme),
                    get = Url.Action(nameof(Get), "Contrato", null, Request.Scheme),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
        }

        [HttpGet("renovacao-automatica/{renovacao}")]
        [SwaggerOperation(
            Summary = "Obtém contratos por renovação automática",
            Description = "Retorna uma lista paginada de contratos filtrados por renovação automática. " +
                        "Útil para encontrar contratos com ou sem renovação automática."
        )]
        [SwaggerResponse(statusCode: 200, description: "Contratos encontrados com sucesso", type: typeof(IEnumerable<ContratoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum contrato encontrado para o tipo de renovação especificado")]
        [SwaggerResponse(statusCode: 400, description: "Tipo de renovação é obrigatório")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> GetByRenovacaoAutomatica(
            [FromRoute, SwaggerParameter("Tipo de renovação automática (1 = Sim, 0 = Não)", Required = true)] int renovacao)
        {
            var result = await _useCase.ObterPorRenovacaoAutomaticaAsync(renovacao);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Select(c => new
                {
                    c.IdContrato,
                    c.ClausulasContrato,
                    c.DataDeEntradaContrato,
                    c.HorarioDeDevolucaoContrato,
                    c.DataDeExpiracaoContrato,
                    c.RenovacaoAutomaticaContrato,
                    c.DataUltimaRenovacaoContrato,
                    c.NumeroRenovacoesContrato,
                    c.AtivoContrato,
                    c.ValorToralContrato,
                    c.QuantidadeParcelas,
                    c.UsuarioContrato,
                    c.MotoContrato,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Contrato", new { id = c.IdContrato }, Request.Scheme),
                        put = Url.Action(nameof(Put), "Contrato", new { id = c.IdContrato }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Contrato", new { id = c.IdContrato }, Request.Scheme),
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(GetByRenovacaoAutomatica), "Contrato", new { renovacao }, Request.Scheme),
                    get = Url.Action(nameof(Get), "Contrato", null, Request.Scheme),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
        }

        [HttpGet("por-data-entrada")]
        [SwaggerOperation(
            Summary = "Obtém contratos por período de entrada",
            Description = "Retorna uma lista paginada de contratos filtrados por período de entrada. " +
                        "Útil para encontrar contratos criados em um período específico."
        )]
        [SwaggerResponse(statusCode: 200, description: "Contratos encontrados com sucesso", type: typeof(IEnumerable<ContratoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum contrato encontrado para o período especificado")]
        [SwaggerResponse(statusCode: 400, description: "Data de início maior que a data de fim")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> GetByDataEntradaEntre(
            [FromQuery, SwaggerParameter("Data de início do período", Required = true)] DateTime dataInicio, 
            [FromQuery, SwaggerParameter("Data de fim do período", Required = true)] DateTime dataFim)
        {
            if (dataFim < dataInicio)
                return BadRequest("A data final não pode ser menor que a data inicial.");

            var result = await _useCase.ObterPorDataEntradaEntreAsync(dataInicio, dataFim);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            var hateaos = new
            {
                data = result.Value?.Select(c => new
                {
                    c.IdContrato,
                    c.ClausulasContrato,
                    c.DataDeEntradaContrato,
                    c.HorarioDeDevolucaoContrato,
                    c.DataDeExpiracaoContrato,
                    c.RenovacaoAutomaticaContrato,
                    c.DataUltimaRenovacaoContrato,
                    c.NumeroRenovacoesContrato,
                    c.AtivoContrato,
                    c.ValorToralContrato,
                    c.QuantidadeParcelas,
                    c.UsuarioContrato,
                    c.MotoContrato,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Contrato", new { id = c.IdContrato }, Request.Scheme),
                        put = Url.Action(nameof(Put), "Contrato", new { id = c.IdContrato }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "Contrato", new { id = c.IdContrato }, Request.Scheme),
                    }
                }),
                links = new
                {
                    self = Url.Action(nameof(GetByDataEntradaEntre), "Contrato", new { dataInicio, dataFim }, Request.Scheme),
                    get = Url.Action(nameof(Get), "Contrato", null, Request.Scheme),
                }
            };

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, hateaos);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria novo contrato",
            Description = "Cria um novo contrato no sistema com os dados fornecidos. " +
                        "Valida se todos os campos obrigatórios estão preenchidos e se não há duplicatas. " +
                        "Retorna os dados do contrato criado incluindo o ID gerado."
        )]
        [SwaggerRequestExample(typeof(ContratoRequestDto), typeof(ContratoRequestDtoSample))]
        [SwaggerResponse(statusCode: 201, description: "Contrato criado com sucesso", type: typeof(ContratoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "Dados inválidos - campos obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível criar o contrato - dados inválidos ou duplicados")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> Post(
            [FromBody, SwaggerParameter("Dados do contrato a ser criado", Required = true)] ContratoRequestDto entity)
        {
            var result = await _useCase.SalvarContratoAsync(entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualiza contrato existente",
            Description = "Atualiza os dados de um contrato existente baseado no ID fornecido. " +
                        "Valida se o contrato existe e se os novos dados são válidos. " +
                        "Retorna os dados atualizados do contrato."
        )]
        [SwaggerRequestExample(typeof(ContratoRequestDto), typeof(ContratoRequestDtoSample))]
        [SwaggerResponse(statusCode: 200, description: "Contrato atualizado com sucesso", type: typeof(ContratoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido ou dados obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 404, description: "Contrato não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível atualizar o contrato - dados inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> Put(
            [FromRoute, SwaggerParameter("ID único do contrato a ser atualizado", Required = true)] int id, 
            [FromBody, SwaggerParameter("Novos dados do contrato", Required = true)] ContratoRequestDto entity)
        {
            var result = await _useCase.EditarContratoAsync(id, entity);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, result.Value);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remove contrato",
            Description = "Remove permanentemente um contrato do sistema baseado no ID fornecido. " +
                        "Esta operação é irreversível e remove todos os dados associados ao contrato."
        )]
        [SwaggerResponse(statusCode: 204, description: "Contrato removido com sucesso")]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "Contrato não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível remover o contrato")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public async Task<IActionResult> Delete(
            [FromRoute, SwaggerParameter("ID único do contrato a ser removido", Required = true)] int id)
        {
            var result = await _useCase.DeletarContratoAsync(id);

            if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

            if (result.StatusCode == 204)
                return NoContent();
            return StatusCode(result.StatusCode, result.Value);
        }
    }
}
