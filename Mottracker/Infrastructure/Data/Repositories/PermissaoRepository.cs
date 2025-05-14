using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

namespace Mottracker.Infrastructure.Data.Repositories
{   
    public class PermissaoRepository : IPermissaoRepository
        {
            private readonly ApplicationContext _context;
    
            public PermissaoRepository(ApplicationContext context)
            {
                _context = context;
            }
            
            public IEnumerable<PermissaoEntity> ObterTodos()
            {
                var Permissao = _context.Permissao.ToList();
    
                return Permissao;
            }
    
            public PermissaoEntity? ObterPorId(int id)
            {
                var Permissao = _context.Permissao.Find(id);
    
                return Permissao;
            }
    
            public PermissaoEntity? Salvar(PermissaoEntity entity)
            {
                _context.Permissao.Add(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public PermissaoEntity? Atualizar(PermissaoEntity entity)
            {
                _context.Permissao.Update(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public PermissaoEntity? Deletar(int id)
            {
                var entity = _context.Permissao.Find(id);
    
                if (entity is not null)
                {
                    _context.Permissao.Remove(entity);
                    _context.SaveChanges();
    
                    return entity;
                }
    
                return null;
            }
    
        }
}
