using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;

namespace Mottracker.Application.Services
{   
    public class MotoApplicationService : IMotoApplicationService
    {
        private readonly IMotoRepository _repository;
      
        public MotoApplicationService(IMotoRepository repository)
        {
            _repository = repository;
        }
      
        public MotoEntity? ObterMotoPorId(int id)
        {
            return _repository.ObterPorId(id);
        }
      
        public IEnumerable<MotoEntity> ObterTodasMotos()
        {
            return _repository.ObterTodos();
        }
              
        public MotoEntity? SalvarDadosMoto(MotoEntity entity)
        {
            return _repository.Salvar(entity);
        }
              
        public MotoEntity? EditarDadosMoto(int id, MotoEntity entity)
        {
            var motoExistente = _repository.ObterPorId(id);
      
            if (motoExistente == null)
                return null;
                  
            motoExistente.PlacaMoto = entity.PlacaMoto;
            motoExistente.ModeloMoto = entity.ModeloMoto;
            motoExistente.AnoMoto = entity.AnoMoto;
            motoExistente.IdentificadorMoto = entity.IdentificadorMoto;
            motoExistente.QuilometragemMoto = entity.QuilometragemMoto;
            motoExistente.EstadoMoto = entity.EstadoMoto;
            motoExistente.CondicoesMoto = entity.CondicoesMoto;
            motoExistente.ContratoMoto = entity.ContratoMoto;
            motoExistente.MotoPatioAtual = entity.MotoPatioAtual;
            motoExistente.MotoPatioOrigem = entity.MotoPatioOrigem;
      
            return _repository.Atualizar(motoExistente);
        }
              
        public MotoEntity? DeletarDadosMoto(int id)
        {
            return _repository.Deletar(id);
        }
    }
}
