using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{
    public interface IContratoRepository
    {
        // Métodos com paginação
        Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterPorAtivoAsync(int ativo, int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterPorUsuarioAsync(long usuarioId, int Deslocamento = 0, int RegistrosRetornado = 3);
        
        // Métodos básicos CRUD
        Task<ContratoEntity?> ObterPorIdAsync(int id);
        Task<ContratoEntity?> SalvarAsync(ContratoEntity entity);
        Task<ContratoEntity?> AtualizarAsync(ContratoEntity entity);
        Task<ContratoEntity?> DeletarAsync(int id);
        
        // Métodos de consulta específicos (sem paginação)
        Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterTodasAsync();
        Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterPorAtivoAsync(int ativo);
        Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterPorUsuarioAsync(long usuarioId);
        Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterPorMotoAsync(long motoId);
        Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterContratosNaoExpiradosAsync(DateTime dataAtual);
        Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterPorRenovacaoAutomaticaAsync(int renovacaoAutomatica);
        Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterPorDataEntradaEntreAsync(DateTime dataInicio, DateTime dataFim);
    }
}
