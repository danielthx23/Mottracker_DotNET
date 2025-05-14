using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

namespace Mottracker.Infrastructure.Data.Repositories
{   
    public class UsuarioPermissaoRepository : IUsuarioPermissaoRepository
        {
            private readonly ApplicationContext _context;
    
            public UsuarioPermissaoRepository(ApplicationContext context)
            {
                _context = context;
            }
            
            public IEnumerable<UsuarioPermissaoEntity> ObterTodos()
            {
                var UsuarioPermissao = _context.UsuarioPermissao.ToList();
    
                return UsuarioPermissao;
            }
    
            public UsuarioPermissaoEntity? ObterPorId(int id)
            {
                var UsuarioPermissao = _context.UsuarioPermissao.Find(id);
    
                return UsuarioPermissao;
            }
    
            public UsuarioPermissaoEntity? Salvar(UsuarioPermissaoEntity entity)
            {
                _context.UsuarioPermissao.Add(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public UsuarioPermissaoEntity? Atualizar(UsuarioPermissaoEntity entity)
            {
                _context.UsuarioPermissao.Update(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public UsuarioPermissaoEntity? Deletar(int id)
            {
                var entity = _context.UsuarioPermissao.Find(id);
    
                if (entity is not null)
                {
                    _context.UsuarioPermissao.Remove(entity);
                    _context.SaveChanges();
    
                    return entity;
                }
    
                return null;
            }
    
        }
}
