using Mottracker.Application.Dtos.Contrato;
using Mottracker.Application.Interfaces;
using Mottracker.Application.Mappers;

using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using System.Net;

namespace Mottracker.Application.UseCases
{
    public class ContratoUseCase : IContratoUseCase
    {
        private readonly IContratoRepository _repository;

        public ContratoUseCase(IContratoRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<ContratoResponseDto>>>> ObterTodosContratosAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            try
            {
                var result = await _repository.ObterTodasAsync(Deslocamento, RegistrosRetornado);

                if (!result.Data.Any())
                    return OperationResult<PageResultModel<IEnumerable<ContratoResponseDto>>>.Failure("Não foram encontrados contratos", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(c => c.ToContratoResponseDto());

                var pageResult = new PageResultModel<IEnumerable<ContratoResponseDto>>
                {
                    Data = responseDtos,
                    Deslocamento = result.Deslocamento,
                    RegistrosRetornado = result.RegistrosRetornado,
                    TotalRegistros = result.TotalRegistros
                };

                return OperationResult<PageResultModel<IEnumerable<ContratoResponseDto>>>.Success(pageResult);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PageResultModel<IEnumerable<ContratoResponseDto>>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<ContratoResponseDto?>> ObterContratoPorIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<ContratoResponseDto?>.Failure("ID inválido");

                var result = await _repository.ObterPorIdAsync(id);

                if (result is null)
                    return OperationResult<ContratoResponseDto?>.Failure("Contrato não encontrado", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToContratoResponseDto();

                return OperationResult<ContratoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<ContratoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<ContratoResponseDto?>> SalvarContratoAsync(ContratoRequestDto dto)
        {
            try
            {
                // Validação básica
                if (dto.ValorToralContrato <= 0)
                    return OperationResult<ContratoResponseDto?>.Failure("Valor total do contrato deve ser maior que zero");

                var result = await _repository.SalvarAsync(dto.ToContratoEntity());

                if (result is null)
                    return OperationResult<ContratoResponseDto?>.Failure(
                        "Não foi possível criar o contrato",
                        (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToContratoResponseDto();

                return OperationResult<ContratoResponseDto?>.Success(responseDto, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<ContratoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<ContratoResponseDto?>> EditarContratoAsync(int id, ContratoRequestDto dto)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<ContratoResponseDto?>.Failure("ID inválido");

                if (dto.ValorToralContrato <= 0)
                    return OperationResult<ContratoResponseDto?>.Failure("Valor total do contrato deve ser maior que zero");

                var contratoExistente = await _repository.ObterPorIdAsync(id);
                if (contratoExistente == null)
                    return OperationResult<ContratoResponseDto?>.Failure("Contrato não encontrado", (int)HttpStatusCode.NotFound);

                contratoExistente.ClausulasContrato = dto.ClausulasContrato;
                contratoExistente.DataDeEntradaContrato = dto.DataDeEntradaContrato;
                contratoExistente.HorarioDeDevolucaoContrato = dto.HorarioDeDevolucaoContrato;
                contratoExistente.DataDeExpiracaoContrato = dto.DataDeExpiracaoContrato;
                contratoExistente.RenovacaoAutomaticaContrato = dto.RenovacaoAutomaticaContrato;
                contratoExistente.DataUltimaRenovacaoContrato = dto.DataUltimaRenovacaoContrato;
                contratoExistente.NumeroRenovacoesContrato = dto.NumeroRenovacoesContrato;
                contratoExistente.AtivoContrato = dto.AtivoContrato;
                contratoExistente.ValorToralContrato = dto.ValorToralContrato;
                contratoExistente.QuantidadeParcelas = dto.QuantidadeParcelas;
                contratoExistente.UsuarioContratoId = dto.UsuarioContratoId;

                var result = await _repository.AtualizarAsync(contratoExistente);

                if (result is null)
                    return OperationResult<ContratoResponseDto?>.Failure("Não foi possível atualizar o contrato", (int)HttpStatusCode.UnprocessableEntity);

                var responseDto = result.ToContratoResponseDto();

                return OperationResult<ContratoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<ContratoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<ContratoResponseDto?>> DeletarContratoAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return OperationResult<ContratoResponseDto?>.Failure("ID inválido");

                var result = await _repository.DeletarAsync(id);

                if (result is null)
                    return OperationResult<ContratoResponseDto?>.Failure("Contrato não encontrado", (int)HttpStatusCode.NotFound);

                var responseDto = result.ToContratoResponseDto();

                return OperationResult<ContratoResponseDto?>.Success(responseDto);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<ContratoResponseDto?>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<ContratoResponseDto>>>> ObterContratosPorAtivoAsync(int ativo, int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            try
            {
                var result = await _repository.ObterPorAtivoAsync(ativo, Deslocamento, RegistrosRetornado);

                if (!result.Data.Any())
                    return OperationResult<PageResultModel<IEnumerable<ContratoResponseDto>>>.Failure("Não foram encontrados contratos para o status especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(c => c.ToContratoResponseDto());

                var pageResult = new PageResultModel<IEnumerable<ContratoResponseDto>>
                {
                    Data = responseDtos,
                    Deslocamento = result.Deslocamento,
                    RegistrosRetornado = result.RegistrosRetornado,
                    TotalRegistros = result.TotalRegistros
                };

                return OperationResult<PageResultModel<IEnumerable<ContratoResponseDto>>>.Success(pageResult);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PageResultModel<IEnumerable<ContratoResponseDto>>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<ContratoResponseDto>>>> ObterContratosPorUsuarioAsync(long usuarioId, int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            try
            {
                if (usuarioId <= 0)
                    return OperationResult<PageResultModel<IEnumerable<ContratoResponseDto>>>.Failure("ID do usuário inválido");

                var result = await _repository.ObterPorUsuarioAsync(usuarioId, Deslocamento, RegistrosRetornado);

                if (!result.Data.Any())
                    return OperationResult<PageResultModel<IEnumerable<ContratoResponseDto>>>.Failure("Não foram encontrados contratos para o usuário especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(c => c.ToContratoResponseDto());

                var pageResult = new PageResultModel<IEnumerable<ContratoResponseDto>>
                {
                    Data = responseDtos,
                    Deslocamento = result.Deslocamento,
                    RegistrosRetornado = result.RegistrosRetornado,
                    TotalRegistros = result.TotalRegistros
                };

                return OperationResult<PageResultModel<IEnumerable<ContratoResponseDto>>>.Success(pageResult);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<PageResultModel<IEnumerable<ContratoResponseDto>>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        // Métodos de consulta específicos (sem paginação)
        public async Task<OperationResult<IEnumerable<ContratoResponseDto>>> ObterTodosContratosAsync()
        {
            try
            {
                var result = await _repository.ObterTodasAsync();

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<ContratoResponseDto>>.Failure("Não foram encontrados contratos", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(c => c.ToContratoResponseDto());

                return OperationResult<IEnumerable<ContratoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<ContratoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<ContratoResponseDto>>> ObterPorAtivoContratoAsync(int ativoContrato)
        {
            try
            {
                var result = await _repository.ObterPorAtivoAsync(ativoContrato);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<ContratoResponseDto>>.Failure("Não foram encontrados contratos para o status especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(c => c.ToContratoResponseDto());

                return OperationResult<IEnumerable<ContratoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<ContratoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<ContratoResponseDto>>> ObterPorUsuarioIdAsync(long usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                    return OperationResult<IEnumerable<ContratoResponseDto>>.Failure("ID do usuário inválido");

                var result = await _repository.ObterPorUsuarioAsync(usuarioId);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<ContratoResponseDto>>.Failure("Não foram encontrados contratos para o usuário especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(c => c.ToContratoResponseDto());

                return OperationResult<IEnumerable<ContratoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<ContratoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<ContratoResponseDto>>> ObterPorMotoIdAsync(long motoId)
        {
            try
            {
                if (motoId <= 0)
                    return OperationResult<IEnumerable<ContratoResponseDto>>.Failure("ID da moto inválido");

                var result = await _repository.ObterPorMotoAsync(motoId);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<ContratoResponseDto>>.Failure("Não foram encontrados contratos para a moto especificada", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(c => c.ToContratoResponseDto());

                return OperationResult<IEnumerable<ContratoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<ContratoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<ContratoResponseDto>>> ObterContratosNaoExpiradosAsync(DateTime dataAtual)
        {
            try
            {
                var result = await _repository.ObterContratosNaoExpiradosAsync(dataAtual);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<ContratoResponseDto>>.Failure("Não foram encontrados contratos não expirados", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(c => c.ToContratoResponseDto());

                return OperationResult<IEnumerable<ContratoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<ContratoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<ContratoResponseDto>>> ObterPorRenovacaoAutomaticaAsync(int renovacaoAutomatica)
        {
            try
            {
                var result = await _repository.ObterPorRenovacaoAutomaticaAsync(renovacaoAutomatica);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<ContratoResponseDto>>.Failure("Não foram encontrados contratos com renovação automática especificada", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(c => c.ToContratoResponseDto());

                return OperationResult<IEnumerable<ContratoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<ContratoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<ContratoResponseDto>>> ObterPorDataEntradaEntreAsync(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                if (dataInicio > dataFim)
                    return OperationResult<IEnumerable<ContratoResponseDto>>.Failure("Data de início deve ser menor ou igual à data de fim");

                var result = await _repository.ObterPorDataEntradaEntreAsync(dataInicio, dataFim);

                if (!result.Data.Any())
                    return OperationResult<IEnumerable<ContratoResponseDto>>.Failure("Não foram encontrados contratos no período especificado", (int)HttpStatusCode.NoContent);

                var responseDtos = result.Data.Select(c => c.ToContratoResponseDto());

                return OperationResult<IEnumerable<ContratoResponseDto>>.Success(responseDtos);
            }
            catch (Exception ex)
            {
                //Log
                return OperationResult<IEnumerable<ContratoResponseDto>>.Failure("Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
