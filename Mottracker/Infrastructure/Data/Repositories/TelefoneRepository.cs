using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

namespace Mottracker.Infrastructure.Data.Repositories
{   
    public class TelefoneRepository : ITelefoneRepository
        {
            private readonly ApplicationContext _context;
    
            public TelefoneRepository(ApplicationContext context)
            {
                _context = context;
            }
            
            public IEnumerable<TelefoneEntity> ObterTodos()
            {
                var Telefone = _context.Telefone.ToList();
    
                return Telefone;
            }
    
            public TelefoneEntity? ObterPorId(int id)
            {
                var Telefone = _context.Telefone.Find(id);
    
                return Telefone;
            }
    
            public TelefoneEntity? Salvar(TelefoneEntity entity)
            {
                _context.Telefone.Add(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public TelefoneEntity? Atualizar(TelefoneEntity entity)
            {
                _context.Telefone.Update(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public TelefoneEntity? Deletar(int id)
            {
                var entity = _context.Telefone.Find(id);
    
                if (entity is not null)
                {
                    _context.Telefone.Remove(entity);
                    _context.SaveChanges();
    
                    return entity;
                }
    
                return null;
            }
    
        }
}
