using Mottracker.Application.Dtos.Endereco;
using Mottracker.Application.Models;

namespace Mottracker.Application.Interfaces
{
    public interface IEnderecoUseCase
    {
        Task<OperationResult<PageResultModel<IEnumerable<EnderecoResponseDto>>>> ObterTodosEnderecosAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<OperationResult<EnderecoResponseDto?>> ObterEnderecoPorIdAsync(int id);
        Task<OperationResult<EnderecoResponseDto?>> SalvarEnderecoAsync(EnderecoRequestDto dto);
        Task<OperationResult<EnderecoResponseDto?>> EditarEnderecoAsync(int id, EnderecoRequestDto dto);
        Task<OperationResult<EnderecoResponseDto?>> DeletarEnderecoAsync(int id);
        
        // Métodos síncronos para compatibilidade
        IEnumerable<EnderecoResponseDto> ObterTodosEnderecos();
        EnderecoResponseDto? ObterEnderecoPorId(int id);
        EnderecoResponseDto? SalvarDadosEndereco(EnderecoRequestDto entity);
        EnderecoResponseDto? EditarDadosEndereco(int id, EnderecoRequestDto entity);
        EnderecoResponseDto? DeletarDadosEndereco(int id);
        IEnumerable<EnderecoResponseDto>? ObterEnderecoPorCep(string cep);
        IEnumerable<EnderecoResponseDto>? ObterEnderecoPorEstado(string estado);
        IEnumerable<EnderecoResponseDto>? ObterEnderecoPorCidade(string cidade);
        IEnumerable<EnderecoResponseDto>? ObterEnderecoPorCidadeEstado(string cidade, string estado);
        IEnumerable<EnderecoResponseDto>? ObterEnderecoPorBairro(string bairro);
        IEnumerable<EnderecoResponseDto>? ObterEnderecoPorLogradouroContendo(string logradouro);
        IEnumerable<EnderecoResponseDto>? ObterEnderecoPorPatioId(int patioId);
    }
}
