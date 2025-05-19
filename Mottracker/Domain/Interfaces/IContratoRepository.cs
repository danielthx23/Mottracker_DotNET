using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{   
    public interface IContratoRepository
    {
        IEnumerable<ContratoEntity> ObterTodos();
        ContratoEntity? ObterPorId(int id);
        ContratoEntity? Salvar(ContratoEntity entity);
        ContratoEntity? Atualizar(ContratoEntity entity);
        ContratoEntity? Deletar(int id);
        IEnumerable<ContratoEntity> ObterPorAtivoContrato(int ativoContrato);
        IEnumerable<ContratoEntity> ObterPorUsuarioId(long usuarioId);
        IEnumerable<ContratoEntity> ObterPorMotoId(long motoId);
        IEnumerable<ContratoEntity> ObterContratosNaoExpirados(DateTime dataAtual);
        IEnumerable<ContratoEntity> ObterPorRenovacaoAutomatica(int renovacaoAutomatica);
        IEnumerable<ContratoEntity> ObterPorDataEntradaEntre(DateTime dataInicio, DateTime dataFim);
    }
}
