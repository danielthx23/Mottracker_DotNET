using Mottracker.Domain.Entities;
using Mottracker.Application.Models;

namespace Mottracker.Domain.Interfaces
{
    public interface IEnderecoRepository
    {
        Task<PageResultModel<IEnumerable<EnderecoEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<EnderecoEntity?> ObterPorIdAsync(int id);
        Task<EnderecoEntity?> SalvarAsync(EnderecoEntity entity);
        Task<EnderecoEntity?> AtualizarAsync(EnderecoEntity entity);
        Task<EnderecoEntity?> DeletarAsync(int id);
    }
}