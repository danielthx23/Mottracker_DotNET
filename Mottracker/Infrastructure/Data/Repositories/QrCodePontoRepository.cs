using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

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
                .Include(q => q.LayoutPatio)
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
                .Include(q => q.LayoutPatio)
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

        // Métodos de consulta específicos (sem paginação)
        public async Task<PageResultModel<IEnumerable<QrCodePontoEntity>>> ObterTodasAsync()
        {
            var result = await _context.QrCodePonto
                .Include(q => q.LayoutPatio)
                .OrderBy(q => q.IdQrCodePonto)
                .ToListAsync();

            return new PageResultModel<IEnumerable<QrCodePontoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<QrCodePontoEntity>>> ObterPorIdentificadorAsync(string identificador)
        {
            var result = await _context.QrCodePonto
                .Include(q => q.LayoutPatio)
                .Where(q => q.IdentificadorQrCode.Contains(identificador))
                .OrderBy(q => q.IdQrCodePonto)
                .ToListAsync();

            return new PageResultModel<IEnumerable<QrCodePontoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<QrCodePontoEntity>>> ObterPorLayoutPatioIdAsync(int layoutPatioId)
        {
            var result = await _context.QrCodePonto
                .Include(q => q.LayoutPatio)
                .Where(q => q.LayoutPatioId == layoutPatioId)
                .OrderBy(q => q.IdQrCodePonto)
                .ToListAsync();

            return new PageResultModel<IEnumerable<QrCodePontoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<QrCodePontoEntity>>> ObterPorPosXRangeAsync(float posXMin, float posXMax)
        {
            var result = await _context.QrCodePonto
                .Include(q => q.LayoutPatio)
                .Where(q => q.PosX >= posXMin && q.PosX <= posXMax)
                .OrderBy(q => q.IdQrCodePonto)
                .ToListAsync();

            return new PageResultModel<IEnumerable<QrCodePontoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<QrCodePontoEntity>>> ObterPorPosYRangeAsync(float posYMin, float posYMax)
        {
            var result = await _context.QrCodePonto
                .Include(q => q.LayoutPatio)
                .Where(q => q.PosY >= posYMin && q.PosY <= posYMax)
                .OrderBy(q => q.IdQrCodePonto)
                .ToListAsync();

            return new PageResultModel<IEnumerable<QrCodePontoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }
    }
}