using Microsoft.EntityFrameworkCore;
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
                return _context.Permissao
                    .Include(p => p.UsuarioPermissoes)
                    .ToList();
            }
            
            public PermissaoEntity? ObterPorId(int id)
            {
                return _context.Permissao
                    .Include(p => p.UsuarioPermissoes)
                    .FirstOrDefault(p => p.IdPermissao == id);
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

            public IEnumerable<PermissaoEntity> ObterPorNomeContendo(string nomePermissao)
            {
                return _context.Permissao
                    .Include(p => p.UsuarioPermissoes)
                    .Where(p => EF.Functions.Like(p.NomePermissao.ToLower(), $"%{nomePermissao.ToLower()}%"))
                    .ToList();
            }

            public IEnumerable<PermissaoEntity> ObterPorDescricaoContendo(string descricao)
            {
                return _context.Permissao
                    .Include(p => p.UsuarioPermissoes)
                    .Where(p => EF.Functions.Like(p.Descricao.ToLower(), $"%{descricao.ToLower()}%"))
                    .ToList();
            }

        }
}
