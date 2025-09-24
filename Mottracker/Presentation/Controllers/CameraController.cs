using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Camera;
using Mottracker.Docs.Samples;
using Mottracker.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/v1/camera")]
    [ApiController]
    public class CameraController : ControllerBase
    {
        private readonly ICameraApplicationService _applicationService;

        public CameraController(ICameraApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(
           Summary = "Lista câmeras com paginação",
           Description = "Retorna uma lista paginada de câmeras cadastradas no sistema. " +
                        "Este endpoint suporta paginação para otimizar a performance com grandes volumes de dados. " +
                        "As câmeras são retornadas com informações completas e links HATEOAS para navegação."
        )]
        [SwaggerResponse(statusCode: 200, description: "Lista de câmeras retornada com sucesso", type: typeof(IEnumerable<CameraResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhuma câmera encontrada")]
        [SwaggerResponse(statusCode: 400, description: "Parâmetros de paginação inválidos")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        [SwaggerResponseExample(statusCode: 200, typeof(CameraResponseListSample))]
        [EnableRateLimiting("rateLimitePolicy")]
        public IActionResult Get(
            [FromQuery, SwaggerParameter("Número de registros a pular (padrão: 0)", Required = false)] int Deslocamento = 0, 
            [FromQuery, SwaggerParameter("Número de registros a retornar (padrão: 3, máximo: 100)", Required = false)] int RegistrosRetornado = 3)
        {
            var result = _applicationService.ObterTodasCameras();

            if (result is not null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(c => new
                    {
                        c.IdCamera,
                        c.NomeCamera,
                        c.IpCamera,
                        c.Status,
                        c.PosX,
                        c.PosY,
                        c.Patio,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "Camera", new { id = c.IdCamera }, Request.Scheme),
                            put = Url.Action(nameof(Put), "Camera", new { id = c.IdCamera }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "Camera", new { id = c.IdCamera }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(Get), "Camera", null, Request.Scheme),
                        create = Url.Action(nameof(Post), "Camera", null, Request.Scheme),
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
            Summary = "Obtém câmera por ID",
            Description = "Retorna os dados completos de uma câmera específica baseada no ID fornecido. " +
                        "Inclui informações detalhadas da câmera e links HATEOAS para operações relacionadas."
        )]
        [SwaggerResponse(statusCode: 200, description: "Câmera encontrada com sucesso", type: typeof(CameraResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "Câmera não encontrada para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetById(
            [FromRoute, SwaggerParameter("ID único da câmera", Required = true)] int id)
        {
            var result = _applicationService.ObterCameraPorId(id);

            if (result is not null)
            {
                var hateaos = new
                {
                    data = result,
                    links = new
                    {
                        self = Url.Action(nameof(GetById), "Camera", new { id }),
                        get = Url.Action(nameof(Get), "Camera", null),
                        put = Url.Action(nameof(Put), "Camera", new { id }),
                        delete = Url.Action(nameof(Delete), "Camera", new { id }),
                    }
                };

                return Ok(hateaos);
            }

            return NotFound();
        }

        [HttpGet("nome/{nome}")]
        [SwaggerOperation(
            Summary = "Obtém câmeras por nome",
            Description = "Retorna uma lista paginada de câmeras filtradas por nome específico. " +
                        "Útil para encontrar todas as câmeras que contenham palavras-chave no nome."
        )]
        [SwaggerResponse(statusCode: 200, description: "Câmeras encontradas com sucesso", type: typeof(IEnumerable<CameraResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhuma câmera encontrada para o nome especificado")]
        [SwaggerResponse(statusCode: 400, description: "Nome é obrigatório e não pode estar vazio")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByNome(
            [FromRoute, SwaggerParameter("Nome da câmera para busca", Required = true)] string nome)
        {
            var result = _applicationService.ObterCameraPorNome(nome);

            if (result is not null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(c => new
                    {
                        c.IdCamera,
                        c.NomeCamera,
                        c.IpCamera,
                        c.Status,
                        c.PosX,
                        c.PosY,
                        c.Patio,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "Camera", new { id = c.IdCamera }, Request.Scheme),
                            put = Url.Action(nameof(Put), "Camera", new { id = c.IdCamera }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "Camera", new { id = c.IdCamera }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(GetByNome), "Camera", new { nome }, Request.Scheme),
                        get = Url.Action(nameof(Get), "Camera", null, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NoContent();
        }

        [HttpGet("status/{status}")]
        [SwaggerOperation(
            Summary = "Obtém câmeras por status",
            Description = "Retorna uma lista paginada de câmeras filtradas por status específico. " +
                        "Útil para encontrar todas as câmeras com um determinado status (Ativa, Inativa)."
        )]
        [SwaggerResponse(statusCode: 200, description: "Câmeras encontradas com sucesso", type: typeof(IEnumerable<CameraResponseDto>))]
        [SwaggerResponse(statusCode: 204, description: "Nenhuma câmera encontrada para o status especificado")]
        [SwaggerResponse(statusCode: 400, description: "Status é obrigatório")]
        [SwaggerResponse(statusCode: 422, description: "Dados de entrada inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult GetByStatus(
            [FromRoute, SwaggerParameter("Status da câmera (Ativa, Inativa)", Required = true)] CameraStatus status)
        {
            var result = _applicationService.ObterCameraPorStatus(status);

            if (result is not null && result.Any())
            {
                var hateaos = new
                {
                    data = result.Select(c => new
                    {
                        c.IdCamera,
                        c.NomeCamera,
                        c.IpCamera,
                        c.Status,
                        c.PosX,
                        c.PosY,
                        c.Patio,
                        links = new
                        {
                            self = Url.Action(nameof(GetById), "Camera", new { id = c.IdCamera }, Request.Scheme),
                            put = Url.Action(nameof(Put), "Camera", new { id = c.IdCamera }, Request.Scheme),
                            delete = Url.Action(nameof(Delete), "Camera", new { id = c.IdCamera }, Request.Scheme),
                        }
                    }),
                    links = new
                    {
                        self = Url.Action(nameof(GetByStatus), "Camera", new { status }, Request.Scheme),
                        get = Url.Action(nameof(Get), "Camera", null, Request.Scheme),
                    }
                };

                return Ok(hateaos);
            }

            return NoContent();
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria nova câmera",
            Description = "Cria uma nova câmera no sistema com os dados fornecidos. " +
                        "Valida se todos os campos obrigatórios estão preenchidos e se não há duplicatas. " +
                        "Retorna os dados da câmera criada incluindo o ID gerado."
        )]
        [SwaggerRequestExample(typeof(CameraRequestDto), typeof(CameraRequestDtoSample))]
        [SwaggerResponse(statusCode: 201, description: "Câmera criada com sucesso", type: typeof(CameraResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "Dados inválidos - nome é obrigatório")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível criar a câmera - dados inválidos ou duplicados")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult Post(
            [FromBody, SwaggerParameter("Dados da câmera a ser criada", Required = true)] CameraRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosCamera(entity);

                if (result is not null)
                    return CreatedAtAction(nameof(GetById), new { id = result.IdCamera }, result);

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
            Summary = "Atualiza câmera existente",
            Description = "Atualiza os dados de uma câmera existente baseada no ID fornecido. " +
                        "Valida se a câmera existe e se os novos dados são válidos. " +
                        "Retorna os dados atualizados da câmera."
        )]
        [SwaggerRequestExample(typeof(CameraRequestDto), typeof(CameraRequestDtoSample))]
        [SwaggerResponse(statusCode: 200, description: "Câmera atualizada com sucesso", type: typeof(CameraResponseDto))]
        [SwaggerResponse(statusCode: 400, description: "ID inválido ou dados obrigatórios ausentes")]
        [SwaggerResponse(statusCode: 404, description: "Câmera não encontrada para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível atualizar a câmera - dados inválidos")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult Put(
            [FromRoute, SwaggerParameter("ID único da câmera a ser atualizada", Required = true)] int id, 
            [FromBody, SwaggerParameter("Novos dados da câmera", Required = true)] CameraRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosCamera(id, entity);

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
            Summary = "Remove câmera",
            Description = "Remove permanentemente uma câmera do sistema baseada no ID fornecido. " +
                        "Esta operação é irreversível e remove todos os dados associados à câmera."
        )]
        [SwaggerResponse(statusCode: 204, description: "Câmera removida com sucesso")]
        [SwaggerResponse(statusCode: 400, description: "ID inválido - deve ser um número positivo")]
        [SwaggerResponse(statusCode: 404, description: "Câmera não encontrada para o ID fornecido")]
        [SwaggerResponse(statusCode: 422, description: "Não foi possível remover a câmera")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno do servidor")]
        public IActionResult Delete(
            [FromRoute, SwaggerParameter("ID único da câmera a ser removida", Required = true)] int id)
        {
            var result = _applicationService.DeletarDadosCamera(id);

            if (result is not null)
                return NoContent();

            return NotFound();
        }
    }
}