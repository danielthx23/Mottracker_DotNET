using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{
    public interface ITelefoneRepository
    {
        // Métodos com paginação
        Task<PageResultModel<IEnumerable<TelefoneEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos básicos CRUD
        Task<TelefoneEntity?> ObterPorIdAsync(int id);
        Task<TelefoneEntity?> SalvarAsync(TelefoneEntity entity);
        Task<TelefoneEntity?> AtualizarAsync(TelefoneEntity entity);
        Task<TelefoneEntity?> DeletarAsync(int id);
        
        // Métodos de consulta específicos (sem paginação)
        Task<PageResultModel<IEnumerable<TelefoneEntity>>> ObterTodasAsync();
        Task<PageResultModel<IEnumerable<TelefoneEntity>>> ObterPorNumeroAsync(string numero);
        Task<PageResultModel<IEnumerable<TelefoneEntity>>> ObterPorUsuarioIdAsync(int usuarioId);
        Task<PageResultModel<IEnumerable<TelefoneEntity>>> ObterPorTipoAsync(string tipo);
    }
}