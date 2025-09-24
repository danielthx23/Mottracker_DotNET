using Mottracker.Application.Dtos.Patio;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Mappers;
using Mottracker.Application.Models;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using System.Net;

namespace Mottracker.Application.UseCases
{
    public class PatioUseCase : IPatioUseCase
    {
        private readonly IPatioRepository _repository;

        public PatioUseCase(IPatioRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<PatioResponseDto>>>> ObterTodosPatiosAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            try
            {
                var result = await _repository.ObterTodasAsync(Deslocamento, RegistrosRetornado);

                if (!result.Data.Any())
                    return OperationResult<PageResultModel<IEnumerable<PatioResponseDto>>>.Failure("Não foram encontrados pátios", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(p => p.ToPatioResponseDto());

                var pageResult = new PageResultModel<IEnumerable<PatioResponseDto>>
                {
                    Data = responseDtos,
                    Deslocamento = result.Deslocamento,
                    RegistrosRetornado = result.RegistrosRetornado,
                    TotalRegistros = result.TotalRegistros
                };

                return OperationResult<PageResultModel<IEnumerable<PatioResponseDto>>>.Success(pageResult);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PageResultModel<IEnumerable<PatioResponseDto>>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<PatioResponseDto?>> ObterPatioPorIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<PatioResponseDto?>.Failure("ID inválido");

                var result = await _repository.ObterPorIdAsync(id);

                if (result is null)
                    return OperationResult<PatioResponseDto?>.Failure("Pátio não encontrado", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToPatioResponseDto();

                return OperationResult<PatioResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PatioResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<PatioResponseDto?>> SalvarPatioAsync(PatioRequestDto dto)
        {
            try
            {
                // Validação básica
                if (string.IsNullOrWhiteSpace(dto.NomePatio))
                    return OperationResult<PatioResponseDto?>.Failure("Nome do pátio é obrigatório");

                var result = await _repository.SalvarAsync(dto.ToPatioEntity());

                if (result is null)
                    return OperationResult<PatioResponseDto?>.Failure(
                        "Não foi possível criar o pátio",
                        (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToPatioResponseDto();

                return OperationResult<PatioResponseDto?>.Success(responseDto, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PatioResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<PatioResponseDto?>> EditarPatioAsync(int id, PatioRequestDto dto)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<PatioResponseDto?>.Failure("ID inválido");

                if (string.IsNullOrWhiteSpace(dto.NomePatio))
                    return OperationResult<PatioResponseDto?>.Failure("Nome do pátio é obrigatório");

                var patioExistente = await _repository.ObterPorIdAsync(id);
                if (patioExistente == null)
                    return OperationResult<PatioResponseDto?>.Failure("Pátio não encontrado", (int)HttpStatusCode.NotFound);

                patioExistente.NomePatio = dto.NomePatio;
                patioExistente.MotosTotaisPatio = dto.MotosTotaisPatio;
                patioExistente.MotosDisponiveisPatio = dto.MotosDisponiveisPatio;
                patioExistente.DataPatio = dto.DataPatio;

                var result = await _repository.AtualizarAsync(patioExistente);

                if (result is null)
                    return OperationResult<PatioResponseDto?>.Failure("Não foi possível atualizar o pátio", (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToPatioResponseDto();

                return OperationResult<PatioResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PatioResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<PatioResponseDto?>> DeletarPatioAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<PatioResponseDto?>.Failure("ID inválido");

                var result = await _repository.DeletarAsync(id);

                if (result is null)
                    return OperationResult<PatioResponseDto?>.Failure("Pátio não encontrado", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToPatioResponseDto();

                return OperationResult<PatioResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PatioResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
