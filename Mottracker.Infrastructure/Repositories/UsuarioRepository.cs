using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

namespace Mottracker.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationContext _context;

        public UsuarioRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PageResultModel<IEnumerable<UsuarioEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var totalRegistros = await _context.Usuario.CountAsync();

            var result = await _context.Usuario
                .Include(u => u.ContratoUsuario)
                .Include(u => u.Telefones)
                .Include(u => u.UsuarioPermissoes)
                .OrderBy(u => u.IdUsuario)
                .Skip(Deslocamento)
                .Take(RegistrosRetornado)
                .ToListAsync();

            return new PageResultModel<IEnumerable<UsuarioEntity>>
            {
                Data = result,
                Deslocamento = Deslocamento,
                RegistrosRetornado = RegistrosRetornado,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<UsuarioEntity?> ObterPorIdAsync(int id)
        {
            return await _context.Usuario
                .Include(u => u.ContratoUsuario)
                .Include(u => u.Telefones)
                .Include(u => u.UsuarioPermissoes)
                .FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        public async Task<UsuarioEntity?> SalvarAsync(UsuarioEntity entity)
        {
            _context.Usuario.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<UsuarioEntity?> AtualizarAsync(UsuarioEntity entity)
        {
            _context.Usuario.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<UsuarioEntity?> DeletarAsync(int id)
        {
            var entity = await _context.Usuario.FindAsync(id);
            if (entity is not null)
            {
                _context.Usuario.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }

            return null;
        }

        public async Task<UsuarioEntity?> ObterPorEmailAsync(string email)
        {
            return await _context.Usuario
                .Include(u => u.ContratoUsuario)
                .Include(u => u.Telefones)
                .Include(u => u.UsuarioPermissoes)
                .FirstOrDefaultAsync(u => u.EmailUsuario == email);
        }

        // Métodos de consulta específicos (sem paginação)
        public async Task<PageResultModel<IEnumerable<UsuarioEntity>>> ObterTodasAsync()
        {
            var result = await _context.Usuario
                .Include(u => u.ContratoUsuario)
                .Include(u => u.Telefones)
                .Include(u => u.UsuarioPermissoes)
                .OrderBy(u => u.IdUsuario)
                .ToListAsync();

            return new PageResultModel<IEnumerable<UsuarioEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }
    }
}