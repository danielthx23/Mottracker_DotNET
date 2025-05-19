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
        IEnumerable<EnderecoEntity> ObterPorCidade(string cidade);
        IEnumerable<EnderecoEntity> ObterPorCidadeEEstado(string cidade, string estado);
        IEnumerable<EnderecoEntity> ObterPorEstado(string estado);
        EnderecoEntity? ObterPorCep(string cep);
        IEnumerable<EnderecoEntity> ObterPorBairro(string bairro);
        IEnumerable<EnderecoEntity> ObterPorLogradouroContendo(string logradouro);
        EnderecoEntity? ObterPorIdPatio(long patioId);
    }
}