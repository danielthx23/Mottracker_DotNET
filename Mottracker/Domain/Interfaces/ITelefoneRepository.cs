using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{   
    public interface ITelefoneRepository
    {
        IEnumerable<TelefoneEntity> ObterTodos();
        TelefoneEntity? ObterPorId(int id);
        TelefoneEntity? Salvar(TelefoneEntity entity);
        TelefoneEntity? Atualizar(TelefoneEntity entity);
        TelefoneEntity? Deletar(int id);
        IEnumerable<TelefoneEntity> ObterPorNumero(string numero);
        IEnumerable<TelefoneEntity> ObterPorIdUsuario(long usuarioId);
        IEnumerable<TelefoneEntity> ObterPorTipo(string tipo);
    }
}