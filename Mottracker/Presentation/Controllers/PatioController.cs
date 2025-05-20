using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Patio;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatioController : ControllerBase
    {
        private readonly IPatioApplicationService _applicationService;

        public PatioController(IPatioApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos os pátios", Description = "Retorna todos os pátios cadastrados.")]
        [SwaggerResponse(200, "Pátios retornados com sucesso", typeof(IEnumerable<PatioResponseDto>))]
        [SwaggerResponse(204, "Nenhum pátio encontrado")]
        [SwaggerResponse(400, "Erro ao obter os pátios")]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodosPatios();

            if (result is not null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém pátio por ID", Description = "Retorna os dados de um pátio específico.")]
        [SwaggerResponse(200, "Pátio retornado com sucesso", typeof(PatioResponseDto))]
        [SwaggerResponse(404, "Pátio não encontrado")]
        [SwaggerResponse(400, "Erro ao obter o pátio")]
        public IActionResult GetById(int id)
        {
            var result = _applicationService.ObterPatioPorId(id);

            if (result is not null)
                return Ok(result);

            return NotFound();
        }

        [HttpGet("buscar-por-nome")]
        [SwaggerOperation(Summary = "Busca pátios pelo nome contendo", Description = "Retorna pátios cujo nome contenha o texto informado.")]
        [SwaggerResponse(200, "Pátios retornados com sucesso", typeof(IEnumerable<PatioResponseDto>))]
        [SwaggerResponse(204, "Nenhum pátio encontrado")]
        [SwaggerResponse(400, "Erro ao buscar pátios")]
        public IActionResult GetByNome([FromQuery] string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return BadRequest("Parâmetro 'nome' é obrigatório.");

            var result = _applicationService.ObterPatiosPorNomeContendo(nome);

            if (result != null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("motos-disponiveis-maior-que")]
        [SwaggerOperation(Summary = "Busca pátios com motos disponíveis acima de uma quantidade", Description = "Retorna pátios com quantidade de motos disponíveis maior que o informado.")]
        [SwaggerResponse(200, "Pátios retornados com sucesso", typeof(IEnumerable<PatioResponseDto>))]
        [SwaggerResponse(204, "Nenhum pátio encontrado")]
        [SwaggerResponse(400, "Erro ao buscar pátios")]
        public IActionResult GetByMotosDisponiveis([FromQuery] int quantidade)
        {
            if (quantidade < 0)
                return BadRequest("Quantidade deve ser maior ou igual a zero.");

            var result = _applicationService.ObterPatiosComMotosDisponiveisAcimaDe(quantidade);

            if (result != null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("data-posterior")]
        [SwaggerOperation(Summary = "Busca pátios por data posterior", Description = "Retorna pátios com data posterior à data informada.")]
        [SwaggerResponse(200, "Pátios retornados com sucesso", typeof(IEnumerable<PatioResponseDto>))]
        [SwaggerResponse(204, "Nenhum pátio encontrado")]
        [SwaggerResponse(400, "Erro ao buscar pátios")]
        public IActionResult GetByDataPosterior([FromQuery] DateTime data)
        {
            var result = _applicationService.ObterPatiosPorDataPosterior(data);

            if (result != null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("data-anterior")]
        [SwaggerOperation(Summary = "Busca pátios por data anterior", Description = "Retorna pátios com data anterior à data informada.")]
        [SwaggerResponse(200, "Pátios retornados com sucesso", typeof(IEnumerable<PatioResponseDto>))]
        [SwaggerResponse(204, "Nenhum pátio encontrado")]
        [SwaggerResponse(400, "Erro ao buscar pátios")]
        public IActionResult GetByDataAnterior([FromQuery] DateTime data)
        {
            var result = _applicationService.ObterPatiosPorDataAnterior(data);

            if (result != null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo pátio", Description = "Salva um novo pátio no sistema.")]
        [SwaggerResponse(201, "Pátio criado com sucesso", typeof(PatioResponseDto))]
        [SwaggerResponse(400, "Erro ao salvar o pátio")]
        public IActionResult Post([FromBody] PatioRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosPatio(entity);

                if (result is not null)
                    return CreatedAtAction(nameof(GetById), new { id = result.IdPatio }, result);

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
        [SwaggerOperation(Summary = "Atualiza um pátio", Description = "Edita os dados de um pátio existente.")]
        [SwaggerResponse(200, "Pátio atualizado com sucesso", typeof(PatioResponseDto))]
        [SwaggerResponse(404, "Pátio não encontrado")]
        [SwaggerResponse(400, "Erro ao atualizar o pátio")]
        public IActionResult Put(int id, [FromBody] PatioRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosPatio(id, entity);

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
        [SwaggerOperation(Summary = "Deleta um pátio", Description = "Remove um pátio com base no ID fornecido.")]
        [SwaggerResponse(204, "Pátio deletado com sucesso")]
        [SwaggerResponse(404, "Pátio não encontrado")]
        [SwaggerResponse(400, "Erro ao deletar o pátio")]
        public IActionResult Delete(int id)
        {
            var result = _applicationService.DeletarDadosPatio(id);

            if (result is not null)
                return NoContent();

            return NotFound();
        }
    }
}
