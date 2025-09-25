using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        // Métodos com paginação
        Task<PageResultModel<IEnumerable<UsuarioEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos básicos CRUD
        Task<UsuarioEntity?> ObterPorIdAsync(int id);
        Task<UsuarioEntity?> ObterPorEmailAsync(string email);
        Task<UsuarioEntity?> SalvarAsync(UsuarioEntity entity);
        Task<UsuarioEntity?> AtualizarAsync(UsuarioEntity entity);
        Task<UsuarioEntity?> DeletarAsync(int id);
        
        // Métodos de consulta específicos (sem paginação)
        Task<PageResultModel<IEnumerable<UsuarioEntity>>> ObterTodasAsync();
    }
}