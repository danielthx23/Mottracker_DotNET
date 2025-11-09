using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{
    public interface IEnderecoRepository
    {
        // Métodos com paginação
        Task<PageResultModel<IEnumerable<EnderecoEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos básicos CRUD
        Task<EnderecoEntity?> ObterPorIdAsync(int id);
        Task<EnderecoEntity?> SalvarAsync(EnderecoEntity entity);
        Task<EnderecoEntity?> AtualizarAsync(EnderecoEntity entity);
        Task<EnderecoEntity?> DeletarAsync(int id);
        
        // Métodos de consulta específicos (sem paginação)
        Task<PageResultModel<IEnumerable<EnderecoEntity>>> ObterTodasAsync();
        Task<PageResultModel<IEnumerable<EnderecoEntity>>> ObterPorCepAsync(string cep);
        Task<PageResultModel<IEnumerable<EnderecoEntity>>> ObterPorEstadoAsync(string estado);
        Task<PageResultModel<IEnumerable<EnderecoEntity>>> ObterPorCidadeAsync(string cidade);
        Task<PageResultModel<IEnumerable<EnderecoEntity>>> ObterPorCidadeEstadoAsync(string cidade, string estado);
        Task<PageResultModel<IEnumerable<EnderecoEntity>>> ObterPorBairroAsync(string bairro);
        Task<PageResultModel<IEnumerable<EnderecoEntity>>> ObterPorLogradouroContendoAsync(string logradouro);
        Task<PageResultModel<IEnumerable<EnderecoEntity>>> ObterPorPatioIdAsync(int patioId);
    }
}