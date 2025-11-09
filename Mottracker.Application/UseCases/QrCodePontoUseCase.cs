using Mottracker.Application.Dtos.QrCodePonto;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Mappers;
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
                if (string.IsNullOrWhiteSpace(dto.IdentificadorQrCode))
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

                if (string.IsNullOrWhiteSpace(dto.IdentificadorQrCode))
                    return OperationResult<QrCodePontoResponseDto?>.Failure("Identificador do QR Code é obrigatório");

                var qrCodeExistente = await _repository.ObterPorIdAsync(id);
                if (qrCodeExistente == null)
                    return OperationResult<QrCodePontoResponseDto?>.Failure("QR Code de ponto não encontrado", (int)HttpStatusCode.NotFound);

                qrCodeExistente.IdentificadorQrCode = dto.IdentificadorQrCode;
                qrCodeExistente.PosX = dto.PosX;
                qrCodeExistente.PosY = dto.PosY;
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

        // Métodos de consulta específicos (sem paginação)
        public async Task<OperationResult<IEnumerable<QrCodePontoResponseDto>>> ObterTodosQrCodePontosAsync()
        {
            try
            {
                var result = await _repository.ObterTodasAsync();

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<QrCodePontoResponseDto>>.Failure("Não foram encontrados QR Codes de ponto", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(q => q.ToQrCodePontoResponseDto());

                return OperationResult<IEnumerable<QrCodePontoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<QrCodePontoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<QrCodePontoResponseDto?>> SalvarDadosQrCodePontoAsync(QrCodePontoRequestDto entity)
        {
            try
            {
                // Validação básica
                if (string.IsNullOrWhiteSpace(entity.IdentificadorQrCode))
                    return OperationResult<QrCodePontoResponseDto?>.Failure("Identificador do QR Code é obrigatório");

                var result = await _repository.SalvarAsync(entity.ToQrCodePontoEntity());

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

        public async Task<OperationResult<QrCodePontoResponseDto?>> EditarDadosQrCodePontoAsync(int id, QrCodePontoRequestDto entity)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<QrCodePontoResponseDto?>.Failure("ID inválido");

                if (string.IsNullOrWhiteSpace(entity.IdentificadorQrCode))
                    return OperationResult<QrCodePontoResponseDto?>.Failure("Identificador do QR Code é obrigatório");

                var qrCodeExistente = await _repository.ObterPorIdAsync(id);
                if (qrCodeExistente == null)
                    return OperationResult<QrCodePontoResponseDto?>.Failure("QR Code de ponto não encontrado", (int)HttpStatusCode.NotFound);

                qrCodeExistente.IdentificadorQrCode = entity.IdentificadorQrCode;
                qrCodeExistente.PosX = entity.PosX;
                qrCodeExistente.PosY = entity.PosY;
                qrCodeExistente.LayoutPatioId = entity.LayoutPatioId;

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

        public async Task<OperationResult<QrCodePontoResponseDto?>> DeletarDadosQrCodePontoAsync(int id)
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

        public async Task<OperationResult<IEnumerable<QrCodePontoResponseDto>>> ObterQrCodePontoPorIdentificadorAsync(string identificador)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(identificador))
                    return OperationResult<IEnumerable<QrCodePontoResponseDto>>.Failure("Identificador do QR Code é obrigatório");

                var result = await _repository.ObterPorIdentificadorAsync(identificador);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<QrCodePontoResponseDto>>.Failure("Não foram encontrados QR Codes para o identificador especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(q => q.ToQrCodePontoResponseDto());

                return OperationResult<IEnumerable<QrCodePontoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<QrCodePontoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<QrCodePontoResponseDto>>> ObterQrCodePontoPorLayoutPatioIdAsync(int layoutPatioId)
        {
            try
            {
                if (layoutPatioId <= 0)
                    return OperationResult<IEnumerable<QrCodePontoResponseDto>>.Failure("ID do layout de pátio inválido");

                var result = await _repository.ObterPorLayoutPatioIdAsync(layoutPatioId);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<QrCodePontoResponseDto>>.Failure("Não foram encontrados QR Codes para o layout de pátio especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(q => q.ToQrCodePontoResponseDto());

                return OperationResult<IEnumerable<QrCodePontoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<QrCodePontoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<QrCodePontoResponseDto>>> ObterQrCodePontoPorPosXRangeAsync(float posXMin, float posXMax)
        {
            try
            {
                if (posXMin > posXMax)
                    return OperationResult<IEnumerable<QrCodePontoResponseDto>>.Failure("Posição X mínima deve ser menor ou igual à máxima");

                var result = await _repository.ObterPorPosXRangeAsync(posXMin, posXMax);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<QrCodePontoResponseDto>>.Failure("Não foram encontrados QR Codes no range de posição X especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(q => q.ToQrCodePontoResponseDto());

                return OperationResult<IEnumerable<QrCodePontoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<QrCodePontoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<QrCodePontoResponseDto>>> ObterQrCodePontoPorPosYRangeAsync(float posYMin, float posYMax)
        {
            try
            {
                if (posYMin > posYMax)
                    return OperationResult<IEnumerable<QrCodePontoResponseDto>>.Failure("Posição Y mínima deve ser menor ou igual à máxima");

                var result = await _repository.ObterPorPosYRangeAsync(posYMin, posYMax);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<QrCodePontoResponseDto>>.Failure("Não foram encontrados QR Codes no range de posição Y especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(q => q.ToQrCodePontoResponseDto());

                return OperationResult<IEnumerable<QrCodePontoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<QrCodePontoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
