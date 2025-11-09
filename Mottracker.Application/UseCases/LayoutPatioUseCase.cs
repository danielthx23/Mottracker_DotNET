using Mottracker.Application.Dtos.LayoutPatio;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Mappers;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using System.Net;

namespace Mottracker.Application.UseCases
{
    public class LayoutPatioUseCase : ILayoutPatioUseCase
    {
        private readonly ILayoutPatioRepository _repository;

        public LayoutPatioUseCase(ILayoutPatioRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<LayoutPatioResponseDto>>>> ObterTodosLayoutsPatiosAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            try
            {
                var result = await _repository.ObterTodasAsync(Deslocamento, RegistrosRetornado);

                if (!result.Data.Any())
                    return OperationResult<PageResultModel<IEnumerable<LayoutPatioResponseDto>>>.Failure("Não foram encontrados layouts de pátio", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(l => l.ToLayoutPatioResponseDto());

                var pageResult = new PageResultModel<IEnumerable<LayoutPatioResponseDto>>
                {
                    Data = responseDtos,
                    Deslocamento = result.Deslocamento,
                    RegistrosRetornado = result.RegistrosRetornado,
                    TotalRegistros = result.TotalRegistros
                };

                return OperationResult<PageResultModel<IEnumerable<LayoutPatioResponseDto>>>.Success(pageResult);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PageResultModel<IEnumerable<LayoutPatioResponseDto>>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<LayoutPatioResponseDto?>> ObterLayoutPatioPorIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<LayoutPatioResponseDto?>.Failure("ID inválido");

                var result = await _repository.ObterPorIdAsync(id);

                if (result is null)
                    return OperationResult<LayoutPatioResponseDto?>.Failure("Layout de pátio não encontrado", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToLayoutPatioResponseDto();

                return OperationResult<LayoutPatioResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<LayoutPatioResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<LayoutPatioResponseDto?>> SalvarLayoutPatioAsync(LayoutPatioRequestDto dto)
        {
            try
            {
                // Validação básica
                if (dto.Largura <= 0)
                    return OperationResult<LayoutPatioResponseDto?>.Failure("Largura deve ser maior que zero");

                if (dto.Comprimento <= 0)
                    return OperationResult<LayoutPatioResponseDto?>.Failure("Comprimento deve ser maior que zero");

                var result = await _repository.SalvarAsync(dto.ToLayoutPatioEntity());

                if (result is null)
                    return OperationResult<LayoutPatioResponseDto?>.Failure(
                        "Não foi possível criar o layout de pátio",
                        (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToLayoutPatioResponseDto();

                return OperationResult<LayoutPatioResponseDto?>.Success(responseDto, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<LayoutPatioResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<LayoutPatioResponseDto?>> EditarLayoutPatioAsync(int id, LayoutPatioRequestDto dto)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<LayoutPatioResponseDto?>.Failure("ID inválido");

                if (dto.Largura <= 0)
                    return OperationResult<LayoutPatioResponseDto?>.Failure("Largura deve ser maior que zero");

                if (dto.Comprimento <= 0)
                    return OperationResult<LayoutPatioResponseDto?>.Failure("Comprimento deve ser maior que zero");

                var layoutExistente = await _repository.ObterPorIdAsync(id);
                if (layoutExistente == null)
                    return OperationResult<LayoutPatioResponseDto?>.Failure("Layout de pátio não encontrado", (int)HttpStatusCode.NotFound);

                layoutExistente.Descricao = dto.Descricao;
                layoutExistente.DataCriacao = dto.DataCriacao;
                layoutExistente.Largura = dto.Largura;
                layoutExistente.Comprimento = dto.Comprimento;
                layoutExistente.Altura = dto.Altura;
                layoutExistente.PatioLayoutPatioId = dto.PatioLayoutPatioId;

                var result = await _repository.AtualizarAsync(layoutExistente);

                if (result is null)
                    return OperationResult<LayoutPatioResponseDto?>.Failure("Não foi possível atualizar o layout de pátio", (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToLayoutPatioResponseDto();

                return OperationResult<LayoutPatioResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<LayoutPatioResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<LayoutPatioResponseDto?>> DeletarLayoutPatioAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<LayoutPatioResponseDto?>.Failure("ID inválido");

                var result = await _repository.DeletarAsync(id);

                if (result is null)
                    return OperationResult<LayoutPatioResponseDto?>.Failure("Layout de pátio não encontrado", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToLayoutPatioResponseDto();

                return OperationResult<LayoutPatioResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<LayoutPatioResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        // Métodos de consulta específicos (sem paginação)
        public async Task<OperationResult<IEnumerable<LayoutPatioResponseDto>>> ObterTodosLayoutsPatiosAsync()
        {
            try
            {
                var result = await _repository.ObterTodasAsync();

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<LayoutPatioResponseDto>>.Failure("Não foram encontrados layouts de pátio", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(l => l.ToLayoutPatioResponseDto());

                return OperationResult<IEnumerable<LayoutPatioResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<LayoutPatioResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<LayoutPatioResponseDto?>> SalvarDadosLayoutPatioAsync(LayoutPatioRequestDto entity)
        {
            try
            {
                // Validação básica
                if (entity.Largura <= 0)
                    return OperationResult<LayoutPatioResponseDto?>.Failure("Largura deve ser maior que zero");

                if (entity.Comprimento <= 0)
                    return OperationResult<LayoutPatioResponseDto?>.Failure("Comprimento deve ser maior que zero");

                var result = await _repository.SalvarAsync(entity.ToLayoutPatioEntity());

                if (result is null)
                    return OperationResult<LayoutPatioResponseDto?>.Failure(
                        "Não foi possível criar o layout de pátio",
                        (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToLayoutPatioResponseDto();

                return OperationResult<LayoutPatioResponseDto?>.Success(responseDto, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<LayoutPatioResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<LayoutPatioResponseDto?>> EditarDadosLayoutPatioAsync(int id, LayoutPatioRequestDto entity)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<LayoutPatioResponseDto?>.Failure("ID inválido");

                if (entity.Largura <= 0)
                    return OperationResult<LayoutPatioResponseDto?>.Failure("Largura deve ser maior que zero");

                if (entity.Comprimento <= 0)
                    return OperationResult<LayoutPatioResponseDto?>.Failure("Comprimento deve ser maior que zero");

                var layoutExistente = await _repository.ObterPorIdAsync(id);
                if (layoutExistente == null)
                    return OperationResult<LayoutPatioResponseDto?>.Failure("Layout de pátio não encontrado", (int)HttpStatusCode.NotFound);

                layoutExistente.Descricao = entity.Descricao;
                layoutExistente.DataCriacao = entity.DataCriacao;
                layoutExistente.Largura = entity.Largura;
                layoutExistente.Comprimento = entity.Comprimento;
                layoutExistente.Altura = entity.Altura;
                layoutExistente.PatioLayoutPatioId = entity.PatioLayoutPatioId;

                var result = await _repository.AtualizarAsync(layoutExistente);

                if (result is null)
                    return OperationResult<LayoutPatioResponseDto?>.Failure("Não foi possível atualizar o layout de pátio", (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToLayoutPatioResponseDto();

                return OperationResult<LayoutPatioResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<LayoutPatioResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<LayoutPatioResponseDto?>> DeletarDadosLayoutPatioAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<LayoutPatioResponseDto?>.Failure("ID inválido");

                var result = await _repository.DeletarAsync(id);

                if (result is null)
                    return OperationResult<LayoutPatioResponseDto?>.Failure("Layout de pátio não encontrado", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToLayoutPatioResponseDto();

                return OperationResult<LayoutPatioResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<LayoutPatioResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<LayoutPatioResponseDto>>> ObterLayoutPatioPorPatioIdAsync(int patioId)
        {
            try
            {
                if (patioId <= 0)
                    return OperationResult<IEnumerable<LayoutPatioResponseDto>>.Failure("ID do pátio inválido");

                var result = await _repository.ObterPorPatioIdAsync(patioId);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<LayoutPatioResponseDto>>.Failure("Não foram encontrados layouts para o pátio especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(l => l.ToLayoutPatioResponseDto());

                return OperationResult<IEnumerable<LayoutPatioResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<LayoutPatioResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<LayoutPatioResponseDto>>> ObterLayoutPatioPorDataCriacaoAsync(DateTime dataCriacao)
        {
            try
            {
                var result = await _repository.ObterPorDataCriacaoAsync(dataCriacao);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<LayoutPatioResponseDto>>.Failure("Não foram encontrados layouts para a data especificada", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(l => l.ToLayoutPatioResponseDto());

                return OperationResult<IEnumerable<LayoutPatioResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<LayoutPatioResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
