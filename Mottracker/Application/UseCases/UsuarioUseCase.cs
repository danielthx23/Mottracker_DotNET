using Mottracker.Application.Dtos.Usuario;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Mappers;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using System.Net;

namespace Mottracker.Application.UseCases
{
    public class UsuarioUseCase : IUsuarioUseCase
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioUseCase(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<UsuarioResponseDto>>>> ObterTodosUsuariosAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            try
            {
                var result = await _repository.ObterTodasAsync(Deslocamento, RegistrosRetornado);

                if (!result.Data.Any())
                    return OperationResult<PageResultModel<IEnumerable<UsuarioResponseDto>>>.Failure("Não foram encontrados usuários", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(u => u.ToUsuarioResponseDto());

                var pageResult = new PageResultModel<IEnumerable<UsuarioResponseDto>>
                {
                    Data = responseDtos,
                    Deslocamento = result.Deslocamento,
                    RegistrosRetornado = result.RegistrosRetornado,
                    TotalRegistros = result.TotalRegistros
                };

                return OperationResult<PageResultModel<IEnumerable<UsuarioResponseDto>>>.Success(pageResult);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PageResultModel<IEnumerable<UsuarioResponseDto>>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<UsuarioResponseDto?>> ObterUsuarioPorIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<UsuarioResponseDto?>.Failure("ID inválido");

                var result = await _repository.ObterPorIdAsync(id);

                if (result is null)
                    return OperationResult<UsuarioResponseDto?>.Failure("Usuário não encontrado", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToUsuarioResponseDto();

                return OperationResult<UsuarioResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<UsuarioResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<UsuarioResponseDto?>> ObterUsuarioPorEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return OperationResult<UsuarioResponseDto?>.Failure("Email é obrigatório");

                var result = await _repository.ObterPorEmailAsync(email);

                if (result is null)
                    return OperationResult<UsuarioResponseDto?>.Failure("Usuário não encontrado", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToUsuarioResponseDto();

                return OperationResult<UsuarioResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<UsuarioResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<UsuarioResponseDto?>> SalvarUsuarioAsync(UsuarioRequestDto dto)
        {
            try
            {
                // Validação básica
                if (string.IsNullOrWhiteSpace(dto.NomeUsuario))
                    return OperationResult<UsuarioResponseDto?>.Failure("Nome do usuário é obrigatório");

                if (string.IsNullOrWhiteSpace(dto.EmailUsuario))
                    return OperationResult<UsuarioResponseDto?>.Failure("Email é obrigatório");

                var result = await _repository.SalvarAsync(dto.ToUsuarioEntity());

                if (result is null)
                    return OperationResult<UsuarioResponseDto?>.Failure(
                        "Não foi possível criar o usuário",
                        (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToUsuarioResponseDto();

                return OperationResult<UsuarioResponseDto?>.Success(responseDto, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<UsuarioResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<UsuarioResponseDto?>> EditarUsuarioAsync(int id, UsuarioRequestDto dto)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<UsuarioResponseDto?>.Failure("ID inválido");

                if (string.IsNullOrWhiteSpace(dto.NomeUsuario))
                    return OperationResult<UsuarioResponseDto?>.Failure("Nome do usuário é obrigatório");

                if (string.IsNullOrWhiteSpace(dto.EmailUsuario))
                    return OperationResult<UsuarioResponseDto?>.Failure("Email é obrigatório");

                var usuarioExistente = await _repository.ObterPorIdAsync(id);
                if (usuarioExistente == null)
                    return OperationResult<UsuarioResponseDto?>.Failure("Usuário não encontrado", (int)HttpStatusCode.NotFound);

                usuarioExistente.NomeUsuario = dto.NomeUsuario;
                usuarioExistente.CPFUsuario = dto.CPFUsuario;
                usuarioExistente.SenhaUsuario = dto.SenhaUsuario;
                usuarioExistente.CNHUsuario = dto.CNHUsuario;
                usuarioExistente.EmailUsuario = dto.EmailUsuario;
                usuarioExistente.TokenUsuario = dto.TokenUsuario;
                usuarioExistente.DataNascimentoUsuario = dto.DataNascimentoUsuario;
                usuarioExistente.CriadoEmUsuario = dto.CriadoEmUsuario;

                var result = await _repository.AtualizarAsync(usuarioExistente);

                if (result is null)
                    return OperationResult<UsuarioResponseDto?>.Failure("Não foi possível atualizar o usuário", (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToUsuarioResponseDto();

                return OperationResult<UsuarioResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<UsuarioResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<UsuarioResponseDto?>> DeletarUsuarioAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<UsuarioResponseDto?>.Failure("ID inválido");

                var result = await _repository.DeletarAsync(id);

                if (result is null)
                    return OperationResult<UsuarioResponseDto?>.Failure("Usuário não encontrado", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToUsuarioResponseDto();

                return OperationResult<UsuarioResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<UsuarioResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        // Métodos de consulta específicos (sem paginação)
        public async Task<OperationResult<IEnumerable<UsuarioResponseDto>>> ObterTodosUsuariosAsync()
        {
            try
            {
                var result = await _repository.ObterTodasAsync();

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<UsuarioResponseDto>>.Failure("Não foram encontrados usuários", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(u => u.ToUsuarioResponseDto());

                return OperationResult<IEnumerable<UsuarioResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<UsuarioResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<UsuarioResponseDto?>> SalvarDadosUsuarioAsync(UsuarioRequestDto entity)
        {
            try
            {
                // Validação básica
                if (string.IsNullOrWhiteSpace(entity.NomeUsuario))
                    return OperationResult<UsuarioResponseDto?>.Failure("Nome do usuário é obrigatório");

                if (string.IsNullOrWhiteSpace(entity.EmailUsuario))
                    return OperationResult<UsuarioResponseDto?>.Failure("Email é obrigatório");

                var result = await _repository.SalvarAsync(entity.ToUsuarioEntity());

                if (result is null)
                    return OperationResult<UsuarioResponseDto?>.Failure(
                        "Não foi possível criar o usuário",
                        (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToUsuarioResponseDto();

                return OperationResult<UsuarioResponseDto?>.Success(responseDto, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<UsuarioResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<UsuarioResponseDto?>> EditarDadosUsuarioAsync(int id, UsuarioRequestDto entity)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<UsuarioResponseDto?>.Failure("ID inválido");

                if (string.IsNullOrWhiteSpace(entity.NomeUsuario))
                    return OperationResult<UsuarioResponseDto?>.Failure("Nome do usuário é obrigatório");

                if (string.IsNullOrWhiteSpace(entity.EmailUsuario))
                    return OperationResult<UsuarioResponseDto?>.Failure("Email é obrigatório");

                var usuarioExistente = await _repository.ObterPorIdAsync(id);
                if (usuarioExistente == null)
                    return OperationResult<UsuarioResponseDto?>.Failure("Usuário não encontrado", (int)HttpStatusCode.NotFound);

                usuarioExistente.NomeUsuario = entity.NomeUsuario;
                usuarioExistente.CPFUsuario = entity.CPFUsuario;
                usuarioExistente.SenhaUsuario = entity.SenhaUsuario;
                usuarioExistente.CNHUsuario = entity.CNHUsuario;
                usuarioExistente.EmailUsuario = entity.EmailUsuario;
                usuarioExistente.TokenUsuario = entity.TokenUsuario;
                usuarioExistente.DataNascimentoUsuario = entity.DataNascimentoUsuario;
                usuarioExistente.CriadoEmUsuario = entity.CriadoEmUsuario;

                var result = await _repository.AtualizarAsync(usuarioExistente);

                if (result is null)
                    return OperationResult<UsuarioResponseDto?>.Failure("Não foi possível atualizar o usuário", (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToUsuarioResponseDto();

                return OperationResult<UsuarioResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<UsuarioResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<UsuarioResponseDto?>> DeletarDadosUsuarioAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<UsuarioResponseDto?>.Failure("ID inválido");

                var result = await _repository.DeletarAsync(id);

                if (result is null)
                    return OperationResult<UsuarioResponseDto?>.Failure("Usuário não encontrado", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToUsuarioResponseDto();

                return OperationResult<UsuarioResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<UsuarioResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
