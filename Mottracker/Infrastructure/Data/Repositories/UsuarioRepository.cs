using Microsoft.EntityFrameworkCore;
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
            return _context.Usuario
                .Include(u => u.ContratoUsuario)
                .Include(u => u.Telefones)
                .Include(u => u.UsuarioPermissoes)
                .ToList();
        }

        public UsuarioEntity? ObterPorId(int id)
        {
            return _context.Usuario
                .Include(u => u.ContratoUsuario)
                .Include(u => u.Telefones)
                .Include(u => u.UsuarioPermissoes)
                .FirstOrDefault(u => u.IdUsuario == id);
        }

        public List<UsuarioEntity> ObterPorIds(List<int> ids)
        {
            return _context.Usuario
                .Where(u => ids.Contains(u.IdUsuario))
                .Include(u => u.ContratoUsuario)
                .Include(u => u.Telefones)
                .Include(u => u.UsuarioPermissoes)
                .ToList();
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

        public UsuarioEntity? ObterPorEmail(string emailUsuario)
        {
            return _context.Usuario
                .Include(u => u.ContratoUsuario)
                .Include(u => u.Telefones)
                .Include(u => u.UsuarioPermissoes)
                .FirstOrDefault(u => u.EmailUsuario.ToLower() == emailUsuario.ToLower());
        }

    }
}
