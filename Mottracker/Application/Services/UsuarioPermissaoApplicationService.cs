using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;

namespace Mottracker.Application.Services
{   
    public class UsuarioPermissaoApplicationService : IUsuarioPermissaoApplicationService
    {
        private readonly IUsuarioPermissaoRepository _repository;
      
        public UsuarioPermissaoApplicationService(IUsuarioPermissaoRepository repository)
        {
            _repository = repository;
        }
      
        public UsuarioPermissaoEntity? ObterUsuarioPermissaoPorId(int id)
        {
            return _repository.ObterPorId(id);
        }
      
        public IEnumerable<UsuarioPermissaoEntity> ObterTodosUsuarioPermissoes() 
        {
            return _repository.ObterTodos();
        }
              
        public UsuarioPermissaoEntity? SalvarDadosUsuarioPermissao(UsuarioPermissaoEntity entity)
        {
            return _repository.Salvar(entity);
        }
              
        public UsuarioPermissaoEntity? EditarDadosUsuarioPermissao(int id, UsuarioPermissaoEntity entity)
        {
            var usuarioPermissaoExistente = _repository.ObterPorId(id);

            if (usuarioPermissaoExistente == null)
                return null;
            
            usuarioPermissaoExistente.Papel = entity.Papel;
            usuarioPermissaoExistente.Usuarios = entity.Usuarios;
            usuarioPermissaoExistente.Permissoes = entity.Permissoes;

            return _repository.Atualizar(usuarioPermissaoExistente);
        }

              
        public UsuarioPermissaoEntity? DeletarDadosUsuarioPermissao(int id)
        {
            return _repository.Deletar(id);
        }
    }
}
