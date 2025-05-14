using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

namespace Mottracker.Infrastructure.Data.Repositories
{   
    public class CameraRepository : ICameraRepository
        {
            private readonly ApplicationContext _context;
    
            public CameraRepository(ApplicationContext context)
            {
                _context = context;
            }
            
            public IEnumerable<CameraEntity> ObterTodos()
            {
                var Camera = _context.Camera.ToList();
    
                return Camera;
            }
    
            public CameraEntity? ObterPorId(int id)
            {
                var Camera = _context.Camera.Find(id);
    
                return Camera;
            }
    
            public CameraEntity? Salvar(CameraEntity entity)
            {
                _context.Camera.Add(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public CameraEntity? Atualizar(CameraEntity entity)
            {
                _context.Camera.Update(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public CameraEntity? Deletar(int id)
            {
                var entity = _context.Camera.Find(id);
    
                if (entity is not null)
                {
                    _context.Camera.Remove(entity);
                    _context.SaveChanges();
    
                    return entity;
                }
    
                return null;
            }
    
        }
}
