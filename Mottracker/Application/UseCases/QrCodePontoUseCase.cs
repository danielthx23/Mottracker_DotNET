using Mottracker.Application.Dtos.QrCodePonto;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Mappers;
using Mottracker.Application.Models;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using System.Net;

namespace Mottracker.Application.UseCases
{
    public class QrCodePontoUseCase : IQrCodePontoUseCase
    {
        private readonly IQrCodePontoRepository _repository;

        public QrCodePontoUseCase(IQrCodePontoRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<QrCodePontoResponseDto>>>> ObterTodosQrCodePontosAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            try
            {
                var result = await _repository.ObterTodasAsync(Deslocamento, RegistrosRetornado);

                if (!result.Data.Any())
                    return OperationResult<PageResultModel<IEnumerable<QrCodePontoResponseDto>>>.Failure("Não foram encontrados QR Codes de ponto", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(q => q.ToQrCodePontoResponseDto());

                var pageResult = new PageResultModel<IEnumerable<QrCodePontoResponseDto>>
                {
                    Data = responseDtos,
                    Deslocamento = result.Deslocamento,
                    RegistrosRetornado = result.RegistrosRetornado,
                    TotalRegistros = result.TotalRegistros
                };

                return OperationResult<PageResultModel<IEnumerable<QrCodePontoResponseDto>>>.Success(pageResult);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PageResultModel<IEnumerable<QrCodePontoResponseDto>>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<QrCodePontoResponseDto?>> ObterQrCodePontoPorIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<QrCodePontoResponseDto?>.Failure("ID inválido");

                var result = await _repository.ObterPorIdAsync(id);

                if (result is null)
                    return OperationResult<QrCodePontoResponseDto?>.Failure("QR Code de ponto não encontrado", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToQrCodePontoResponseDto();

                return OperationResult<QrCodePontoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<QrCodePontoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<QrCodePontoResponseDto?>> SalvarQrCodePontoAsync(QrCodePontoRequestDto dto)
        {
            try
            {
                // Validação básica
                if (string.IsNullOrWhiteSpace(dto.IdentificadorQrCodePonto))
                    return OperationResult<QrCodePontoResponseDto?>.Failure("Identificador do QR Code é obrigatório");

                var result = await _repository.SalvarAsync(dto.ToQrCodePontoEntity());

                if (result is null)
                    return OperationResult<QrCodePontoResponseDto?>.Failure(
                        "Não foi possível criar o QR Code de ponto",
                        (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToQrCodePontoResponseDto();

                return OperationResult<QrCodePontoResponseDto?>.Success(responseDto, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<QrCodePontoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<QrCodePontoResponseDto?>> EditarQrCodePontoAsync(int id, QrCodePontoRequestDto dto)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<QrCodePontoResponseDto?>.Failure("ID inválido");

                if (string.IsNullOrWhiteSpace(dto.IdentificadorQrCodePonto))
                    return OperationResult<QrCodePontoResponseDto?>.Failure("Identificador do QR Code é obrigatório");

                var qrCodeExistente = await _repository.ObterPorIdAsync(id);
                if (qrCodeExistente == null)
                    return OperationResult<QrCodePontoResponseDto?>.Failure("QR Code de ponto não encontrado", (int)HttpStatusCode.NotFound);

                qrCodeExistente.IdentificadorQrCode = dto.IdentificadorQrCodePonto;
                qrCodeExistente.PosX = dto.PosXQrCodePonto;
                qrCodeExistente.PosY = dto.PosYQrCodePonto;
                qrCodeExistente.LayoutPatioId = dto.LayoutPatioId;

                var result = await _repository.AtualizarAsync(qrCodeExistente);

                if (result is null)
                    return OperationResult<QrCodePontoResponseDto?>.Failure("Não foi possível atualizar o QR Code de ponto", (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToQrCodePontoResponseDto();

                return OperationResult<QrCodePontoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<QrCodePontoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<QrCodePontoResponseDto?>> DeletarQrCodePontoAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<QrCodePontoResponseDto?>.Failure("ID inválido");

                var result = await _repository.DeletarAsync(id);

                if (result is null)
                    return OperationResult<QrCodePontoResponseDto?>.Failure("QR Code de ponto não encontrado", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToQrCodePontoResponseDto();

                return OperationResult<QrCodePontoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<QrCodePontoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
