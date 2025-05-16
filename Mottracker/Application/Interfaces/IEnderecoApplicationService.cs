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
    }
}
