using Mottracker.Application.Dtos.Telefone;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Mappers;
using Mottracker.Application.Models;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using System.Net;

namespace Mottracker.Application.UseCases
{
    public class TelefoneUseCase : ITelefoneUseCase
    {
        private readonly ITelefoneRepository _repository;

        public TelefoneUseCase(ITelefoneRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<TelefoneResponseDto>>>> ObterTodosTelefonesAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            try
            {
                var result = await _repository.ObterTodasAsync(Deslocamento, RegistrosRetornado);

                if (!result.Data.Any())
                    return OperationResult<PageResultModel<IEnumerable<TelefoneResponseDto>>>.Failure("Não foram encontrados telefones", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(t => t.ToTelefoneResponseDto());

                var pageResult = new PageResultModel<IEnumerable<TelefoneResponseDto>>
                {
                    Data = responseDtos,
                    Deslocamento = result.Deslocamento,
                    RegistrosRetornado = result.RegistrosRetornado,
                    TotalRegistros = result.TotalRegistros
                };

                return OperationResult<PageResultModel<IEnumerable<TelefoneResponseDto>>>.Success(pageResult);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PageResultModel<IEnumerable<TelefoneResponseDto>>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<TelefoneResponseDto?>> ObterTelefonePorIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<TelefoneResponseDto?>.Failure("ID inválido");

                var result = await _repository.ObterPorIdAsync(id);

                if (result is null)
                    return OperationResult<TelefoneResponseDto?>.Failure("Telefone não encontrado", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToTelefoneResponseDto();

                return OperationResult<TelefoneResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<TelefoneResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<TelefoneResponseDto?>> SalvarTelefoneAsync(TelefoneRequestDto dto)
        {
            try
            {
                // Validação básica
                if (string.IsNullOrWhiteSpace(dto.NumeroTelefone))
                    return OperationResult<TelefoneResponseDto?>.Failure("Número do telefone é obrigatório");

                var result = await _repository.SalvarAsync(dto.ToTelefoneEntity());

                if (result is null)
                    return OperationResult<TelefoneResponseDto?>.Failure(
                        "Não foi possível criar o telefone",
                        (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToTelefoneResponseDto();

                return OperationResult<TelefoneResponseDto?>.Success(responseDto, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<TelefoneResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<TelefoneResponseDto?>> EditarTelefoneAsync(int id, TelefoneRequestDto dto)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<TelefoneResponseDto?>.Failure("ID inválido");

                if (string.IsNullOrWhiteSpace(dto.NumeroTelefone))
                    return OperationResult<TelefoneResponseDto?>.Failure("Número do telefone é obrigatório");

                var telefoneExistente = await _repository.ObterPorIdAsync(id);
                if (telefoneExistente == null)
                    return OperationResult<TelefoneResponseDto?>.Failure("Telefone não encontrado", (int)HttpStatusCode.NotFound);

                telefoneExistente.Numero = dto.NumeroTelefone;
                telefoneExistente.Tipo = dto.TipoTelefone;
                telefoneExistente.UsuarioId = dto.UsuarioId;

                var result = await _repository.AtualizarAsync(telefoneExistente);

                if (result is null)
                    return OperationResult<TelefoneResponseDto?>.Failure("Não foi possível atualizar o telefone", (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToTelefoneResponseDto();

                return OperationResult<TelefoneResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<TelefoneResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<TelefoneResponseDto?>> DeletarTelefoneAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<TelefoneResponseDto?>.Failure("ID inválido");

                var result = await _repository.DeletarAsync(id);

                if (result is null)
                    return OperationResult<TelefoneResponseDto?>.Failure("Telefone não encontrado", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToTelefoneResponseDto();

                return OperationResult<TelefoneResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<TelefoneResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
