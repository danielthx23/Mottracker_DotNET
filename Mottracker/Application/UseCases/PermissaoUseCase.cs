using Mottracker.Application.Dtos.Permissao;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Mappers;
using Mottracker.Application.Models;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using System.Net;

namespace Mottracker.Application.UseCases
{
    public class PermissaoUseCase : IPermissaoUseCase
    {
        private readonly IPermissaoRepository _repository;

        public PermissaoUseCase(IPermissaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<PermissaoResponseDto>>>> ObterTodosPermissoesAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            try
            {
                var result = await _repository.ObterTodasAsync(Deslocamento, RegistrosRetornado);

                if (!result.Data.Any())
                    return OperationResult<PageResultModel<IEnumerable<PermissaoResponseDto>>>.Failure("Não foram encontradas permissões", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(p => p.ToPermissaoResponseDto());

                var pageResult = new PageResultModel<IEnumerable<PermissaoResponseDto>>
                {
                    Data = responseDtos,
                    Deslocamento = result.Deslocamento,
                    RegistrosRetornado = result.RegistrosRetornado,
                    TotalRegistros = result.TotalRegistros
                };

                return OperationResult<PageResultModel<IEnumerable<PermissaoResponseDto>>>.Success(pageResult);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PageResultModel<IEnumerable<PermissaoResponseDto>>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<PermissaoResponseDto?>> ObterPermissaoPorIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<PermissaoResponseDto?>.Failure("ID inválido");

                var result = await _repository.ObterPorIdAsync(id);

                if (result is null)
                    return OperationResult<PermissaoResponseDto?>.Failure("Permissão não encontrada", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToPermissaoResponseDto();

                return OperationResult<PermissaoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PermissaoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<PermissaoResponseDto?>> SalvarPermissaoAsync(PermissaoRequestDto dto)
        {
            try
            {
                // Validação básica
                if (string.IsNullOrWhiteSpace(dto.NomePermissao))
                    return OperationResult<PermissaoResponseDto?>.Failure("Nome da permissão é obrigatório");

                var result = await _repository.SalvarAsync(dto.ToPermissaoEntity());

                if (result is null)
                    return OperationResult<PermissaoResponseDto?>.Failure(
                        "Não foi possível criar a permissão",
                        (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToPermissaoResponseDto();

                return OperationResult<PermissaoResponseDto?>.Success(responseDto, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PermissaoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<PermissaoResponseDto?>> EditarPermissaoAsync(int id, PermissaoRequestDto dto)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<PermissaoResponseDto?>.Failure("ID inválido");

                if (string.IsNullOrWhiteSpace(dto.NomePermissao))
                    return OperationResult<PermissaoResponseDto?>.Failure("Nome da permissão é obrigatório");

                var permissaoExistente = await _repository.ObterPorIdAsync(id);
                if (permissaoExistente == null)
                    return OperationResult<PermissaoResponseDto?>.Failure("Permissão não encontrada", (int)HttpStatusCode.NotFound);

                permissaoExistente.NomePermissao = dto.NomePermissao;
                permissaoExistente.Descricao = dto.Descricao;

                var result = await _repository.AtualizarAsync(permissaoExistente);

                if (result is null)
                    return OperationResult<PermissaoResponseDto?>.Failure("Não foi possível atualizar a permissão", (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToPermissaoResponseDto();

                return OperationResult<PermissaoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PermissaoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<PermissaoResponseDto?>> DeletarPermissaoAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<PermissaoResponseDto?>.Failure("ID inválido");

                var result = await _repository.DeletarAsync(id);

                if (result is null)
                    return OperationResult<PermissaoResponseDto?>.Failure("Permissão não encontrada", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToPermissaoResponseDto();

                return OperationResult<PermissaoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PermissaoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
