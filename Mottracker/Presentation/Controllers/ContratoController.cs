using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Contrato;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoApplicationService _applicationService;

        public ContratoController(IContratoApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista todos os contratos",
            Description = "Retorna todos os contratos cadastrados no sistema.")]
        [SwaggerResponse(200, "Lista de contratos retornada com sucesso", typeof(IEnumerable<ContratoResponseDto>))]
        [SwaggerResponse(204, "Nenhum contrato encontrado")]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodosContratos();
            return result is not null && result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obtém contrato por ID",
            Description = "Retorna os dados de um contrato específico identificado pelo ID.")]
        [SwaggerResponse(200, "Contrato retornado com sucesso", typeof(ContratoResponseDto))]
        [SwaggerResponse(404, "Contrato não encontrado")]
        public IActionResult GetById(int id)
        {
            var result = _applicationService.ObterContratoPorId(id);
            return result is not null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria um novo contrato",
            Description = "Salva os dados de um novo contrato no sistema.")]
        [SwaggerResponse(201, "Contrato criado com sucesso", typeof(ContratoResponseDto))]
        [SwaggerResponse(400, "Erro ao salvar o contrato")]
        public IActionResult Post([FromBody] ContratoRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosContrato(entity);
                return result is not null
                    ? CreatedAtAction(nameof(GetById), new { id = result.IdContrato }, result)
                    : BadRequest("Não foi possível salvar os dados.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualiza contrato existente",
            Description = "Edita os dados de um contrato já existente com base no ID.")]
        [SwaggerResponse(200, "Contrato atualizado com sucesso", typeof(ContratoResponseDto))]
        [SwaggerResponse(404, "Contrato não encontrado")]
        [SwaggerResponse(400, "Erro ao atualizar o contrato")]
        public IActionResult Put(int id, [FromBody] ContratoRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosContrato(id, entity);
                return result is not null ? Ok(result) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remove um contrato",
            Description = "Deleta um contrato pelo ID fornecido.")]
        [SwaggerResponse(204, "Contrato deletado com sucesso")]
        [SwaggerResponse(404, "Contrato não encontrado")]
        public IActionResult Delete(int id)
        {
            var result = _applicationService.DeletarDadosContrato(id);
            return result is not null ? NoContent() : NotFound();
        }

        [HttpGet("ativo/{ativoContrato}")]
        [SwaggerOperation(
            Summary = "Lista contratos por status de ativo",
            Description = "Retorna contratos filtrando pelo campo AtivoContrato (1 = Ativo, 0 = Inativo).")]
        [SwaggerResponse(200, "Contratos ativos retornados", typeof(IEnumerable<ContratoResponseDto>))]
        [SwaggerResponse(204, "Nenhum contrato encontrado")]
        public IActionResult GetByAtivo(int ativoContrato)
        {
            var result = _applicationService.ObterPorAtivoContrato(ativoContrato);
            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("usuario/{usuarioId}")]
        [SwaggerOperation(
            Summary = "Lista contratos por ID do usuário",
            Description = "Retorna todos os contratos vinculados a um determinado usuário pelo ID.")]
        [SwaggerResponse(200, "Contratos retornados", typeof(IEnumerable<ContratoResponseDto>))]
        [SwaggerResponse(204, "Nenhum contrato encontrado")]
        public IActionResult GetByUsuarioId(long usuarioId)
        {
            var result = _applicationService.ObterPorUsuarioId(usuarioId);
            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("moto/{motoId}")]
        [SwaggerOperation(
            Summary = "Lista contratos por ID da moto",
            Description = "Retorna todos os contratos relacionados a uma determinada moto pelo seu ID.")]
        [SwaggerResponse(200, "Contratos retornados", typeof(IEnumerable<ContratoResponseDto>))]
        [SwaggerResponse(204, "Nenhum contrato encontrado")]
        public IActionResult GetByMotoId(long motoId)
        {
            var result = _applicationService.ObterPorMotoId(motoId);
            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("nao-expirados")]
        [SwaggerOperation(
            Summary = "Lista contratos que ainda não expiraram",
            Description = "Retorna todos os contratos cuja data de expiração é maior ou igual à data atual.")]
        [SwaggerResponse(200, "Contratos não expirados retornados", typeof(IEnumerable<ContratoResponseDto>))]
        [SwaggerResponse(204, "Nenhum contrato encontrado")]
        public IActionResult GetContratosNaoExpirados()
        {
            var dataAtual = DateTime.UtcNow;
            var result = _applicationService.ObterContratosNaoExpirados(dataAtual);
            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("renovacao-automatica/{renovacao}")]
        [SwaggerOperation(
            Summary = "Lista contratos com renovação automática",
            Description = "Retorna todos os contratos com o campo RenovacaoAutomaticaContrato igual ao valor informado (1 ou 0).")]
        [SwaggerResponse(200, "Contratos retornados com sucesso", typeof(IEnumerable<ContratoResponseDto>))]
        [SwaggerResponse(204, "Nenhum contrato encontrado")]
        public IActionResult GetByRenovacaoAutomatica(int renovacao)
        {
            var result = _applicationService.ObterPorRenovacaoAutomatica(renovacao);
            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("por-data-entrada")]
        [SwaggerOperation(
            Summary = "Lista contratos entre duas datas de entrada",
            Description = "Retorna contratos cuja DataDeEntradaContrato está entre dataInicio e dataFim (inclusive).")]
        [SwaggerResponse(200, "Contratos retornados com sucesso", typeof(IEnumerable<ContratoResponseDto>))]
        [SwaggerResponse(204, "Nenhum contrato encontrado")]
        [SwaggerResponse(400, "Data de início maior que a data de fim")]
        public IActionResult GetByDataEntradaEntre([FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim)
        {
            if (dataFim < dataInicio)
                return BadRequest("A data final não pode ser menor que a data inicial.");

            var result = _applicationService.ObterPorDataEntradaEntre(dataInicio, dataFim);
            return result.Any() ? Ok(result) : NoContent();
        }
    }
}
