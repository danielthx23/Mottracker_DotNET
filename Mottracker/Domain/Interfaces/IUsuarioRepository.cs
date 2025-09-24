using Mottracker.Domain.Entities;
using Mottracker.Application.Models;

namespace Mottracker.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<PageResultModel<IEnumerable<UsuarioEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<UsuarioEntity?> ObterPorIdAsync(int id);
        Task<UsuarioEntity?> SalvarAsync(UsuarioEntity entity);
        Task<UsuarioEntity?> AtualizarAsync(UsuarioEntity entity);
        Task<UsuarioEntity?> DeletarAsync(int id);
        Task<UsuarioEntity?> ObterPorEmailAsync(string email);
    }
}