using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

namespace Mottracker.Infrastructure.Data.Repositories
{   
    public class UsuarioRepository : IUsuarioRepository
        {
            private readonly ApplicationContext _context;
    
            public UsuarioRepository(ApplicationContext context)
            {
                _context = context;
            }
            
            public IEnumerable<UsuarioEntity> ObterTodos()
            {
                var Usuario = _context.Usuario.ToList();
    
                return Usuario;
            }
    
            public UsuarioEntity? ObterPorId(int id)
            {
                var Usuario = _context.Usuario.Find(id);
    
                return Usuario;
            }
    
            public UsuarioEntity? Salvar(UsuarioEntity entity)
            {
                _context.Usuario.Add(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public UsuarioEntity? Atualizar(UsuarioEntity entity)
            {
                _context.Usuario.Update(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public UsuarioEntity? Deletar(int id)
            {
                var entity = _context.Usuario.Find(id);
    
                if (entity is not null)
                {
                    _context.Usuario.Remove(entity);
                    _context.SaveChanges();
    
                    return entity;
                }
    
                return null;
            }
    
        }
}
