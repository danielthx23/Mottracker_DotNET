using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;

namespace Mottracker.Application.Services
{   
    public class ContratoApplicationService : IContratoApplicationService
    {
        private readonly IContratoRepository _repository;
      
        public ContratoApplicationService(IContratoRepository repository)
        {
            _repository = repository;
        }
      
        public ContratoEntity? ObterContratoPorId(int id)
        {
            return _repository.ObterPorId(id);
        }
      
        public IEnumerable<ContratoEntity> ObterTodosContratos() 
        {
            return _repository.ObterTodos();
        }
              
        public ContratoEntity? SalvarDadosContrato(ContratoEntity entity)
        {
            return _repository.Salvar(entity);
        }
              
        public ContratoEntity? EditarDadosContrato(int id, ContratoEntity entity)
        {
            var contratoExistente = _repository.ObterPorId(id);
      
            if (contratoExistente == null)
                return null;
                  
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
            contratoExistente.UsuarioContrato = entity.UsuarioContrato;
            contratoExistente.MotoContrato = entity.MotoContrato;
      
            return _repository.Atualizar(contratoExistente);
        }
              
        public ContratoEntity? DeletarDadosContrato(int id)
        {
            return _repository.Deletar(id);
        }
    }
}
