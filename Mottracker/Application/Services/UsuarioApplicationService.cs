using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;

namespace Mottracker.Application.Services
{   
    public class UsuarioApplicationService : IUsuarioApplicationService
    {
        private readonly IUsuarioRepository _repository;
      
        public UsuarioApplicationService(IUsuarioRepository repository)
        {
            _repository = repository;
        }
      
        public UsuarioEntity? ObterUsuarioPorId(int id)
        {
            return _repository.ObterPorId(id);
        }
      
        public IEnumerable<UsuarioEntity> ObterTodosUsuarios() 
        {
            return _repository.ObterTodos();
        }
              
        public UsuarioEntity? SalvarDadosUsuario(UsuarioEntity entity)
        {
            return _repository.Salvar(entity);
        }
              
        public UsuarioEntity? EditarDadosUsuario(int id, UsuarioEntity entity)
        {
            var usuarioExistente = _repository.ObterPorId(id);

            if (usuarioExistente == null)
                return null;
            
            usuarioExistente.NomeUsuario = entity.NomeUsuario;
            usuarioExistente.CPFUsuario = entity.CPFUsuario;
            usuarioExistente.SenhaUsuario = entity.SenhaUsuario;
            usuarioExistente.CNHUsuario = entity.CNHUsuario;
            usuarioExistente.EmailUsuario = entity.EmailUsuario;
            usuarioExistente.TelefoneUsuario = entity.TelefoneUsuario;
            usuarioExistente.TokenUsuario = entity.TokenUsuario;
            usuarioExistente.DataNascimentoUsuario = entity.DataNascimentoUsuario;
            usuarioExistente.CriadoEmUsuario = entity.CriadoEmUsuario;
            usuarioExistente.ContratosUsuario = entity.ContratosUsuario;
            usuarioExistente.UsuarioPermissao = entity.UsuarioPermissao;

            return _repository.Atualizar(usuarioExistente);
        }

              
        public UsuarioEntity? DeletarDadosUsuario(int id)
        {
            return _repository.Deletar(id);
        }
    }
}
