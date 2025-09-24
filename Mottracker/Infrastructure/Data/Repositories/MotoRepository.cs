using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Enums;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.Data.AppData;
using Mottracker.Application.Models;

namespace Mottracker.Infrastructure.Data.Repositories
{
    public class MotoRepository : IMotoRepository
    {
        private readonly ApplicationContext _context;

        public MotoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PageResultModel<IEnumerable<MotoEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var totalRegistros = await _context.Moto.CountAsync();

            var result = await _context.Moto
                .Include(m => m.ContratoMoto)
                .Include(m => m.MotoPatioAtual)
                .OrderBy(m => m.IdMoto)
                .Skip(Deslocamento)
                .Take(RegistrosRetornado)
                .ToListAsync();

            return new PageResultModel<IEnumerable<MotoEntity>>
            {
                Data = result,
                Deslocamento = Deslocamento,
                RegistrosRetornado = RegistrosRetornado,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<MotoEntity?> ObterPorIdAsync(int id)
        {
            return await _context.Moto
                .Include(m => m.ContratoMoto)
                .Include(m => m.MotoPatioAtual)
                .FirstOrDefaultAsync(m => m.IdMoto == id);
        }

        public async Task<MotoEntity?> SalvarAsync(MotoEntity entity)
        {
            _context.Moto.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<MotoEntity?> AtualizarAsync(MotoEntity entity)
        {
            _context.Moto.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<MotoEntity?> DeletarAsync(int id)
        {
            var entity = await _context.Moto.FindAsync(id);
            if (entity is not null)
            {
                _context.Moto.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }

            return null;
        }

        public async Task<MotoEntity?> ObterPorPlacaAsync(string placa)
        {
            return await _context.Moto
                .Include(m => m.ContratoMoto)
                .Include(m => m.MotoPatioAtual)
                .FirstOrDefaultAsync(m => m.PlacaMoto == placa);
        }

        public async Task<PageResultModel<IEnumerable<MotoEntity>>> ObterPorEstadoAsync(Estados estado, int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var totalRegistros = await _context.Moto
                .Where(m => m.EstadoMoto == estado)
                .CountAsync();

            var result = await _context.Moto
                .Include(m => m.ContratoMoto)
                .Include(m => m.MotoPatioAtual)
                .Where(m => m.EstadoMoto == estado)
                .OrderBy(m => m.IdMoto)
                .Skip(Deslocamento)
                .Take(RegistrosRetornado)
                .ToListAsync();

            return new PageResultModel<IEnumerable<MotoEntity>>
            {
                Data = result,
                Deslocamento = Deslocamento,
                RegistrosRetornado = RegistrosRetornado,
                TotalRegistros = totalRegistros
            };
        }
    }
}