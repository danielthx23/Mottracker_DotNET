using Mottracker.Application.Dtos.UsuarioPermissao;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Mappers;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using System.Net;

namespace Mottracker.Application.UseCases
{
    public class UsuarioPermissaoUseCase : IUsuarioPermissaoUseCase
    {
        private readonly IUsuarioPermissaoRepository _repository;

        public UsuarioPermissaoUseCase(IUsuarioPermissaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<UsuarioPermissaoResponseDto>>>> ObterTodosUsuarioPermissoesAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            try
            {
                var result = await _repository.ObterTodasAsync(Deslocamento, RegistrosRetornado);

                if (!result.Data.Any())
                    return OperationResult<PageResultModel<IEnumerable<UsuarioPermissaoResponseDto>>>.Failure("Não foram encontradas permissões de usuário", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(up => up.ToUsuarioPermissaoResponseDto());

                var pageResult = new PageResultModel<IEnumerable<UsuarioPermissaoResponseDto>>
                {
                    Data = responseDtos,
                    Deslocamento = result.Deslocamento,
                    RegistrosRetornado = result.RegistrosRetornado,
                    TotalRegistros = result.TotalRegistros
                };

                return OperationResult<PageResultModel<IEnumerable<UsuarioPermissaoResponseDto>>>.Success(pageResult);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PageResultModel<IEnumerable<UsuarioPermissaoResponseDto>>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<UsuarioPermissaoResponseDto?>> ObterUsuarioPermissaoPorIdAsync(int usuarioId, int permissaoId)
        {
            try
            {
                if (usuarioId <= 0 || permissaoId <= 0)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure("IDs inválidos");

                var result = await _repository.ObterPorIdAsync(usuarioId, permissaoId);

                if (result is null)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure("Permissão de usuário não encontrada", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToUsuarioPermissaoResponseDto();

                return OperationResult<UsuarioPermissaoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<UsuarioPermissaoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<UsuarioPermissaoResponseDto?>> SalvarUsuarioPermissaoAsync(UsuarioPermissaoRequestDto dto)
        {
            try
            {
                // Validação básica
                if (dto.IdUsuario <= 0)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure("ID do usuário é obrigatório");

                if (dto.IdPermissao <= 0)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure("ID da permissão é obrigatório");

                var result = await _repository.SalvarAsync(dto.ToUsuarioPermissaoEntity());

                if (result is null)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure(
                        "Não foi possível criar a permissão de usuário",
                        (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToUsuarioPermissaoResponseDto();

                return OperationResult<UsuarioPermissaoResponseDto?>.Success(responseDto, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<UsuarioPermissaoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<UsuarioPermissaoResponseDto?>> EditarUsuarioPermissaoAsync(int usuarioId, int permissaoId, UsuarioPermissaoRequestDto dto)
        {
            try
            {
                if (usuarioId <= 0 || permissaoId <= 0)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure("IDs inválidos");

                if (dto.IdUsuario <= 0)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure("ID do usuário é obrigatório");

                if (dto.IdPermissao <= 0)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure("ID da permissão é obrigatório");

                var usuarioPermissaoExistente = await _repository.ObterPorIdAsync(usuarioId, permissaoId);
                if (usuarioPermissaoExistente == null)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure("Permissão de usuário não encontrada", (int)HttpStatusCode.NotFound);

                usuarioPermissaoExistente.UsuarioId = dto.IdUsuario;
                usuarioPermissaoExistente.PermissaoId = dto.IdPermissao;
                usuarioPermissaoExistente.Papel = dto.PapelUsuarioPermissao;

                var result = await _repository.AtualizarAsync(usuarioPermissaoExistente);

                if (result is null)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure("Não foi possível atualizar a permissão de usuário", (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToUsuarioPermissaoResponseDto();

                return OperationResult<UsuarioPermissaoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<UsuarioPermissaoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<UsuarioPermissaoResponseDto?>> DeletarUsuarioPermissaoAsync(int usuarioId, int permissaoId)
        {
            try
            {
                if (usuarioId <= 0 || permissaoId <= 0)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure("IDs inválidos");

                var result = await _repository.DeletarAsync(usuarioId, permissaoId);

                if (result is null)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure("Permissão de usuário não encontrada", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToUsuarioPermissaoResponseDto();

                return OperationResult<UsuarioPermissaoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<UsuarioPermissaoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        // Métodos de consulta específicos (sem paginação)
        public async Task<OperationResult<IEnumerable<UsuarioPermissaoResponseDto>>> ObterTodosUsuarioPermissoesAsync()
        {
            try
            {
                var result = await _repository.ObterTodasAsync();

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<UsuarioPermissaoResponseDto>>.Failure("Não foram encontradas permissões de usuário", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(up => up.ToUsuarioPermissaoResponseDto());

                return OperationResult<IEnumerable<UsuarioPermissaoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<UsuarioPermissaoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<UsuarioPermissaoResponseDto>>> ObterUsuarioPermissoesPorUsuarioIdAsync(long usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                    return OperationResult<IEnumerable<UsuarioPermissaoResponseDto>>.Failure("ID do usuário inválido");

                var result = await _repository.ObterPorUsuarioIdAsync(usuarioId);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<UsuarioPermissaoResponseDto>>.Failure("Não foram encontradas permissões para o usuário especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(up => up.ToUsuarioPermissaoResponseDto());

                return OperationResult<IEnumerable<UsuarioPermissaoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<UsuarioPermissaoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<UsuarioPermissaoResponseDto>>> ObterUsuarioPermissoesPorPermissaoIdAsync(long permissaoId)
        {
            try
            {
                if (permissaoId <= 0)
                    return OperationResult<IEnumerable<UsuarioPermissaoResponseDto>>.Failure("ID da permissão inválido");

                var result = await _repository.ObterPorPermissaoIdAsync(permissaoId);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<UsuarioPermissaoResponseDto>>.Failure("Não foram encontradas permissões para a permissão especificada", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(up => up.ToUsuarioPermissaoResponseDto());

                return OperationResult<IEnumerable<UsuarioPermissaoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<UsuarioPermissaoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<UsuarioPermissaoResponseDto?>> SalvarDadosUsuarioPermissaoAsync(UsuarioPermissaoRequestDto entity)
        {
            try
            {
                // Validação básica
                if (entity.IdUsuario <= 0)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure("ID do usuário é obrigatório");

                if (entity.IdPermissao <= 0)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure("ID da permissão é obrigatório");

                var result = await _repository.SalvarAsync(entity.ToUsuarioPermissaoEntity());

                if (result is null)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure(
                        "Não foi possível criar a permissão de usuário",
                        (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToUsuarioPermissaoResponseDto();

                return OperationResult<UsuarioPermissaoResponseDto?>.Success(responseDto, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<UsuarioPermissaoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<UsuarioPermissaoResponseDto?>> EditarDadosUsuarioPermissaoAsync(int usuarioId, int permissaoId, UsuarioPermissaoRequestDto entity)
        {
            try
            {
                if (usuarioId <= 0 || permissaoId <= 0)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure("IDs inválidos");

                if (entity.IdUsuario <= 0)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure("ID do usuário é obrigatório");

                if (entity.IdPermissao <= 0)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure("ID da permissão é obrigatório");

                var usuarioPermissaoExistente = await _repository.ObterPorIdAsync(usuarioId, permissaoId);
                if (usuarioPermissaoExistente == null)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure("Permissão de usuário não encontrada", (int)HttpStatusCode.NotFound);

                usuarioPermissaoExistente.UsuarioId = entity.IdUsuario;
                usuarioPermissaoExistente.PermissaoId = entity.IdPermissao;
                usuarioPermissaoExistente.Papel = entity.PapelUsuarioPermissao;

                var result = await _repository.AtualizarAsync(usuarioPermissaoExistente);

                if (result is null)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure("Não foi possível atualizar a permissão de usuário", (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToUsuarioPermissaoResponseDto();

                return OperationResult<UsuarioPermissaoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<UsuarioPermissaoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<UsuarioPermissaoResponseDto?>> DeletarDadosUsuarioPermissaoAsync(int usuarioId, int permissaoId)
        {
            try
            {
                if (usuarioId <= 0 || permissaoId <= 0)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure("IDs inválidos");

                var result = await _repository.DeletarAsync(usuarioId, permissaoId);

                if (result is null)
                    return OperationResult<UsuarioPermissaoResponseDto?>.Failure("Permissão de usuário não encontrada", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToUsuarioPermissaoResponseDto();

                return OperationResult<UsuarioPermissaoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<UsuarioPermissaoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
