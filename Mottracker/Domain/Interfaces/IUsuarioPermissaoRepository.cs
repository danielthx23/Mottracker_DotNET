using Mottracker.Domain.Entities;
using Mottracker.Application.Models;

namespace Mottracker.Domain.Interfaces
{
    public interface IUsuarioPermissaoRepository
    {
        Task<PageResultModel<IEnumerable<UsuarioPermissaoEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<UsuarioPermissaoEntity?> ObterPorIdAsync(int usuarioId, int permissaoId);
        Task<UsuarioPermissaoEntity?> SalvarAsync(UsuarioPermissaoEntity entity);
        Task<UsuarioPermissaoEntity?> AtualizarAsync(UsuarioPermissaoEntity entity);
        Task<UsuarioPermissaoEntity?> DeletarAsync(int usuarioId, int permissaoId);
    }
}