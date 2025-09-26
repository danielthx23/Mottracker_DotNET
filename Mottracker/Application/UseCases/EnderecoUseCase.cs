using Mottracker.Application.Dtos.Endereco;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Mappers;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using System.Net;

namespace Mottracker.Application.UseCases
{
    public class EnderecoUseCase : IEnderecoUseCase
    {
        private readonly IEnderecoRepository _repository;

        public EnderecoUseCase(IEnderecoRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<EnderecoResponseDto>>>> ObterTodosEnderecosAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            try
            {
                var result = await _repository.ObterTodasAsync(Deslocamento, RegistrosRetornado);

                if (!result.Data.Any())
                    return OperationResult<PageResultModel<IEnumerable<EnderecoResponseDto>>>.Failure("Não foram encontrados endereços", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(e => e.ToEnderecoResponseDto());

                var pageResult = new PageResultModel<IEnumerable<EnderecoResponseDto>>
                {
                    Data = responseDtos,
                    Deslocamento = result.Deslocamento,
                    RegistrosRetornado = result.RegistrosRetornado,
                    TotalRegistros = result.TotalRegistros
                };

                return OperationResult<PageResultModel<IEnumerable<EnderecoResponseDto>>>.Success(pageResult);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PageResultModel<IEnumerable<EnderecoResponseDto>>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<EnderecoResponseDto?>> ObterEnderecoPorIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<EnderecoResponseDto?>.Failure("ID inválido");

                var result = await _repository.ObterPorIdAsync(id);

                if (result is null)
                    return OperationResult<EnderecoResponseDto?>.Failure("Endereço não encontrado", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToEnderecoResponseDto();

                return OperationResult<EnderecoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<EnderecoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<EnderecoResponseDto?>> SalvarEnderecoAsync(EnderecoRequestDto dto)
        {
            try
            {
                // Validação básica
                if (string.IsNullOrWhiteSpace(dto.Logradouro))
                    return OperationResult<EnderecoResponseDto?>.Failure("Logradouro é obrigatório");

                if (string.IsNullOrWhiteSpace(dto.CEP))
                    return OperationResult<EnderecoResponseDto?>.Failure("CEP é obrigatório");

                var result = await _repository.SalvarAsync(dto.ToEnderecoEntity());

                if (result is null)
                    return OperationResult<EnderecoResponseDto?>.Failure(
                        "Não foi possível criar o endereço",
                        (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToEnderecoResponseDto();

                return OperationResult<EnderecoResponseDto?>.Success(responseDto, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<EnderecoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<EnderecoResponseDto?>> EditarEnderecoAsync(int id, EnderecoRequestDto dto)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<EnderecoResponseDto?>.Failure("ID inválido");

                if (string.IsNullOrWhiteSpace(dto.Logradouro))
                    return OperationResult<EnderecoResponseDto?>.Failure("Logradouro é obrigatório");

                if (string.IsNullOrWhiteSpace(dto.CEP))
                    return OperationResult<EnderecoResponseDto?>.Failure("CEP é obrigatório");

                var enderecoExistente = await _repository.ObterPorIdAsync(id);
                if (enderecoExistente == null)
                    return OperationResult<EnderecoResponseDto?>.Failure("Endereço não encontrado", (int)HttpStatusCode.NotFound);

                enderecoExistente.Logradouro = dto.Logradouro;
                enderecoExistente.Numero = dto.Numero;
                enderecoExistente.Complemento = dto.Complemento;
                enderecoExistente.Bairro = dto.Bairro;
                enderecoExistente.Cidade = dto.Cidade;
                enderecoExistente.Estado = dto.Estado;
                enderecoExistente.CEP = dto.CEP;
                enderecoExistente.Referencia = dto.Referencia;
                enderecoExistente.PatioEnderecoId = dto.PatioEnderecoId;

                var result = await _repository.AtualizarAsync(enderecoExistente);

                if (result is null)
                    return OperationResult<EnderecoResponseDto?>.Failure("Não foi possível atualizar o endereço", (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToEnderecoResponseDto();

                return OperationResult<EnderecoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<EnderecoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<EnderecoResponseDto?>> DeletarEnderecoAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<EnderecoResponseDto?>.Failure("ID inválido");

                var result = await _repository.DeletarAsync(id);

                if (result is null)
                    return OperationResult<EnderecoResponseDto?>.Failure("Endereço não encontrado", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToEnderecoResponseDto();

                return OperationResult<EnderecoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<EnderecoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        // Métodos de consulta específicos (sem paginação)
        public async Task<OperationResult<IEnumerable<EnderecoResponseDto>>> ObterTodosEnderecosAsync()
        {
            try
            {
                var result = await _repository.ObterTodasAsync();

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Não foram encontrados endereços", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(e => e.ToEnderecoResponseDto());

                return OperationResult<IEnumerable<EnderecoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<EnderecoResponseDto?>> SalvarDadosEnderecoAsync(EnderecoRequestDto entity)
        {
            try
            {
                // Validação básica
                if (string.IsNullOrWhiteSpace(entity.Logradouro))
                    return OperationResult<EnderecoResponseDto?>.Failure("Logradouro é obrigatório");

                if (string.IsNullOrWhiteSpace(entity.CEP))
                    return OperationResult<EnderecoResponseDto?>.Failure("CEP é obrigatório");

                var result = await _repository.SalvarAsync(entity.ToEnderecoEntity());

                if (result is null)
                    return OperationResult<EnderecoResponseDto?>.Failure(
                        "Não foi possível criar o endereço",
                        (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToEnderecoResponseDto();

                return OperationResult<EnderecoResponseDto?>.Success(responseDto, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<EnderecoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<EnderecoResponseDto?>> EditarDadosEnderecoAsync(int id, EnderecoRequestDto entity)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<EnderecoResponseDto?>.Failure("ID inválido");

                if (string.IsNullOrWhiteSpace(entity.Logradouro))
                    return OperationResult<EnderecoResponseDto?>.Failure("Logradouro é obrigatório");

                if (string.IsNullOrWhiteSpace(entity.CEP))
                    return OperationResult<EnderecoResponseDto?>.Failure("CEP é obrigatório");

                var enderecoExistente = await _repository.ObterPorIdAsync(id);
                if (enderecoExistente == null)
                    return OperationResult<EnderecoResponseDto?>.Failure("Endereço não encontrado", (int)HttpStatusCode.NotFound);

                enderecoExistente.Logradouro = entity.Logradouro;
                enderecoExistente.Numero = entity.Numero;
                enderecoExistente.Complemento = entity.Complemento;
                enderecoExistente.Bairro = entity.Bairro;
                enderecoExistente.Cidade = entity.Cidade;
                enderecoExistente.Estado = entity.Estado;
                enderecoExistente.CEP = entity.CEP;
                enderecoExistente.Referencia = entity.Referencia;
                enderecoExistente.PatioEnderecoId = entity.PatioEnderecoId;

                var result = await _repository.AtualizarAsync(enderecoExistente);

                if (result is null)
                    return OperationResult<EnderecoResponseDto?>.Failure("Não foi possível atualizar o endereço", (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToEnderecoResponseDto();

                return OperationResult<EnderecoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<EnderecoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<EnderecoResponseDto?>> DeletarDadosEnderecoAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<EnderecoResponseDto?>.Failure("ID inválido");

                var result = await _repository.DeletarAsync(id);

                if (result is null)
                    return OperationResult<EnderecoResponseDto?>.Failure("Endereço não encontrado", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToEnderecoResponseDto();

                return OperationResult<EnderecoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<EnderecoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<EnderecoResponseDto>>> ObterEnderecoPorCepAsync(string cep)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cep))
                    return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("CEP é obrigatório");

                var result = await _repository.ObterPorCepAsync(cep);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Não foram encontrados endereços para o CEP especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(e => e.ToEnderecoResponseDto());

                return OperationResult<IEnumerable<EnderecoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<EnderecoResponseDto>>> ObterEnderecoPorEstadoAsync(string estado)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(estado))
                    return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Estado é obrigatório");

                var result = await _repository.ObterPorEstadoAsync(estado);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Não foram encontrados endereços para o estado especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(e => e.ToEnderecoResponseDto());

                return OperationResult<IEnumerable<EnderecoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<EnderecoResponseDto>>> ObterEnderecoPorCidadeAsync(string cidade)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cidade))
                    return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Cidade é obrigatória");

                var result = await _repository.ObterPorCidadeAsync(cidade);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Não foram encontrados endereços para a cidade especificada", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(e => e.ToEnderecoResponseDto());

                return OperationResult<IEnumerable<EnderecoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<EnderecoResponseDto>>> ObterEnderecoPorCidadeEstadoAsync(string cidade, string estado)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cidade))
                    return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Cidade é obrigatória");

                if (string.IsNullOrWhiteSpace(estado))
                    return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Estado é obrigatório");

                var result = await _repository.ObterPorCidadeEstadoAsync(cidade, estado);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Não foram encontrados endereços para a cidade e estado especificados", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(e => e.ToEnderecoResponseDto());

                return OperationResult<IEnumerable<EnderecoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<EnderecoResponseDto>>> ObterEnderecoPorBairroAsync(string bairro)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(bairro))
                    return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Bairro é obrigatório");

                var result = await _repository.ObterPorBairroAsync(bairro);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Não foram encontrados endereços para o bairro especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(e => e.ToEnderecoResponseDto());

                return OperationResult<IEnumerable<EnderecoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<EnderecoResponseDto>>> ObterEnderecoPorLogradouroContendoAsync(string logradouro)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(logradouro))
                    return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Logradouro é obrigatório");

                var result = await _repository.ObterPorLogradouroContendoAsync(logradouro);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Não foram encontrados endereços para o logradouro especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(e => e.ToEnderecoResponseDto());

                return OperationResult<IEnumerable<EnderecoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<EnderecoResponseDto>>> ObterEnderecoPorPatioIdAsync(int patioId)
        {
            try
            {
                if (patioId <= 0)
                    return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("ID do pátio inválido");

                var result = await _repository.ObterPorPatioIdAsync(patioId);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Não foram encontrados endereços para o pátio especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(e => e.ToEnderecoResponseDto());

                return OperationResult<IEnumerable<EnderecoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<EnderecoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
