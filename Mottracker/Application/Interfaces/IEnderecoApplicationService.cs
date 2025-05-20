using Mottracker.Application.Dtos.Endereco;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{
    public interface IEnderecoApplicationService
    {
        IEnumerable<EnderecoResponseDto> ObterTodosEnderecos();
        EnderecoResponseDto? ObterEnderecoPorId(int id);
        EnderecoResponseDto? SalvarDadosEndereco(EnderecoRequestDto entity);
        EnderecoResponseDto? EditarDadosEndereco(int id, EnderecoRequestDto entity);
        EnderecoResponseDto? DeletarDadosEndereco(int id);
        EnderecoResponseDto? ObterEnderecoPorCep(string cep);
        IEnumerable<EnderecoResponseDto> ObterEnderecosPorEstado(string estado);
        IEnumerable<EnderecoResponseDto> ObterEnderecosPorCidade(string cidade);
        IEnumerable<EnderecoResponseDto> ObterEnderecosPorCidadeEEstado(string cidade, string estado);
        IEnumerable<EnderecoResponseDto> ObterEnderecosPorBairro(string bairro);
        IEnumerable<EnderecoResponseDto> ObterEnderecosPorLogradouroContendo(string logradouro);
        EnderecoResponseDto? ObterEnderecoPorIdPatio(long patioId);
    }
}
