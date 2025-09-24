using Mottracker.Application.Dtos.Endereco;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Mappers;
using Mottracker.Application.Models;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using System.Net;

namespace Mottracker.Application.UseCases
{
    public class EnderecoUseCase : IEnderecoUseCase
    {
        private readonly IEnderecoRepository _repository;

        public EnderecoUseCase(IEnderecoRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<EnderecoResponseDto>>>> ObterTodosEnderecosAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            try
            {
                var result = await _repository.ObterTodasAsync(Deslocamento, RegistrosRetornado);

                if (!result.Data.Any())
                    return OperationResult<PageResultModel<IEnumerable<EnderecoResponseDto>>>.Failure("Não foram encontrados endereços", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(e => e.ToEnderecoResponseDto());

                var pageResult = new PageResultModel<IEnumerable<EnderecoResponseDto>>
                {
                    Data = responseDtos,
                    Deslocamento = result.Deslocamento,
                    RegistrosRetornado = result.RegistrosRetornado,
                    TotalRegistros = result.TotalRegistros
                };

                return OperationResult<PageResultModel<IEnumerable<EnderecoResponseDto>>>.Success(pageResult);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PageResultModel<IEnumerable<EnderecoResponseDto>>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<EnderecoResponseDto?>> ObterEnderecoPorIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<EnderecoResponseDto?>.Failure("ID inválido");

                var result = await _repository.ObterPorIdAsync(id);

                if (result is null)
                    return OperationResult<EnderecoResponseDto?>.Failure("Endereço não encontrado", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToEnderecoResponseDto();

                return OperationResult<EnderecoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<EnderecoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<EnderecoResponseDto?>> SalvarEnderecoAsync(EnderecoRequestDto dto)
        {
            try
            {
                // Validação básica
                if (string.IsNullOrWhiteSpace(dto.Logradouro))
                    return OperationResult<EnderecoResponseDto?>.Failure("Logradouro é obrigatório");

                if (string.IsNullOrWhiteSpace(dto.CEP))
                    return OperationResult<EnderecoResponseDto?>.Failure("CEP é obrigatório");

                var result = await _repository.SalvarAsync(dto.ToEnderecoEntity());

                if (result is null)
                    return OperationResult<EnderecoResponseDto?>.Failure(
                        "Não foi possível criar o endereço",
                        (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToEnderecoResponseDto();

                return OperationResult<EnderecoResponseDto?>.Success(responseDto, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<EnderecoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<EnderecoResponseDto?>> EditarEnderecoAsync(int id, EnderecoRequestDto dto)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<EnderecoResponseDto?>.Failure("ID inválido");

                if (string.IsNullOrWhiteSpace(dto.Logradouro))
                    return OperationResult<EnderecoResponseDto?>.Failure("Logradouro é obrigatório");

                if (string.IsNullOrWhiteSpace(dto.CEP))
                    return OperationResult<EnderecoResponseDto?>.Failure("CEP é obrigatório");

                var enderecoExistente = await _repository.ObterPorIdAsync(id);
                if (enderecoExistente == null)
                    return OperationResult<EnderecoResponseDto?>.Failure("Endereço não encontrado", (int)HttpStatusCode.NotFound);

                enderecoExistente.Logradouro = dto.Logradouro;
                enderecoExistente.Numero = dto.Numero;
                enderecoExistente.Complemento = dto.Complemento;
                enderecoExistente.Bairro = dto.Bairro;
                enderecoExistente.Cidade = dto.Cidade;
                enderecoExistente.Estado = dto.Estado;
                enderecoExistente.CEP = dto.CEP;
                enderecoExistente.Referencia = dto.Referencia;
                enderecoExistente.PatioEnderecoId = dto.PatioId;

                var result = await _repository.AtualizarAsync(enderecoExistente);

                if (result is null)
                    return OperationResult<EnderecoResponseDto?>.Failure("Não foi possível atualizar o endereço", (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToEnderecoResponseDto();

                return OperationResult<EnderecoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<EnderecoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<EnderecoResponseDto?>> DeletarEnderecoAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<EnderecoResponseDto?>.Failure("ID inválido");

                var result = await _repository.DeletarAsync(id);

                if (result is null)
                    return OperationResult<EnderecoResponseDto?>.Failure("Endereço não encontrado", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToEnderecoResponseDto();

                return OperationResult<EnderecoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<EnderecoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
