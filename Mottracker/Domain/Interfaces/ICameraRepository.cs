using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{   
    public interface ICameraRepository
    {
        IEnumerable<CameraEntity> ObterTodos();
        CameraEntity? ObterPorId(int id);
        CameraEntity? Salvar(CameraEntity entity);
        CameraEntity? Atualizar(CameraEntity entity);
        CameraEntity? Deletar(int id);
    }
}
