using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.QrCodePonto;
using Mottracker.Docs.Samples;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/v1/qr-code-ponto")]
    [ApiController]
    public class QrCodePontoController : ControllerBase
    {
        private readonly IQrCodePontoApplicationService _applicationService;

        public QrCodePontoController(IQrCodePontoApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(
           Summary = "Lista QR Codes de ponto com paginação",
           Description = "Retorna uma lista paginada de QR Codes de ponto cadastrados no sistema. " +
                        "Este endpoint suporta paginação para otimizar a performance com grandes volumes de dados. " +
                        "Os QR Codes são retornados com informações completas e links HATEOAS para navegação."
        )]
        [SwaggerResponse(statusCode: 200, description: "Lista de QR Codes retornada com sucesso", type: typeof(IEnumerable<QrCodePontoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum QR Code encontrado")]
        [SwaggerResponse(statusCode: 400, description: "Parâmetros de paginação inválidos")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        [SwaggerResponseExample(statusCode: 200, typeof(QrCodePontoResponseListSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public IActionResult Get(
            [FromQuery, SwaggerParameter("Número de registros a pular (padrão: 0)", Required = false)] int Deslocamento = 0, 
            [FromQuery, SwaggerParameter("Número de registros a retornar (padrão: 3, máximo: 100)", Required = false)] int RegistrosRetornado = 3)
        {
            var result = _applicationService.ObterTodosQrCodePontos();

            if (result is not null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(q => new
                    {
                        q.IdQrCodePonto,
                        q.IdentificadorQrCodePonto,
                        q.PosXQrCodePonto,
                        q.PosYQrCodePonto,
                        q.LayoutPatioQrCodePonto,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "QrCodePonto", new { id = q.IdQrCodePonto }, Request.Scheme),
                            put = Url.Action(nameof(Put), "QrCodePonto", new { id = q.IdQrCodePonto }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "QrCodePonto", new { id = q.IdQrCodePonto }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(Get), "QrCodePonto", null, Request.Scheme),
                        create = Url.Action(nameof(Post), "QrCodePonto", null, Request.Scheme),
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
            Summary = "Obtém QR Code de ponto por ID",
            Description = "Retorna os dados completos de um QR Code de ponto específico baseado no ID fornecido. " +
                        "Inclui informações detalhadas do QR Code e links HATEOAS para operações relacionadas."
        )]
        [SwaggerResponse(statusCode: 200, description: "QR Code encontrado com sucesso", type: typeof(QrCodePontoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "QR Code não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetById(
            [FromRoute, SwaggerParameter("ID único do QR Code de ponto", Required = true)] int id)
        {
            var result = _applicationService.ObterQrCodePontoPorId(id);

            if (result is not null)
            {
                var hateaos = new
                {
                    data = result,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "QrCodePonto", new { id }),
                        get = Url.Action(nameof(Get), "QrCodePonto", null),
                        put = Url.Action(nameof(Put), "QrCodePonto", new { id }),
                        delete = Url.Action(nameof(Delete), "QrCodePonto", new { id }),
                    }
                };

                return Ok(hateaos);
            }

            return NotFound();
        }

        [HttpGet("identificador/{identificador}")]
        [SwaggerOperation(
            Summary = "Obtém QR Code de ponto por identificador",
            Description = "Retorna os dados completos de um QR Code de ponto específico baseado no identificador fornecido. " +
                        "Útil para busca rápida por identificador único."
        )]
        [SwaggerResponse(statusCode: 200, description: "QR Code encontrado com sucesso", type: typeof(QrCodePontoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "Identificador é obrigatório e não pode estar vazio")]
        [SwaggerResponse(statusCode: 404, description: "QR Code não encontrado para o identificador fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByIdentificador(
            [FromRoute, SwaggerParameter("Identificador único do QR Code", Required = true)] string identificador)
        {
            var result = _applicationService.ObterQrCodePontoPorIdentificador(identificador);

            if (result is not null)
            {
                var hateaos = new
                {
                    data = result,
                    links = new
                    {
                        self = Url.Action(nameof(GetByIdentificador), "QrCodePonto", new { identificador }, Request.Scheme),
                        get = Url.Action(nameof(Get), "QrCodePonto", null, Request.Scheme),
                        put = Url.Action(nameof(Put), "QrCodePonto", new { id = result.IdQrCodePonto }, Request.Scheme),
                        delete = Url.Action(nameof(Delete), "QrCodePonto", new { id = result.IdQrCodePonto }, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NotFound();
        }

        [HttpGet("layout-patio/{layoutPatioId}")]
        [SwaggerOperation(
            Summary = "Obtém QR Codes por layout de pátio",
            Description = "Retorna uma lista paginada de QR Codes filtrados por layout de pátio específico. " +
                        "Útil para encontrar todos os QR Codes de um determinado layout de pátio."
        )]
        [SwaggerResponse(statusCode: 200, description: "QR Codes encontrados com sucesso", type: typeof(IEnumerable<QrCodePontoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum QR Code encontrado para o layout especificado")]
        [SwaggerResponse(statusCode: 400, description: "ID do layout de pátio é obrigatório")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByLayoutPatioId(
            [FromRoute, SwaggerParameter("ID único do layout de pátio", Required = true)] long layoutPatioId)
        {
            var result = _applicationService.ObterQrCodePontosPorLayoutPatioId(layoutPatioId);

            if (result is not null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(q => new
                    {
                        q.IdQrCodePonto,
                        q.IdentificadorQrCodePonto,
                        q.PosXQrCodePonto,
                        q.PosYQrCodePonto,
                        q.LayoutPatioQrCodePonto,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "QrCodePonto", new { id = q.IdQrCodePonto }, Request.Scheme),
                            put = Url.Action(nameof(Put), "QrCodePonto", new { id = q.IdQrCodePonto }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "QrCodePonto", new { id = q.IdQrCodePonto }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(GetByLayoutPatioId), "QrCodePonto", new { layoutPatioId }, Request.Scheme),
                        get = Url.Action(nameof(Get), "QrCodePonto", null, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NoContent();
        }

        [HttpGet("posicao-x")]
        [SwaggerOperation(
            Summary = "Obtém QR Codes por faixa de posição X",
            Description = "Retorna uma lista paginada de QR Codes filtrados por faixa de posição X. " +
                        "Útil para encontrar QR Codes em uma área específica do layout."
        )]
        [SwaggerResponse(statusCode: 200, description: "QR Codes encontrados com sucesso", type: typeof(IEnumerable<QrCodePontoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum QR Code encontrado para a faixa especificada")]
        [SwaggerResponse(statusCode: 400, description: "Valores de posição são obrigatórios")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByPosXRange(
            [FromQuery, SwaggerParameter("Posição X inicial", Required = true)] float posXInicial, 
            [FromQuery, SwaggerParameter("Posição X final", Required = true)] float posXFinal)
        {
            var result = _applicationService.ObterQrCodePontosPorPosicaoXEntre(posXInicial, posXFinal);

            if (result is not null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(q => new
                    {
                        q.IdQrCodePonto,
                        q.IdentificadorQrCodePonto,
                        q.PosXQrCodePonto,
                        q.PosYQrCodePonto,
                        q.LayoutPatioQrCodePonto,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "QrCodePonto", new { id = q.IdQrCodePonto }, Request.Scheme),
                            put = Url.Action(nameof(Put), "QrCodePonto", new { id = q.IdQrCodePonto }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "QrCodePonto", new { id = q.IdQrCodePonto }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(GetByPosXRange), "QrCodePonto", new { posXInicial, posXFinal }, Request.Scheme),
                        get = Url.Action(nameof(Get), "QrCodePonto", null, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NoContent();
        }

        [HttpGet("posicao-y")]
        [SwaggerOperation(
            Summary = "Obtém QR Codes por faixa de posição Y",
            Description = "Retorna uma lista paginada de QR Codes filtrados por faixa de posição Y. " +
                        "Útil para encontrar QR Codes em uma área específica do layout."
        )]
        [SwaggerResponse(statusCode: 200, description: "QR Codes encontrados com sucesso", type: typeof(IEnumerable<QrCodePontoResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhum QR Code encontrado para a faixa especificada")]
        [SwaggerResponse(statusCode: 400, description: "Valores de posição são obrigatórios")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByPosYRange(
            [FromQuery, SwaggerParameter("Posição Y inicial", Required = true)] float posYInicial, 
            [FromQuery, SwaggerParameter("Posição Y final", Required = true)] float posYFinal)
        {
            var result = _applicationService.ObterQrCodePontosPorPosicaoYEntre(posYInicial, posYFinal);

            if (result is not null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(q => new
                    {
                        q.IdQrCodePonto,
                        q.IdentificadorQrCodePonto,
                        q.PosXQrCodePonto,
                        q.PosYQrCodePonto,
                        q.LayoutPatioQrCodePonto,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "QrCodePonto", new { id = q.IdQrCodePonto }, Request.Scheme),
                            put = Url.Action(nameof(Put), "QrCodePonto", new { id = q.IdQrCodePonto }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "QrCodePonto", new { id = q.IdQrCodePonto }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(GetByPosYRange), "QrCodePonto", new { posYInicial, posYFinal }, Request.Scheme),
                        get = Url.Action(nameof(Get), "QrCodePonto", null, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NoContent();
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria novo QR Code de ponto",
            Description = "Cria um novo QR Code de ponto no sistema com os dados fornecidos. " +
                        "Valida se todos os campos obrigatórios estão preenchidos e se não há duplicatas. " +
                        "Retorna os dados do QR Code criado incluindo o ID gerado."
        )]
        [SwaggerRequestExample(typeof(QrCodePontoRequestDto), typeof(QrCodePontoRequestDtoSample))]
        [SwaggerResponse(statusCode: 201, description: "QR Code criado com sucesso", type: typeof(QrCodePontoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "Dados inválidos - campos obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível criar o QR Code - dados inválidos ou duplicados")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult Post(
            [FromBody, SwaggerParameter("Dados do QR Code de ponto a ser criado", Required = true)] QrCodePontoRequestDto entity)
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
        [SwaggerOperation(
            Summary = "Atualiza QR Code de ponto existente",
            Description = "Atualiza os dados de um QR Code de ponto existente baseado no ID fornecido. " +
                        "Valida se o QR Code existe e se os novos dados são válidos. " +
                        "Retorna os dados atualizados do QR Code."
        )]
        [SwaggerRequestExample(typeof(QrCodePontoRequestDto), typeof(QrCodePontoRequestDtoSample))]
        [SwaggerResponse(statusCode: 200, description: "QR Code atualizado com sucesso", type: typeof(QrCodePontoResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido ou dados obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 404, description: "QR Code não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível atualizar o QR Code - dados inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult Put(
            [FromRoute, SwaggerParameter("ID único do QR Code de ponto a ser atualizado", Required = true)] int id, 
            [FromBody, SwaggerParameter("Novos dados do QR Code de ponto", Required = true)] QrCodePontoRequestDto entity)
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
        [SwaggerOperation(
            Summary = "Remove QR Code de ponto",
            Description = "Remove permanentemente um QR Code de ponto do sistema baseado no ID fornecido. " +
                        "Esta operação é irreversível e remove todos os dados associados ao QR Code."
        )]
        [SwaggerResponse(statusCode: 204, description: "QR Code removido com sucesso")]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "QR Code não encontrado para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível remover o QR Code")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult Delete(
            [FromRoute, SwaggerParameter("ID único do QR Code de ponto a ser removido", Required = true)] int id)
        {
            var result = _applicationService.DeletarDadosQrCodePonto(id);

            if (result is not null)
                return NoContent();

            return NotFound();
        }
    }
}
