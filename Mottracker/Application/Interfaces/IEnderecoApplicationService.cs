using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{   
    public interface IEnderecoApplicationService
    {
        IEnumerable<EnderecoEntity> ObterTodosEnderecos();
        EnderecoEntity? ObterEnderecoPorId(int id);
        EnderecoEntity? SalvarDadosEndereco(EnderecoEntity entity);
        EnderecoEntity? EditarDadosEndereco(int id, EnderecoEntity entity);
        EnderecoEntity? DeletarDadosEndereco(int id);
    }
}
