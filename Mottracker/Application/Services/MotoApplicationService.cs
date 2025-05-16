using Mottracker.Application.Dtos.Moto;
using Mottracker.Application.Dtos.Patio;
using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Application.Dtos;

namespace Mottracker.Application.Services
{   
    public class MotoApplicationService : IMotoApplicationService
    {
        private readonly IMotoRepository _repository;

        public MotoApplicationService(IMotoRepository repository)
        {
            _repository = repository;
        }
        
        public MotoResponseDto? ObterMotoPorId(int id)
        {
            var moto = _repository.ObterPorId(id);
            if (moto == null) return null;

            return MapToResponseDto(moto);
        }
        
        public IEnumerable<MotoResponseDto> ObterTodasMotos()
        {
            var motos = _repository.ObterTodos();
            return motos.Select(MapToResponseDto);
        }
        
        public MotoResponseDto? SalvarDadosMoto(MotoRequestDto entity)
        {
            var motoEntity = new MotoEntity
            {
                PlacaMoto = entity.PlacaMoto,
                ModeloMoto = entity.ModeloMoto,
                AnoMoto = entity.AnoMoto,
                IdentificadorMoto = entity.IdentificadorMoto,
                QuilometragemMoto = entity.QuilometragemMoto,
                EstadoMoto = entity.EstadoMoto,
                CondicoesMoto = entity.CondicoesMoto,
                ContratoMotoId = entity.ContratoMotoId,
                MotoPatioAtualId = entity.MotoPatioAtualId,
                MotoPatioOrigemId = entity.MotoPatioOrigemId
            };

            var salvo = _repository.Salvar(motoEntity);
            if (salvo == null) return null;
            
            return MapToResponseDto(salvo);
        }
        
        public MotoResponseDto? EditarDadosMoto(int id, MotoRequestDto entity)
        {
            var motoExistente = _repository.ObterPorId(id);
            if (motoExistente == null) return null;

            motoExistente.PlacaMoto = entity.PlacaMoto;
            motoExistente.ModeloMoto = entity.ModeloMoto;
            motoExistente.AnoMoto = entity.AnoMoto;
            motoExistente.IdentificadorMoto = entity.IdentificadorMoto;
            motoExistente.QuilometragemMoto = entity.QuilometragemMoto;
            motoExistente.EstadoMoto = entity.EstadoMoto;
            motoExistente.CondicoesMoto = entity.CondicoesMoto;
            motoExistente.ContratoMotoId = entity.ContratoMotoId;
            motoExistente.MotoPatioAtualId = entity.MotoPatioAtualId;
            motoExistente.MotoPatioOrigemId = entity.MotoPatioOrigemId;

            var atualizado = _repository.Atualizar(motoExistente);
            if (atualizado == null) return null;
            
            return MapToResponseDto(atualizado);
        }
        
        public MotoResponseDto? DeletarDadosMoto(int id)
        {
            var deletado = _repository.Deletar(id);
            return deletado == null ? null : MapToResponseDto(deletado);
        }
        
        private MotoResponseDto MapToResponseDto(MotoEntity moto)
        {
            return new MotoResponseDto
            {
                IdMoto = moto.IdMoto,
                PlacaMoto = moto.PlacaMoto,
                ModeloMoto = moto.ModeloMoto,
                AnoMoto = moto.AnoMoto,
                IdentificadorMoto = moto.IdentificadorMoto,
                QuilometragemMoto = moto.QuilometragemMoto,
                EstadoMoto = moto.EstadoMoto,
                CondicoesMoto = moto.CondicoesMoto,
                MotoPatioOrigemId = moto.MotoPatioOrigemId,

                ContratoMoto = moto.ContratoMoto != null ? new ContratoDto
                {
                    IdContrato = moto.ContratoMoto.IdContrato,
                    ClausulasContrato = moto.ContratoMoto.ClausulasContrato,
                    DataDeEntradaContrato = moto.ContratoMoto.DataDeEntradaContrato,
                    HorarioDeDevolucaoContrato = moto.ContratoMoto.HorarioDeDevolucaoContrato,
                    DataDeExpiracaoContrato = moto.ContratoMoto.DataDeExpiracaoContrato,
                    RenovacaoAutomaticaContrato = moto.ContratoMoto.RenovacaoAutomaticaContrato,
                    DataUltimaRenovacaoContrato = moto.ContratoMoto.DataUltimaRenovacaoContrato,
                    NumeroRenovacoesContrato = moto.ContratoMoto.NumeroRenovacoesContrato,
                    AtivoContrato = moto.ContratoMoto.AtivoContrato,
                    ValorToralContrato = moto.ContratoMoto.ValorToralContrato,
                    QuantidadeParcelas = moto.ContratoMoto.QuantidadeParcelas
                } : null,

                MotoPatioAtual = moto.MotoPatioAtual != null ? new PatioDto
                {
                    IdPatio = moto.MotoPatioAtual.IdPatio,
                    NomePatio = moto.MotoPatioAtual.NomePatio,
                    MotosTotaisPatio = moto.MotoPatioAtual.MotosTotaisPatio,
                    MotosDisponiveisPatio = moto.MotoPatioAtual.MotosDisponiveisPatio,
                    DataPatio = moto.MotoPatioAtual.DataPatio
                } : null
            };
        }
    }
}
