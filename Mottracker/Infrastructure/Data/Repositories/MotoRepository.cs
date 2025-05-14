using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

namespace Mottracker.Infrastructure.Data.Repositories
{   
    public class MotoRepository : IMotoRepository
        {
            private readonly ApplicationContext _context;
    
            public MotoRepository(ApplicationContext context)
            {
                _context = context;
            }
            
            public IEnumerable<MotoEntity> ObterTodos()
            {
                var Moto = _context.Moto.ToList();
    
                return Moto;
            }
    
            public MotoEntity? ObterPorId(int id)
            {
                var Moto = _context.Moto.Find(id);
    
                return Moto;
            }
    
            public MotoEntity? Salvar(MotoEntity entity)
            {
                _context.Moto.Add(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public MotoEntity? Atualizar(MotoEntity entity)
            {
                _context.Moto.Update(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public MotoEntity? Deletar(int id)
            {
                var entity = _context.Moto.Find(id);
    
                if (entity is not null)
                {
                    _context.Moto.Remove(entity);
                    _context.SaveChanges();
    
                    return entity;
                }
    
                return null;
            }
    
        }
}
