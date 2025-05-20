using Microsoft.AspNetCore.Mvc;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Dtos.Endereco;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Mottracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoApplicationService _applicationService;

        public EnderecoController(IEnderecoApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos os endereços", Description = "Retorna todos os endereços cadastrados.")]
        [SwaggerResponse(200, "Endereços retornados com sucesso", typeof(IEnumerable<EnderecoResponseDto>))]
        [SwaggerResponse(204, "Nenhum endereço encontrado")]
        [SwaggerResponse(400, "Erro ao obter os endereços")]
        public IActionResult Get()
        {
            var result = _applicationService.ObterTodosEnderecos();

            if (result is not null && result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém endereço por ID", Description = "Retorna os dados de um endereço específico.")]
        [SwaggerResponse(200, "Endereço retornado com sucesso", typeof(EnderecoResponseDto))]
        [SwaggerResponse(404, "Endereço não encontrado")]
        [SwaggerResponse(400, "Erro ao obter o endereço")]
        public IActionResult GetById(int id)
        {
            var result = _applicationService.ObterEnderecoPorId(id);

            if (result is not null)
                return Ok(result);

            return NotFound();
        }

        [HttpGet("cep/{cep}")]
        [SwaggerOperation(Summary = "Obtém endereço por CEP", Description = "Retorna um endereço específico pelo CEP.")]
        [SwaggerResponse(200, "Endereço encontrado", typeof(EnderecoResponseDto))]
        [SwaggerResponse(404, "Endereço não encontrado")]
        public IActionResult GetByCep(string cep)
        {
            var result = _applicationService.ObterEnderecoPorCep(cep);
            return result is not null ? Ok(result) : NotFound();
        }

        [HttpGet("estado/{estado}")]
        [SwaggerOperation(Summary = "Busca endereços por estado", Description = "Retorna todos os endereços no estado informado.")]
        [SwaggerResponse(200, "Endereços encontrados", typeof(IEnumerable<EnderecoResponseDto>))]
        [SwaggerResponse(204, "Nenhum endereço encontrado")]
        public IActionResult GetByEstado(string estado)
        {
            var result = _applicationService.ObterEnderecosPorEstado(estado);
            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("cidade/{cidade}")]
        [SwaggerOperation(Summary = "Busca endereços por cidade", Description = "Retorna todos os endereços na cidade informada.")]
        [SwaggerResponse(200, "Endereços encontrados", typeof(IEnumerable<EnderecoResponseDto>))]
        [SwaggerResponse(204, "Nenhum endereço encontrado")]
        public IActionResult GetByCidade(string cidade)
        {
            var result = _applicationService.ObterEnderecosPorCidade(cidade);
            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("cidade-estado")]
        [SwaggerOperation(Summary = "Busca por cidade e estado", Description = "Retorna todos os endereços filtrados por cidade e estado.")]
        [SwaggerResponse(200, "Endereços encontrados", typeof(IEnumerable<EnderecoResponseDto>))]
        [SwaggerResponse(204, "Nenhum endereço encontrado")]
        public IActionResult GetByCidadeEstado([FromQuery] string cidade, [FromQuery] string estado)
        {
            var result = _applicationService.ObterEnderecosPorCidadeEEstado(cidade, estado);
            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("bairro/{bairro}")]
        [SwaggerOperation(Summary = "Busca endereços por bairro", Description = "Retorna todos os endereços localizados no bairro informado.")]
        [SwaggerResponse(200, "Endereços encontrados", typeof(IEnumerable<EnderecoResponseDto>))]
        [SwaggerResponse(204, "Nenhum endereço encontrado")]
        public IActionResult GetByBairro(string bairro)
        {
            var result = _applicationService.ObterEnderecosPorBairro(bairro);
            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("logradouro")]
        [SwaggerOperation(Summary = "Busca por logradouro parcial", Description = "Retorna endereços cujo logradouro contenha a string informada.")]
        [SwaggerResponse(200, "Endereços encontrados", typeof(IEnumerable<EnderecoResponseDto>))]
        [SwaggerResponse(204, "Nenhum endereço encontrado")]
        public IActionResult GetByLogradouroContendo([FromQuery] string logradouro)
        {
            var result = _applicationService.ObterEnderecosPorLogradouroContendo(logradouro);
            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("patio/{patioId}")]
        [SwaggerOperation(Summary = "Busca endereço por ID de Pátio", Description = "Retorna o endereço vinculado ao ID do pátio informado.")]
        [SwaggerResponse(200, "Endereço encontrado", typeof(EnderecoResponseDto))]
        [SwaggerResponse(404, "Endereço não encontrado")]
        public IActionResult GetByPatioId(long patioId)
        {
            var result = _applicationService.ObterEnderecoPorIdPatio(patioId);
            return result is not null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo endereço", Description = "Salva um novo endereço no sistema.")]
        [SwaggerResponse(201, "Endereço criado com sucesso", typeof(EnderecoResponseDto))]
        [SwaggerResponse(400, "Erro ao salvar o endereço")]
        public IActionResult Post([FromBody] EnderecoRequestDto entity)
        {
            try
            {
                var result = _applicationService.SalvarDadosEndereco(entity);

                if (result is not null)
                    return CreatedAtAction(nameof(GetById), new { id = result.IdEndereco }, result);

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
        [SwaggerOperation(Summary = "Atualiza endereço existente", Description = "Edita os dados de um endereço já existente.")]
        [SwaggerResponse(200, "Endereço atualizado com sucesso", typeof(EnderecoResponseDto))]
        [SwaggerResponse(404, "Endereço não encontrado")]
        [SwaggerResponse(400, "Erro ao atualizar o endereço")]
        public IActionResult Put(int id, [FromBody] EnderecoRequestDto entity)
        {
            try
            {
                var result = _applicationService.EditarDadosEndereco(id, entity);

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
        [SwaggerOperation(Summary = "Remove um endereço", Description = "Deleta um endereço pelo ID fornecido.")]
        [SwaggerResponse(204, "Endereço deletado com sucesso")]
        [SwaggerResponse(404, "Endereço não encontrado")]
        [SwaggerResponse(400, "Erro ao deletar o endereço")]
        public IActionResult Delete(int id)
        {
            var result = _applicationService.DeletarDadosEndereco(id);

            if (result is not null)
                return NoContent();

            return NotFound();
        }
    }
}