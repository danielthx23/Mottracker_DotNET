using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{   
    public interface IContratoApplicationService
    {
        IEnumerable<ContratoEntity> ObterTodosContratos();
        ContratoEntity? ObterContratoPorId(int id);
        ContratoEntity? SalvarDadosContrato(ContratoEntity entity);
        ContratoEntity? EditarDadosContrato(int id, ContratoEntity entity);
        ContratoEntity? DeletarDadosContrato(int id);
    }
}
