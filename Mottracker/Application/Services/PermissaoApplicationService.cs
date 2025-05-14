using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;

namespace Mottracker.Application.Services
{   
    public class PermissaoApplicationService : IPermissaoApplicationService
    {
        private readonly IPermissaoRepository _repository;
      
        public PermissaoApplicationService(IPermissaoRepository repository)
        {
            _repository = repository;
        }
      
        public PermissaoEntity? ObterPermissaoPorId(int id)
        {
            return _repository.ObterPorId(id);
        }
      
        public IEnumerable<PermissaoEntity> ObterTodosPermissoes() 
        {
            return _repository.ObterTodos();
        }
              
        public PermissaoEntity? SalvarDadosPermissao(PermissaoEntity entity)
        {
            return _repository.Salvar(entity);
        }
              
        public PermissaoEntity? EditarDadosPermissao(int id, PermissaoEntity entity)
        {
            var permissaoExistente = _repository.ObterPorId(id);
      
            if (permissaoExistente == null)
                return null;
                  
            permissaoExistente.NomePermissao = entity.NomePermissao;
            permissaoExistente.Descricao = entity.Descricao;
            permissaoExistente.UsuariosPermissoes = entity.UsuariosPermissoes;
      
            return _repository.Atualizar(permissaoExistente);
        }
              
        public PermissaoEntity? DeletarDadosPermissao(int id)
        {
            return _repository.Deletar(id);
        }
    }
}
