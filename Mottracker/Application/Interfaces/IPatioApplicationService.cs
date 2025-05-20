using Mottracker.Application.Dtos.Patio;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{   
    public interface IPatioApplicationService
    {
        IEnumerable<PatioResponseDto> ObterTodosPatios();
        PatioResponseDto? ObterPatioPorId(int id);
        PatioResponseDto? SalvarDadosPatio(PatioRequestDto entity);
        PatioResponseDto? EditarDadosPatio(int id, PatioRequestDto entity);
        PatioResponseDto? DeletarDadosPatio(int id);
        IEnumerable<PatioResponseDto> ObterPatiosPorNomeContendo(string nomePatio);
        IEnumerable<PatioResponseDto> ObterPatiosComMotosDisponiveisAcimaDe(int quantidade);
        IEnumerable<PatioResponseDto> ObterPatiosPorDataPosterior(DateTime data);
        IEnumerable<PatioResponseDto> ObterPatiosPorDataAnterior(DateTime data);
    }
}
