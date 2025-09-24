using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.Data.AppData;
using Mottracker.Application.Models;

namespace Mottracker.Infrastructure.Data.Repositories
{
    public class QrCodePontoRepository : IQrCodePontoRepository
    {
        private readonly ApplicationContext _context;

        public QrCodePontoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PageResultModel<IEnumerable<QrCodePontoEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var totalRegistros = await _context.QrCodePonto.CountAsync();

            var result = await _context.QrCodePonto
                .Include(q => q.LayoutPatioQrCode)
                .OrderBy(q => q.IdQrCodePonto)
                .Skip(Deslocamento)
                .Take(RegistrosRetornado)
                .ToListAsync();

            return new PageResultModel<IEnumerable<QrCodePontoEntity>>
            {
                Data = result,
                Deslocamento = Deslocamento,
                RegistrosRetornado = RegistrosRetornado,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<QrCodePontoEntity?> ObterPorIdAsync(int id)
        {
            return await _context.QrCodePonto
                .Include(q => q.LayoutPatioQrCode)
                .FirstOrDefaultAsync(q => q.IdQrCodePonto == id);
        }

        public async Task<QrCodePontoEntity?> SalvarAsync(QrCodePontoEntity entity)
        {
            _context.QrCodePonto.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<QrCodePontoEntity?> AtualizarAsync(QrCodePontoEntity entity)
        {
            _context.QrCodePonto.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<QrCodePontoEntity?> DeletarAsync(int id)
        {
            var entity = await _context.QrCodePonto.FindAsync(id);
            if (entity is not null)
            {
                _context.QrCodePonto.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }

            return null;
        }
    }
}