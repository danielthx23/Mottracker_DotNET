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

        public async Task<PageResultModel<IEnumerable<TelefoneEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var totalRegistros = await _context.Telefone.CountAsync();

            var result = await _context.Telefone
                .Include(t => t.Usuario)
                .OrderBy(t => t.IdTelefone)
                .Skip(Deslocamento)
                .Take(RegistrosRetornado)
                .ToListAsync();

            return new PageResultModel<IEnumerable<TelefoneEntity>>
            {
                Data = result,
                Deslocamento = Deslocamento,
                RegistrosRetornado = RegistrosRetornado,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<TelefoneEntity?> ObterPorIdAsync(int id)
        {
            return await _context.Telefone
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(t => t.IdTelefone == id);
        }

        public async Task<TelefoneEntity?> SalvarAsync(TelefoneEntity entity)
        {
            _context.Telefone.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TelefoneEntity?> AtualizarAsync(TelefoneEntity entity)
        {
            _context.Telefone.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TelefoneEntity?> DeletarAsync(int id)
        {
            var entity = await _context.Telefone.FindAsync(id);
            if (entity is not null)
            {
                _context.Telefone.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }

            return null;
        }

        // Métodos de consulta específicos (sem paginação)
        public async Task<PageResultModel<IEnumerable<TelefoneEntity>>> ObterTodasAsync()
        {
            var result = await _context.Telefone
                .Include(t => t.Usuario)
                .OrderBy(t => t.IdTelefone)
                .ToListAsync();

            return new PageResultModel<IEnumerable<TelefoneEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<TelefoneEntity>>> ObterPorNumeroAsync(string numero)
        {
            var result = await _context.Telefone
                .Include(t => t.Usuario)
                .Where(t => t.Numero.Contains(numero))
                .OrderBy(t => t.IdTelefone)
                .ToListAsync();

            return new PageResultModel<IEnumerable<TelefoneEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<TelefoneEntity>>> ObterPorUsuarioIdAsync(int usuarioId)
        {
            var result = await _context.Telefone
                .Include(t => t.Usuario)
                .Where(t => t.UsuarioId == usuarioId)
                .OrderBy(t => t.IdTelefone)
                .ToListAsync();

            return new PageResultModel<IEnumerable<TelefoneEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<TelefoneEntity>>> ObterPorTipoAsync(string tipo)
        {
            var result = await _context.Telefone
                .Include(t => t.Usuario)
                .Where(t => t.Tipo == tipo)
                .OrderBy(t => t.IdTelefone)
                .ToListAsync();

            return new PageResultModel<IEnumerable<TelefoneEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }
    }
}