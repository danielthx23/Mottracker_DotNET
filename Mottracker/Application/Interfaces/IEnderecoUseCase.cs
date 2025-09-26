using Mottracker.Application.Dtos.Endereco;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{
    public interface IEnderecoUseCase
    {
        // Métodos com paginação
        Task<OperationResult<PageResultModel<IEnumerable<EnderecoResponseDto>>>> ObterTodosEnderecosAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos básicos CRUD
        Task<OperationResult<EnderecoResponseDto?>> ObterEnderecoPorIdAsync(int id);
        Task<OperationResult<EnderecoResponseDto?>> SalvarEnderecoAsync(EnderecoRequestDto dto);
        Task<OperationResult<EnderecoResponseDto?>> EditarEnderecoAsync(int id, EnderecoRequestDto dto);
        Task<OperationResult<EnderecoResponseDto?>> DeletarEnderecoAsync(int id);
        
        // Métodos de consulta específicos (sem paginação)
        Task<OperationResult<IEnumerable<EnderecoResponseDto>>> ObterTodosEnderecosAsync();
        Task<OperationResult<EnderecoResponseDto?>> SalvarDadosEnderecoAsync(EnderecoRequestDto entity);
        Task<OperationResult<EnderecoResponseDto?>> EditarDadosEnderecoAsync(int id, EnderecoRequestDto entity);
        Task<OperationResult<EnderecoResponseDto?>> DeletarDadosEnderecoAsync(int id);
        Task<OperationResult<IEnumerable<EnderecoResponseDto>>> ObterEnderecoPorCepAsync(string cep);
        Task<OperationResult<IEnumerable<EnderecoResponseDto>>> ObterEnderecoPorEstadoAsync(string estado);
        Task<OperationResult<IEnumerable<EnderecoResponseDto>>> ObterEnderecoPorCidadeAsync(string cidade);
        Task<OperationResult<IEnumerable<EnderecoResponseDto>>> ObterEnderecoPorCidadeEstadoAsync(string cidade, string estado);
        Task<OperationResult<IEnumerable<EnderecoResponseDto>>> ObterEnderecoPorBairroAsync(string bairro);
        Task<OperationResult<IEnumerable<EnderecoResponseDto>>> ObterEnderecoPorLogradouroContendoAsync(string logradouro);
        Task<OperationResult<IEnumerable<EnderecoResponseDto>>> ObterEnderecoPorPatioIdAsync(int patioId);
    }
}
