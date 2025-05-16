using Mottracker.Application.Dtos.Contrato;
using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Mottracker.Application.Dtos.Moto;
using Mottracker.Application.Dtos.Usuario;

namespace Mottracker.Application.Services
{
    public class ContratoApplicationService : IContratoApplicationService
    {
        private readonly IContratoRepository _repository;

        public ContratoApplicationService(IContratoRepository repository)
        {
            _repository = repository;
        }

        public ContratoResponseDto? ObterContratoPorId(int id)
        {
            var contrato = _repository.ObterPorId(id);

            if (contrato == null) return null;

            return MapToResponseDto(contrato);
        }

        public IEnumerable<ContratoResponseDto> ObterTodosContratos()
        {
            var contratos = _repository.ObterTodos();

            return contratos.Select(MapToResponseDto);
        }

        public ContratoResponseDto? SalvarDadosContrato(ContratoRequestDto entity)
        {
            var contratoEntity = new ContratoEntity
            {
                ClausulasContrato = entity.ClausulasContrato,
                DataDeEntradaContrato = entity.DataDeEntradaContrato,
                HorarioDeDevolucaoContrato = entity.HorarioDeDevolucaoContrato,
                DataDeExpiracaoContrato = entity.DataDeExpiracaoContrato,
                RenovacaoAutomaticaContrato = entity.RenovacaoAutomaticaContrato,
                DataUltimaRenovacaoContrato = entity.DataUltimaRenovacaoContrato,
                NumeroRenovacoesContrato = entity.NumeroRenovacoesContrato,
                AtivoContrato = entity.AtivoContrato,
                ValorToralContrato = entity.ValorToralContrato,
                QuantidadeParcelas = entity.QuantidadeParcelas,
                UsuarioContratoId = entity.UsuarioContratoId,
            };

            var saved = _repository.Salvar(contratoEntity);
            if (saved == null) return null;

            return MapToResponseDto(saved);
        }

        public ContratoResponseDto? EditarDadosContrato(int id, ContratoRequestDto entity)
        {
            var contratoExistente = _repository.ObterPorId(id);
            if (contratoExistente == null) return null;

            contratoExistente.ClausulasContrato = entity.ClausulasContrato;
            contratoExistente.DataDeEntradaContrato = entity.DataDeEntradaContrato;
            contratoExistente.HorarioDeDevolucaoContrato = entity.HorarioDeDevolucaoContrato;
            contratoExistente.DataDeExpiracaoContrato = entity.DataDeExpiracaoContrato;
            contratoExistente.RenovacaoAutomaticaContrato = entity.RenovacaoAutomaticaContrato;
            contratoExistente.DataUltimaRenovacaoContrato = entity.DataUltimaRenovacaoContrato;
            contratoExistente.NumeroRenovacoesContrato = entity.NumeroRenovacoesContrato;
            contratoExistente.AtivoContrato = entity.AtivoContrato;
            contratoExistente.ValorToralContrato = entity.ValorToralContrato;
            contratoExistente.QuantidadeParcelas = entity.QuantidadeParcelas;
            contratoExistente.UsuarioContratoId = entity.UsuarioContratoId;

            var updated = _repository.Atualizar(contratoExistente);
            if (updated == null) return null;
            
            return MapToResponseDto(updated);
        }

        public ContratoResponseDto? DeletarDadosContrato(int id)
        {
            var deleted = _repository.Deletar(id);
            if (deleted == null) return null;

            return MapToResponseDto(deleted);
        }

        private ContratoResponseDto MapToResponseDto(ContratoEntity contrato)
{
    return new ContratoResponseDto
    {
        IdContrato = contrato.IdContrato,
        ClausulasContrato = contrato.ClausulasContrato,
        DataDeEntradaContrato = contrato.DataDeEntradaContrato,
        HorarioDeDevolucaoContrato = contrato.HorarioDeDevolucaoContrato,
        DataDeExpiracaoContrato = contrato.DataDeExpiracaoContrato,
        RenovacaoAutomaticaContrato = contrato.RenovacaoAutomaticaContrato,
        DataUltimaRenovacaoContrato = contrato.DataUltimaRenovacaoContrato,
        NumeroRenovacoesContrato = contrato.NumeroRenovacoesContrato,
        AtivoContrato = contrato.AtivoContrato,
        ValorToralContrato = contrato.ValorToralContrato,
        QuantidadeParcelas = contrato.QuantidadeParcelas,

        UsuarioContrato = contrato.UsuarioContrato != null ? new UsuarioDto
        {
            IdUsuario = contrato.UsuarioContrato.IdUsuario,
            NomeUsuario = contrato.UsuarioContrato.NomeUsuario,
            CPFUsuario = contrato.UsuarioContrato.CPFUsuario,
            SenhaUsuario = contrato.UsuarioContrato.SenhaUsuario,
            CNHUsuario = contrato.UsuarioContrato.CNHUsuario,
            EmailUsuario = contrato.UsuarioContrato.EmailUsuario,
            TokenUsuario = contrato.UsuarioContrato.TokenUsuario,
            DataNascimentoUsuario = contrato.UsuarioContrato.DataNascimentoUsuario,
            CriadoEmUsuario = contrato.UsuarioContrato.CriadoEmUsuario,
        } : null,

        MotoContrato = contrato.MotoContrato != null ? new MotoDto
        {
            IdMoto = contrato.MotoContrato.IdMoto,
            PlacaMoto = contrato.MotoContrato.PlacaMoto,
            ModeloMoto = contrato.MotoContrato.ModeloMoto,
            AnoMoto = contrato.MotoContrato.AnoMoto,
            IdentificadorMoto = contrato.MotoContrato.IdentificadorMoto,
            QuilometragemMoto = contrato.MotoContrato.QuilometragemMoto,
            EstadoMoto = contrato.MotoContrato.EstadoMoto,
            CondicoesMoto = contrato.MotoContrato.CondicoesMoto,
            MotoPatioOrigemId = contrato.MotoContrato.MotoPatioOrigemId
        } : null
    };
}

    }
}
