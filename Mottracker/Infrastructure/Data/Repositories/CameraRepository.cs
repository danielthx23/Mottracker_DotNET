using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Enums;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.Data.AppData;
using Mottracker.Application.Models;

namespace Mottracker.Infrastructure.Data.Repositories
{
    public class CameraRepository : ICameraRepository
    {
        private readonly ApplicationContext _context;

        public CameraRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PageResultModel<IEnumerable<CameraEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var totalRegistros = await _context.Camera.CountAsync();

            var result = await _context.Camera
                .Include(c => c.Patio)
                .OrderBy(c => c.IdCamera)
                .Skip(Deslocamento)
                .Take(RegistrosRetornado)
                .ToListAsync();

            return new PageResultModel<IEnumerable<CameraEntity>>
            {
                Data = result,
                Deslocamento = Deslocamento,
                RegistrosRetornado = RegistrosRetornado,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<CameraEntity?> ObterPorIdAsync(int id)
        {
            return await _context.Camera
                .Include(c => c.Patio)
                .FirstOrDefaultAsync(c => c.IdCamera == id);
        }

        public async Task<CameraEntity?> SalvarAsync(CameraEntity entity)
        {
            _context.Camera.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<CameraEntity?> AtualizarAsync(CameraEntity entity)
        {
            _context.Camera.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<CameraEntity?> DeletarAsync(int id)
        {
            var entity = await _context.Camera.FindAsync(id);
            if (entity is not null)
            {
                _context.Camera.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }

            return null;
        }

        public async Task<PageResultModel<IEnumerable<CameraEntity>>> ObterPorNomeAsync(string nome, int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var totalRegistros = await _context.Camera
                .Where(c => c.NomeCamera.ToLower().Contains(nome.ToLower()))
                .CountAsync();

            var result = await _context.Camera
                .Include(c => c.Patio)
                .Where(c => c.NomeCamera.ToLower().Contains(nome.ToLower()))
                .OrderBy(c => c.IdCamera)
                .Skip(Deslocamento)
                .Take(RegistrosRetornado)
                .ToListAsync();

            return new PageResultModel<IEnumerable<CameraEntity>>
            {
                Data = result,
                Deslocamento = Deslocamento,
                RegistrosRetornado = RegistrosRetornado,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<PageResultModel<IEnumerable<CameraEntity>>> ObterPorStatusAsync(CameraStatus status, int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var totalRegistros = await _context.Camera
                .Where(c => c.Status == status)
                .CountAsync();

            var result = await _context.Camera
                .Include(c => c.Patio)
                .Where(c => c.Status == status)
                .OrderBy(c => c.IdCamera)
                .Skip(Deslocamento)
                .Take(RegistrosRetornado)
                .ToListAsync();

            return new PageResultModel<IEnumerable<CameraEntity>>
            {
                Data = result,
                Deslocamento = Deslocamento,
                RegistrosRetornado = RegistrosRetornado,
                TotalRegistros = totalRegistros
            };
        }
    }
}
