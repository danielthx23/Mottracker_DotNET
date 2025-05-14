using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{   
    public interface IUsuarioApplicationService
    {
        IEnumerable<UsuarioEntity> ObterTodosUsuarios();
        UsuarioEntity? ObterUsuarioPorId(int id);
        UsuarioEntity? SalvarDadosUsuario(UsuarioEntity entity);
        UsuarioEntity? EditarDadosUsuario(int id, UsuarioEntity entity);
        UsuarioEntity? DeletarDadosUsuario(int id);
    }
}
