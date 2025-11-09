using Mottracker.Application.Dtos.Telefone;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Mappers;
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
                if (string.IsNullOrWhiteSpace(dto.Numero))
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

                if (string.IsNullOrWhiteSpace(dto.Numero))
                    return OperationResult<TelefoneResponseDto?>.Failure("Número do telefone é obrigatório");

                var telefoneExistente = await _repository.ObterPorIdAsync(id);
                if (telefoneExistente == null)
                    return OperationResult<TelefoneResponseDto?>.Failure("Telefone não encontrado", (int)HttpStatusCode.NotFound);

                telefoneExistente.Numero = dto.Numero;
                telefoneExistente.Tipo = dto.Tipo;
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

        // Métodos de consulta específicos (sem paginação)
        public async Task<OperationResult<IEnumerable<TelefoneResponseDto>>> ObterTodosTelefonesAsync()
        {
            try
            {
                var result = await _repository.ObterTodasAsync();

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<TelefoneResponseDto>>.Failure("Não foram encontrados telefones", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(t => t.ToTelefoneResponseDto());

                return OperationResult<IEnumerable<TelefoneResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<TelefoneResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<TelefoneResponseDto?>> SalvarDadosTelefoneAsync(TelefoneRequestDto entity)
        {
            try
            {
                // Validação básica
                if (string.IsNullOrWhiteSpace(entity.Numero))
                    return OperationResult<TelefoneResponseDto?>.Failure("Número do telefone é obrigatório");

                var result = await _repository.SalvarAsync(entity.ToTelefoneEntity());

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

        public async Task<OperationResult<TelefoneResponseDto?>> EditarDadosTelefoneAsync(int id, TelefoneRequestDto entity)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<TelefoneResponseDto?>.Failure("ID inválido");

                if (string.IsNullOrWhiteSpace(entity.Numero))
                    return OperationResult<TelefoneResponseDto?>.Failure("Número do telefone é obrigatório");

                var telefoneExistente = await _repository.ObterPorIdAsync(id);
                if (telefoneExistente == null)
                    return OperationResult<TelefoneResponseDto?>.Failure("Telefone não encontrado", (int)HttpStatusCode.NotFound);

                telefoneExistente.Numero = entity.Numero;
                telefoneExistente.Tipo = entity.Tipo;
                telefoneExistente.UsuarioId = entity.UsuarioId;

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

        public async Task<OperationResult<TelefoneResponseDto?>> DeletarDadosTelefoneAsync(int id)
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

        public async Task<OperationResult<IEnumerable<TelefoneResponseDto>>> ObterTelefonePorNumeroAsync(string numero)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numero))
                    return OperationResult<IEnumerable<TelefoneResponseDto>>.Failure("Número do telefone é obrigatório");

                var result = await _repository.ObterPorNumeroAsync(numero);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<TelefoneResponseDto>>.Failure("Não foram encontrados telefones para o número especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(t => t.ToTelefoneResponseDto());

                return OperationResult<IEnumerable<TelefoneResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<TelefoneResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<TelefoneResponseDto>>> ObterTelefonePorUsuarioIdAsync(int usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                    return OperationResult<IEnumerable<TelefoneResponseDto>>.Failure("ID do usuário inválido");

                var result = await _repository.ObterPorUsuarioIdAsync(usuarioId);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<TelefoneResponseDto>>.Failure("Não foram encontrados telefones para o usuário especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(t => t.ToTelefoneResponseDto());

                return OperationResult<IEnumerable<TelefoneResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<TelefoneResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<TelefoneResponseDto>>> ObterTelefonePorTipoAsync(string tipo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tipo))
                    return OperationResult<IEnumerable<TelefoneResponseDto>>.Failure("Tipo do telefone é obrigatório");

                var result = await _repository.ObterPorTipoAsync(tipo);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<TelefoneResponseDto>>.Failure("Não foram encontrados telefones para o tipo especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(t => t.ToTelefoneResponseDto());

                return OperationResult<IEnumerable<TelefoneResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<TelefoneResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
