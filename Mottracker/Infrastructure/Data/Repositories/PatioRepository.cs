using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.Data.AppData;
using Mottracker.Application.Models;

namespace Mottracker.Infrastructure.Data.Repositories
{
    public class PatioRepository : IPatioRepository
    {
        private readonly ApplicationContext _context;

        public PatioRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PageResultModel<IEnumerable<PatioEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var totalRegistros = await _context.Patio.CountAsync();

            var result = await _context.Patio
                .Include(p => p.MotosPatioAtual)
                .Include(p => p.CamerasPatio)
                .Include(p => p.LayoutPatio)
                .Include(p => p.EnderecoPatio)
                .OrderBy(p => p.IdPatio)
                .Skip(Deslocamento)
                .Take(RegistrosRetornado)
                .ToListAsync();

            return new PageResultModel<IEnumerable<PatioEntity>>
            {
                Data = result,
                Deslocamento = Deslocamento,
                RegistrosRetornado = RegistrosRetornado,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<PatioEntity?> ObterPorIdAsync(int id)
        {
            return await _context.Patio
                .Include(p => p.MotosPatioAtual)
                .Include(p => p.CamerasPatio)
                .Include(p => p.LayoutPatio)
                .Include(p => p.EnderecoPatio)
                .FirstOrDefaultAsync(p => p.IdPatio == id);
        }

        public async Task<PatioEntity?> SalvarAsync(PatioEntity entity)
        {
            _context.Patio.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<PatioEntity?> AtualizarAsync(PatioEntity entity)
        {
            _context.Patio.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<PatioEntity?> DeletarAsync(int id)
        {
            var entity = await _context.Patio.FindAsync(id);
            if (entity is not null)
            {
                _context.Patio.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }

            return null;
        }
    }
}