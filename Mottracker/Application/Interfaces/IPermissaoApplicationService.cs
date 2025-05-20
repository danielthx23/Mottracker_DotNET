using Mottracker.Application.Dtos.Permissao;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{   
    public interface IPermissaoApplicationService
    {
        IEnumerable<PermissaoResponseDto> ObterTodosPermissoes();
        PermissaoResponseDto? ObterPermissaoPorId(int id);
        PermissaoResponseDto? SalvarDadosPermissao(PermissaoRequestDto entity);
        PermissaoResponseDto? EditarDadosPermissao(int id, PermissaoRequestDto entity);
        PermissaoResponseDto? DeletarDadosPermissao(int id);
        IEnumerable<PermissaoResponseDto> ObterPermissoesPorNomeContendo(string nomePermissao);
        IEnumerable<PermissaoResponseDto> ObterPermissoesPorDescricaoContendo(string descricao);
    }
}
