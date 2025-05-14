using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{   
    public interface IEnderecoRepository
    {
        IEnumerable<EnderecoEntity> ObterTodos();
        EnderecoEntity? ObterPorId(int id);
        EnderecoEntity? Salvar(EnderecoEntity entity);
        EnderecoEntity? Atualizar(EnderecoEntity entity);
        EnderecoEntity? Deletar(int id);
    }
}
