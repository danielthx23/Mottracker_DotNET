using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;

namespace Mottracker.Application.Services
{   
    public class LayoutPatioApplicationService : ILayoutPatioApplicationService
    {
        private readonly ILayoutPatioRepository _repository;
      
        public LayoutPatioApplicationService(ILayoutPatioRepository repository)
        {
            _repository = repository;
        }
      
        public LayoutPatioEntity? ObterLayoutPatioPorId(int id)
        {
            return _repository.ObterPorId(id);
        }
      
        public IEnumerable<LayoutPatioEntity> ObterTodosLayoutsPatios()
        {
            return _repository.ObterTodos();
        }
              
        public LayoutPatioEntity? SalvarDadosLayoutPatio(LayoutPatioEntity entity)
        {
            return _repository.Salvar(entity);
        }
              
        public LayoutPatioEntity? EditarDadosLayoutPatio(int id, LayoutPatioEntity entity)
        {
            var layoutPatioExistente = _repository.ObterPorId(id);
      
            if (layoutPatioExistente == null)
                return null;
                  
            layoutPatioExistente.Descricao = entity.Descricao;
            layoutPatioExistente.DataCriacao = entity.DataCriacao;
            layoutPatioExistente.PatioLayoutPatio = entity.PatioLayoutPatio;
            layoutPatioExistente.QrCodesLayoutPatio = entity.QrCodesLayoutPatio;
      
            return _repository.Atualizar(layoutPatioExistente);
        }
              
        public LayoutPatioEntity? DeletarDadosLayoutPatio(int id)
        {
            return _repository.Deletar(id);
        }
    }
}
