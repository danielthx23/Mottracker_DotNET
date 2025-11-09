using Mottracker.Application.Dtos.Camera;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Mappers;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using System.Net;
using Mottracker.Domain.Enums;

namespace Mottracker.Application.UseCases
{
    public class CameraUseCase : ICameraUseCase
    {
        private readonly ICameraRepository _repository;

        public CameraUseCase(ICameraRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<CameraResponseDto>>>> ObterTodasCamerasAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            try
            {
                var result = await _repository.ObterTodasAsync(Deslocamento, RegistrosRetornado);

                if (!result.Data.Any())
                    return OperationResult<PageResultModel<IEnumerable<CameraResponseDto>>>.Failure("Não foram encontradas câmeras", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(c => c.ToCameraResponseDto());

                var pageResult = new PageResultModel<IEnumerable<CameraResponseDto>>
                {
                    Data = responseDtos,
                    Deslocamento = result.Deslocamento,
                    RegistrosRetornado = result.RegistrosRetornado,
                    TotalRegistros = result.TotalRegistros
                };

                return OperationResult<PageResultModel<IEnumerable<CameraResponseDto>>>.Success(pageResult);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PageResultModel<IEnumerable<CameraResponseDto>>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<CameraResponseDto?>> ObterCameraPorIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<CameraResponseDto?>.Failure("ID inválido");

                var result = await _repository.ObterPorIdAsync(id);

                if (result is null)
                    return OperationResult<CameraResponseDto?>.Failure("Câmera não encontrada", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToCameraResponseDto();

                return OperationResult<CameraResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<CameraResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<CameraResponseDto?>> SalvarCameraAsync(CameraRequestDto dto)
        {
            try
            {
                // Validação básica
                if (string.IsNullOrWhiteSpace(dto.NomeCamera))
                    return OperationResult<CameraResponseDto?>.Failure("Nome da câmera é obrigatório");

                var result = await _repository.SalvarAsync(dto.ToCameraEntity());

                if (result is null)
                    return OperationResult<CameraResponseDto?>.Failure(
                        "Não foi possível criar a câmera",
                        (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToCameraResponseDto();

                return OperationResult<CameraResponseDto?>.Success(responseDto, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<CameraResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<CameraResponseDto?>> EditarCameraAsync(int id, CameraRequestDto dto)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<CameraResponseDto?>.Failure("ID inválido");

                if (string.IsNullOrWhiteSpace(dto.NomeCamera))
                    return OperationResult<CameraResponseDto?>.Failure("Nome da câmera é obrigatório");

                var cameraExistente = await _repository.ObterPorIdAsync(id);
                if (cameraExistente == null)
                    return OperationResult<CameraResponseDto?>.Failure("Câmera não encontrada", (int)HttpStatusCode.NotFound);

                cameraExistente.NomeCamera = dto.NomeCamera;
                cameraExistente.IpCamera = dto.IpCamera;
                cameraExistente.Status = dto.Status;
                cameraExistente.PosX = dto.PosX;
                cameraExistente.PosY = dto.PosY;
                cameraExistente.PatioId = dto.PatioId;

                var result = await _repository.AtualizarAsync(cameraExistente);

                if (result is null)
                    return OperationResult<CameraResponseDto?>.Failure("Não foi possível atualizar a câmera", (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToCameraResponseDto();

                return OperationResult<CameraResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<CameraResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<CameraResponseDto?>> DeletarCameraAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<CameraResponseDto?>.Failure("ID inválido");

                var result = await _repository.DeletarAsync(id);

                if (result is null)
                    return OperationResult<CameraResponseDto?>.Failure("Câmera não encontrada", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToCameraResponseDto();

                return OperationResult<CameraResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<CameraResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<CameraResponseDto>>>> ObterCamerasPorNomeAsync(string nome, int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nome))
                    return OperationResult<PageResultModel<IEnumerable<CameraResponseDto>>>.Failure(
                        "Nome é obrigatório");

                var result = await _repository.ObterPorNomeAsync(nome, Deslocamento, RegistrosRetornado);

                if (!result.Data.Any())
                    return OperationResult<PageResultModel<IEnumerable<CameraResponseDto>>>.Failure("Não foram encontradas câmeras para o nome especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(c => c.ToCameraResponseDto());

                var pageResult = new PageResultModel<IEnumerable<CameraResponseDto>>
                {
                    Data = responseDtos,
                    Deslocamento = result.Deslocamento,
                    RegistrosRetornado = result.RegistrosRetornado,
                    TotalRegistros = result.TotalRegistros
                };

                return OperationResult<PageResultModel<IEnumerable<CameraResponseDto>>>.Success(pageResult);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PageResultModel<IEnumerable<CameraResponseDto>>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<CameraResponseDto>>>> ObterCamerasPorStatusAsync(CameraStatus status, int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            try
            {
                var result = await _repository.ObterPorStatusAsync(status, Deslocamento, RegistrosRetornado);

                if (!result.Data.Any())
                    return OperationResult<PageResultModel<IEnumerable<CameraResponseDto>>>.Failure("Não foram encontradas câmeras para o status especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(c => c.ToCameraResponseDto());

                var pageResult = new PageResultModel<IEnumerable<CameraResponseDto>>
                {
                    Data = responseDtos,
                    Deslocamento = result.Deslocamento,
                    RegistrosRetornado = result.RegistrosRetornado,
                    TotalRegistros = result.TotalRegistros
                };

                return OperationResult<PageResultModel<IEnumerable<CameraResponseDto>>>.Success(pageResult);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PageResultModel<IEnumerable<CameraResponseDto>>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
