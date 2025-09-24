using Mottracker.Domain.Entities;
using Mottracker.Application.Models;

namespace Mottracker.Domain.Interfaces
{
    public interface IPermissaoRepository
    {
        Task<PageResultModel<IEnumerable<PermissaoEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<PermissaoEntity?> ObterPorIdAsync(int id);
        Task<PermissaoEntity?> SalvarAsync(PermissaoEntity entity);
        Task<PermissaoEntity?> AtualizarAsync(PermissaoEntity entity);
        Task<PermissaoEntity?> DeletarAsync(int id);
    }
}