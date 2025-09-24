using Mottracker.Domain.Entities;
using Mottracker.Application.Models;

namespace Mottracker.Domain.Interfaces
{
    public interface IContratoRepository
    {
        Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<ContratoEntity?> ObterPorIdAsync(int id);
        Task<ContratoEntity?> SalvarAsync(ContratoEntity entity);
        Task<ContratoEntity?> AtualizarAsync(ContratoEntity entity);
        Task<ContratoEntity?> DeletarAsync(int id);
        Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterPorAtivoAsync(int ativo, int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterPorUsuarioAsync(long usuarioId, int Deslocamento = 0, int RegistrosRetornado = 3);
    }
}
