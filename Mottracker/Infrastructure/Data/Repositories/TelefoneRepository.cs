using Microsoft.EntityFrameworkCore;
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
            return _context.Telefone
                .Include(t => t.Usuario)
                .ToList();
        }

        public TelefoneEntity? ObterPorId(int id)
        {
            return _context.Telefone
                .Include(t => t.Usuario)
                .FirstOrDefault(t => t.IdTelefone == id);
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

        public IEnumerable<TelefoneEntity> ObterPorNumero(string numero)
        {
            return _context.Telefone
                .Where(t => t.Numero == numero)
                .ToList();
        }

        public IEnumerable<TelefoneEntity> ObterPorIdUsuario(long usuarioId)
        {
            return _context.Telefone
                .Where(t => t.UsuarioId == usuarioId)
                .ToList();
        }

        public IEnumerable<TelefoneEntity> ObterPorTipo(string tipo)
        {
            return _context.Telefone
                .Where(t => t.Tipo == tipo)
                .ToList();
        }

    }
}