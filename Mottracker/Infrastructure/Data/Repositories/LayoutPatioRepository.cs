using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

namespace Mottracker.Infrastructure.Data.Repositories
{   
    public class LayoutPatioRepository : ILayoutPatioRepository
        {
            private readonly ApplicationContext _context;
    
            public LayoutPatioRepository(ApplicationContext context)
            {
                _context = context;
            }
            
            public IEnumerable<LayoutPatioEntity> ObterTodos()
            {
                var LayoutPatio = _context.LayoutPatio.ToList();
    
                return LayoutPatio;
            }
    
            public LayoutPatioEntity? ObterPorId(int id)
            {
                var LayoutPatio = _context.LayoutPatio.Find(id);
    
                return LayoutPatio;
            }
    
            public LayoutPatioEntity? Salvar(LayoutPatioEntity entity)
            {
                _context.LayoutPatio.Add(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public LayoutPatioEntity? Atualizar(LayoutPatioEntity entity)
            {
                _context.LayoutPatio.Update(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public LayoutPatioEntity? Deletar(int id)
            {
                var entity = _context.LayoutPatio.Find(id);
    
                if (entity is not null)
                {
                    _context.LayoutPatio.Remove(entity);
                    _context.SaveChanges();
    
                    return entity;
                }
    
                return null;
            }
    
        }
}
