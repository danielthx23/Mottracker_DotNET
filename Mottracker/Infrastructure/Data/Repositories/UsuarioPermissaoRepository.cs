using Microsoft.EntityFrameworkCore;
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
            return _context.UsuarioPermissao
                .Include(up => up.Usuario)
                .Include(up => up.Permissao)
                .ToList();
        }

        public UsuarioPermissaoEntity? ObterPorId(int usuarioId, int permissaoId)
        {
            return _context.UsuarioPermissao
                .Include(up => up.Usuario)
                .Include(up => up.Permissao)
                .FirstOrDefault(up => up.UsuarioId == usuarioId && up.PermissaoId == permissaoId);
        }
        
        public List<UsuarioPermissaoEntity> ObterPorIds(List<(int usuarioId, int permissaoId)> chaves)
        {
            return _context.UsuarioPermissao
                .Include(up => up.Usuario)
                .Include(up => up.Permissao)
                .Where(up => chaves.Any(c => c.usuarioId == up.UsuarioId && c.permissaoId == up.PermissaoId))
                .ToList();
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

        public UsuarioPermissaoEntity? Deletar(int usuarioId, int permissaoId)
        {
            var entity = _context.UsuarioPermissao
                .FirstOrDefault(up => up.UsuarioId == usuarioId && up.PermissaoId == permissaoId);

            if (entity is not null)
            {
                _context.UsuarioPermissao.Remove(entity);
                _context.SaveChanges();

                return entity;
            }

            return null;
        }

        public IEnumerable<UsuarioPermissaoEntity> ObterPorIdUsuario(long usuarioId)
        {
            return _context.UsuarioPermissao
                .Include(up => up.Usuario)
                .Include(up => up.Permissao)
                .Where(up => up.UsuarioId == usuarioId)
                .ToList();
        }

        public IEnumerable<UsuarioPermissaoEntity> ObterPorIdPermissao(long permissaoId)
        {
            return _context.UsuarioPermissao
                .Include(up => up.Usuario)
                .Include(up => up.Permissao)
                .Where(up => up.PermissaoId == permissaoId)
                .ToList();
        }

    }
}