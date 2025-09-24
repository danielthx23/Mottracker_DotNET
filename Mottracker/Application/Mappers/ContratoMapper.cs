using Mottracker.Application.Dtos.Contrato;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Mappers
{
    public static class ContratoMapper
    {
        public static ContratoEntity ToContratoEntity(this ContratoRequestDto obj)
        {
            return new ContratoEntity
            {
                IdContrato = obj.IdContrato,
                ClausulasContrato = obj.ClausulasContrato,
                DataDeEntradaContrato = obj.DataDeEntradaContrato,
                HorarioDeDevolucaoContrato = obj.HorarioDeDevolucaoContrato,
                DataDeExpiracaoContrato = obj.DataDeExpiracaoContrato,
                RenovacaoAutomaticaContrato = obj.RenovacaoAutomaticaContrato ? 1 : 0,
                DataUltimaRenovacaoContrato = obj.DataUltimaRenovacaoContrato,
                NumeroRenovacoesContrato = obj.NumeroRenovacoesContrato,
                AtivoContrato = obj.AtivoContrato ? 1 : 0,
                ValorToralContrato = obj.ValorToralContrato,
                QuantidadeParcelas = obj.QuantidadeParcelas,
                UsuarioContratoId = obj.UsuarioContratoId
            };
        }

        public static ContratoDto ToContratoDto(this ContratoEntity obj)
        {
            return new ContratoDto
            {
                IdContrato = obj.IdContrato,
                ClausulasContrato = obj.ClausulasContrato,
                DataDeEntradaContrato = obj.DataDeEntradaContrato,
                HorarioDeDevolucaoContrato = obj.HorarioDeDevolucaoContrato,
                DataDeExpiracaoContrato = obj.DataDeExpiracaoContrato,
                RenovacaoAutomaticaContrato = obj.RenovacaoAutomaticaContrato,
                DataUltimaRenovacaoContrato = obj.DataUltimaRenovacaoContrato,
                NumeroRenovacoesContrato = obj.NumeroRenovacoesContrato,
                AtivoContrato = obj.AtivoContrato,
                ValorToralContrato = obj.ValorToralContrato,
                QuantidadeParcelas = obj.QuantidadeParcelas
            };
        }

        public static ContratoResponseDto ToContratoResponseDto(this ContratoEntity obj)
        {
            return new ContratoResponseDto
            {
                IdContrato = obj.IdContrato,
                ClausulasContrato = obj.ClausulasContrato,
                DataDeEntradaContrato = obj.DataDeEntradaContrato,
                HorarioDeDevolucaoContrato = obj.HorarioDeDevolucaoContrato,
                DataDeExpiracaoContrato = obj.DataDeExpiracaoContrato,
                RenovacaoAutomaticaContrato = obj.RenovacaoAutomaticaContrato == 1,
                DataUltimaRenovacaoContrato = obj.DataUltimaRenovacaoContrato,
                NumeroRenovacoesContrato = obj.NumeroRenovacoesContrato,
                AtivoContrato = obj.AtivoContrato == 1,
                ValorToralContrato = obj.ValorToralContrato,
                QuantidadeParcelas = obj.QuantidadeParcelas,
                UsuarioContrato = obj.UsuarioContrato?.ToUsuarioDto(),
                MotoContrato = obj.MotoContrato?.ToMotoDto()
            };
        }
    }
}
