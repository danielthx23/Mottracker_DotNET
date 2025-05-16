using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

namespace Mottracker.Infrastructure.Data.Repositories
{   
    public class PatioRepository : IPatioRepository
        {
            private readonly ApplicationContext _context;
    
            public PatioRepository(ApplicationContext context)
            {
                _context = context;
            }
            
            public IEnumerable<PatioEntity> ObterTodos()
            {
                return _context.Patio
                    .Include(p => p.LayoutPatio)
                    .Include(p => p.EnderecoPatio)
                    .Include(p => p.MotosPatioAtual)
                    .Include(p => p.CamerasPatio)
                    .ToList();
            }

            public PatioEntity? ObterPorId(int id)
            {
                return _context.Patio
                    .Include(p => p.LayoutPatio)
                    .Include(p => p.EnderecoPatio)
                    .Include(p => p.MotosPatioAtual)
                    .Include(p => p.CamerasPatio)
                    .FirstOrDefault(p => p.IdPatio == id);
            }

            public List<PatioEntity> ObterPorIds(List<int> ids)
            {
                return _context.Patio
                    .Where(p => ids.Contains(p.IdPatio))
                    .ToList();
            }
    
            public PatioEntity? Salvar(PatioEntity entity)
            {
                _context.Patio.Add(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public PatioEntity? Atualizar(PatioEntity entity)
            {
                _context.Patio.Update(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public PatioEntity? Deletar(int id)
            {
                var entity = _context.Patio.Find(id);
    
                if (entity is not null)
                {
                    _context.Patio.Remove(entity);
                    _context.SaveChanges();
    
                    return entity;
                }
    
                return null;
            }
    
        }
}
