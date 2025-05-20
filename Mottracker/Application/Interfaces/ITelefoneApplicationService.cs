using Mottracker.Application.Dtos.Telefone;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{   
    public interface ITelefoneApplicationService
    {
        IEnumerable<TelefoneResponseDto> ObterTodosTelefones();
        TelefoneResponseDto? ObterTelefonePorId(int id);
        TelefoneResponseDto? SalvarDadosTelefone(TelefoneRequestDto entity);
        TelefoneResponseDto? EditarDadosTelefone(int id, TelefoneRequestDto entity);
        TelefoneResponseDto? DeletarDadosTelefone(int id);
        IEnumerable<TelefoneResponseDto> ObterTelefonesPorNumero(string numero);
        IEnumerable<TelefoneResponseDto> ObterTelefonesPorUsuarioId(long usuarioId);
        IEnumerable<TelefoneResponseDto> ObterTelefonesPorTipo(string tipo);
    }
}
