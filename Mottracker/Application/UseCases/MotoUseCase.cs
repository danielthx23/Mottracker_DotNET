using Mottracker.Application.Dtos.Moto;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Mappers;
using Mottracker.Application.Models;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Domain.Enums;
using System.Net;

namespace Mottracker.Application.UseCases
{
    public class MotoUseCase : IMotoUseCase
    {
        private readonly IMotoRepository _repository;

        public MotoUseCase(IMotoRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<MotoResponseDto>>>> ObterTodasMotosAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            try
            {
                var result = await _repository.ObterTodasAsync(Deslocamento, RegistrosRetornado);

                if (!result.Data.Any())
                    return OperationResult<PageResultModel<IEnumerable<MotoResponseDto>>>.Failure("Não foram encontradas motos", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(m => m.ToMotoResponseDto());

                var pageResult = new PageResultModel<IEnumerable<MotoResponseDto>>
                {
                    Data = responseDtos,
                    Deslocamento = result.Deslocamento,
                    RegistrosRetornado = result.RegistrosRetornado,
                    TotalRegistros = result.TotalRegistros
                };

                return OperationResult<PageResultModel<IEnumerable<MotoResponseDto>>>.Success(pageResult);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PageResultModel<IEnumerable<MotoResponseDto>>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<MotoResponseDto?>> ObterMotoPorIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<MotoResponseDto?>.Failure("ID inválido");

                var result = await _repository.ObterPorIdAsync(id);

                if (result is null)
                    return OperationResult<MotoResponseDto?>.Failure("Moto não encontrada", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToMotoResponseDto();

                return OperationResult<MotoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<MotoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<MotoResponseDto?>> ObterMotoPorPlacaAsync(string placa)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(placa))
                    return OperationResult<MotoResponseDto?>.Failure("Placa é obrigatória");

                var result = await _repository.ObterPorPlacaAsync(placa);

                if (result is null)
                    return OperationResult<MotoResponseDto?>.Failure("Moto não encontrada", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToMotoResponseDto();

                return OperationResult<MotoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<MotoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<MotoResponseDto?>> SalvarMotoAsync(MotoRequestDto dto)
        {
            try
            {
                // Validação básica
                if (string.IsNullOrWhiteSpace(dto.PlacaMoto))
                    return OperationResult<MotoResponseDto?>.Failure("Placa da moto é obrigatória");

                if (string.IsNullOrWhiteSpace(dto.ModeloMoto))
                    return OperationResult<MotoResponseDto?>.Failure("Modelo da moto é obrigatório");

                var result = await _repository.SalvarAsync(dto.ToMotoEntity());

                if (result is null)
                    return OperationResult<MotoResponseDto?>.Failure(
                        "Não foi possível criar a moto",
                        (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToMotoResponseDto();

                return OperationResult<MotoResponseDto?>.Success(responseDto, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<MotoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<MotoResponseDto?>> EditarMotoAsync(int id, MotoRequestDto dto)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<MotoResponseDto?>.Failure("ID inválido");

                if (string.IsNullOrWhiteSpace(dto.PlacaMoto))
                    return OperationResult<MotoResponseDto?>.Failure("Placa da moto é obrigatória");

                if (string.IsNullOrWhiteSpace(dto.ModeloMoto))
                    return OperationResult<MotoResponseDto?>.Failure("Modelo da moto é obrigatório");

                var motoExistente = await _repository.ObterPorIdAsync(id);
                if (motoExistente == null)
                    return OperationResult<MotoResponseDto?>.Failure("Moto não encontrada", (int)HttpStatusCode.NotFound);

                motoExistente.PlacaMoto = dto.PlacaMoto;
                motoExistente.ModeloMoto = dto.ModeloMoto;
                motoExistente.AnoMoto = dto.AnoMoto;
                motoExistente.IdentificadorMoto = dto.IdentificadorMoto;
                motoExistente.QuilometragemMoto = dto.QuilometragemMoto;
                motoExistente.EstadoMoto = dto.EstadoMoto;
                motoExistente.CondicoesMoto = dto.CondicoesMoto;
                motoExistente.MotoPatioOrigemId = dto.MotoPatioOrigemId;
                motoExistente.ContratoMotoId = dto.ContratoMotoId;
                motoExistente.MotoPatioAtualId = dto.MotoPatioAtualId;

                var result = await _repository.AtualizarAsync(motoExistente);

                if (result is null)
                    return OperationResult<MotoResponseDto?>.Failure("Não foi possível atualizar a moto", (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToMotoResponseDto();

                return OperationResult<MotoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<MotoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<MotoResponseDto?>> DeletarMotoAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<MotoResponseDto?>.Failure("ID inválido");

                var result = await _repository.DeletarAsync(id);

                if (result is null)
                    return OperationResult<MotoResponseDto?>.Failure("Moto não encontrada", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToMotoResponseDto();

                return OperationResult<MotoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<MotoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<MotoResponseDto>>>> ObterMotosPorEstadoAsync(Estados estado, int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            try
            {
                var result = await _repository.ObterPorEstadoAsync(estado, Deslocamento, RegistrosRetornado);

                if (!result.Data.Any())
                    return OperationResult<PageResultModel<IEnumerable<MotoResponseDto>>>.Failure("Não foram encontradas motos para o estado especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(m => m.ToMotoResponseDto());

                var pageResult = new PageResultModel<IEnumerable<MotoResponseDto>>
                {
                    Data = responseDtos,
                    Deslocamento = result.Deslocamento,
                    RegistrosRetornado = result.RegistrosRetornado,
                    TotalRegistros = result.TotalRegistros
                };

                return OperationResult<PageResultModel<IEnumerable<MotoResponseDto>>>.Success(pageResult);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PageResultModel<IEnumerable<MotoResponseDto>>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
