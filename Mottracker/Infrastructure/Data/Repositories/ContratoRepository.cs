using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.Data.AppData;
using Mottracker.Application.Models;

namespace Mottracker.Infrastructure.Data.Repositories
{
    public class ContratoRepository : IContratoRepository
    {
        private readonly ApplicationContext _context;

        public ContratoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var totalRegistros = await _context.Contrato.CountAsync();

            var result = await _context.Contrato
                .Include(c => c.UsuarioContrato)
                .Include(c => c.MotoContrato)
                .OrderBy(c => c.IdContrato)
                .Skip(Deslocamento)
                .Take(RegistrosRetornado)
                .ToListAsync();

            return new PageResultModel<IEnumerable<ContratoEntity>>
            {
                Data = result,
                Deslocamento = Deslocamento,
                RegistrosRetornado = RegistrosRetornado,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<ContratoEntity?> ObterPorIdAsync(int id)
        {
            return await _context.Contrato
                .Include(c => c.UsuarioContrato)
                .Include(c => c.MotoContrato)
                .FirstOrDefaultAsync(c => c.IdContrato == id);
        }

        public async Task<ContratoEntity?> SalvarAsync(ContratoEntity entity)
        {
            _context.Contrato.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ContratoEntity?> AtualizarAsync(ContratoEntity entity)
        {
            _context.Contrato.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ContratoEntity?> DeletarAsync(int id)
        {
            var entity = await _context.Contrato.FindAsync(id);
            if (entity is not null)
            {
                _context.Contrato.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }

            return null;
        }

        public async Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterPorAtivoAsync(int ativo, int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var totalRegistros = await _context.Contrato
                .Where(c => c.AtivoContrato == ativo)
                .CountAsync();

            var result = await _context.Contrato
                .Include(c => c.UsuarioContrato)
                .Include(c => c.MotoContrato)
                .Where(c => c.AtivoContrato == ativo)
                .OrderBy(c => c.IdContrato)
                .Skip(Deslocamento)
                .Take(RegistrosRetornado)
                .ToListAsync();

            return new PageResultModel<IEnumerable<ContratoEntity>>
            {
                Data = result,
                Deslocamento = Deslocamento,
                RegistrosRetornado = RegistrosRetornado,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterPorUsuarioAsync(long usuarioId, int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var totalRegistros = await _context.Contrato
                .Where(c => c.UsuarioContratoId == usuarioId)
                .CountAsync();

            var result = await _context.Contrato
                .Include(c => c.UsuarioContrato)
                .Include(c => c.MotoContrato)
                .Where(c => c.UsuarioContratoId == usuarioId)
                .OrderBy(c => c.IdContrato)
                .Skip(Deslocamento)
                .Take(RegistrosRetornado)
                .ToListAsync();

            return new PageResultModel<IEnumerable<ContratoEntity>>
            {
                Data = result,
                Deslocamento = Deslocamento,
                RegistrosRetornado = RegistrosRetornado,
                TotalRegistros = totalRegistros
            };
        }
    }
}