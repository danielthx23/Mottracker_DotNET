using Mottracker.Domain.Entities;
using Mottracker.Domain.Enums;

namespace Mottracker.Domain.Interfaces
{
    public interface IMotoRepository
    {
        // Métodos com paginação
        Task<PageResultModel<IEnumerable<MotoEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<PageResultModel<IEnumerable<MotoEntity>>> ObterPorEstadoAsync(Estados estado, int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos básicos CRUD
        Task<MotoEntity?> ObterPorIdAsync(int id);
        Task<MotoEntity?> ObterPorPlacaAsync(string placa);
        Task<MotoEntity?> SalvarAsync(MotoEntity entity);
        Task<MotoEntity?> AtualizarAsync(MotoEntity entity);
        Task<MotoEntity?> DeletarAsync(int id);
        
        // Métodos de consulta específicos (sem paginação)
        Task<PageResultModel<IEnumerable<MotoEntity>>> ObterTodasAsync();
        Task<PageResultModel<IEnumerable<MotoEntity>>> ObterPorEstadoAsync(Estados estado);
        Task<PageResultModel<IEnumerable<MotoEntity>>> ObterPorContratoAsync(long contratoId);
    }
}