using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

namespace Mottracker.Infrastructure.Data.Repositories
{
    public class LayoutPatioRepository : ILayoutPatioRepository
    {
        private readonly ApplicationContext _context;

        public LayoutPatioRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PageResultModel<IEnumerable<LayoutPatioEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var totalRegistros = await _context.LayoutPatio.CountAsync();

            var result = await _context.LayoutPatio
                .Include(l => l.PatioLayoutPatio)
                .Include(l => l.QrCodesLayoutPatio)
                .OrderBy(l => l.IdLayoutPatio)
                .Skip(Deslocamento)
                .Take(RegistrosRetornado)
                .ToListAsync();

            return new PageResultModel<IEnumerable<LayoutPatioEntity>>
            {
                Data = result,
                Deslocamento = Deslocamento,
                RegistrosRetornado = RegistrosRetornado,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<LayoutPatioEntity?> ObterPorIdAsync(int id)
        {
            return await _context.LayoutPatio
                .Include(l => l.PatioLayoutPatio)
                .Include(l => l.QrCodesLayoutPatio)
                .FirstOrDefaultAsync(l => l.IdLayoutPatio == id);
        }

        public async Task<LayoutPatioEntity?> SalvarAsync(LayoutPatioEntity entity)
        {
            _context.LayoutPatio.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<LayoutPatioEntity?> AtualizarAsync(LayoutPatioEntity entity)
        {
            _context.LayoutPatio.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<LayoutPatioEntity?> DeletarAsync(int id)
        {
            var entity = await _context.LayoutPatio.FindAsync(id);
            if (entity is not null)
            {
                _context.LayoutPatio.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }

            return null;
        }

        // Métodos de consulta específicos (sem paginação)
        public async Task<PageResultModel<IEnumerable<LayoutPatioEntity>>> ObterTodasAsync()
        {
            var result = await _context.LayoutPatio
                .Include(l => l.PatioLayoutPatio)
                .Include(l => l.QrCodesLayoutPatio)
                .OrderBy(l => l.IdLayoutPatio)
                .ToListAsync();

            return new PageResultModel<IEnumerable<LayoutPatioEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<LayoutPatioEntity>>> ObterPorPatioIdAsync(int patioId)
        {
            var result = await _context.LayoutPatio
                .Include(l => l.PatioLayoutPatio)
                .Include(l => l.QrCodesLayoutPatio)
                .Where(l => l.PatioLayoutPatioId == patioId)
                .OrderBy(l => l.IdLayoutPatio)
                .ToListAsync();

            return new PageResultModel<IEnumerable<LayoutPatioEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<LayoutPatioEntity>>> ObterPorDataCriacaoAsync(DateTime dataCriacao)
        {
            var result = await _context.LayoutPatio
                .Include(l => l.PatioLayoutPatio)
                .Include(l => l.QrCodesLayoutPatio)
                .Where(l => l.DataCriacao.Date == dataCriacao.Date)
                .OrderBy(l => l.IdLayoutPatio)
                .ToListAsync();

            return new PageResultModel<IEnumerable<LayoutPatioEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }
    }
}