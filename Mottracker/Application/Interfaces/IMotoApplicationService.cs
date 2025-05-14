using Mottracker.Domain.Entities;

namespace Mottracker.Application.Interfaces
{   
    public interface IMotoApplicationService
    {
        IEnumerable<MotoEntity> ObterTodasMotos();
        MotoEntity? ObterMotoPorId(int id);
        MotoEntity? SalvarDadosMoto(MotoEntity entity);
        MotoEntity? EditarDadosMoto(int id, MotoEntity entity);
        MotoEntity? DeletarDadosMoto(int id);
    }
}
