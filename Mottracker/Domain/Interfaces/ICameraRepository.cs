using Mottracker.Domain.Entities;
using Mottracker.Domain.Enums;

namespace Mottracker.Domain.Interfaces
{   
    public interface ICameraRepository
    {
        IEnumerable<CameraEntity> ObterTodos();
        CameraEntity? ObterPorId(int id);
        CameraEntity? Salvar(CameraEntity entity);
        CameraEntity? Atualizar(CameraEntity entity);
        CameraEntity? Deletar(int id);
        IEnumerable<CameraEntity> ObterPorNome(string nomeCamera);
        IEnumerable<CameraEntity> ObterPorStatus(CameraStatus status);
    }
}