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
                var Patio = _context.Patio.ToList();
    
                return Patio;
            }
    
            public PatioEntity? ObterPorId(int id)
            {
                var Patio = _context.Patio.Find(id);
    
                return Patio;
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
