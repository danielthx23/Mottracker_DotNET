using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

namespace Mottracker.Infrastructure.Repositories
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

        // Métodos de consulta específicos (sem paginação)
        public async Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterTodasAsync()
        {
            var result = await _context.Contrato
                .Include(c => c.UsuarioContrato)
                .Include(c => c.MotoContrato)
                .OrderBy(c => c.IdContrato)
                .ToListAsync();

            return new PageResultModel<IEnumerable<ContratoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterPorAtivoAsync(int ativo)
        {
            var result = await _context.Contrato
                .Include(c => c.UsuarioContrato)
                .Include(c => c.MotoContrato)
                .Where(c => c.AtivoContrato == ativo)
                .OrderBy(c => c.IdContrato)
                .ToListAsync();

            return new PageResultModel<IEnumerable<ContratoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterPorUsuarioAsync(long usuarioId)
        {
            var result = await _context.Contrato
                .Include(c => c.UsuarioContrato)
                .Include(c => c.MotoContrato)
                .Where(c => c.UsuarioContratoId == usuarioId)
                .OrderBy(c => c.IdContrato)
                .ToListAsync();

            return new PageResultModel<IEnumerable<ContratoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterPorMotoAsync(long motoId)
        {
            var result = await _context.Contrato
                .Include(c => c.UsuarioContrato)
                .Include(c => c.MotoContrato)
                .Where(c => c.MotoContrato.IdMoto == motoId)
                .OrderBy(c => c.IdContrato)
                .ToListAsync();

            return new PageResultModel<IEnumerable<ContratoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterContratosNaoExpiradosAsync(DateTime dataAtual)
        {
            var result = await _context.Contrato
                .Include(c => c.UsuarioContrato)
                .Include(c => c.MotoContrato)
                .Where(c => c.DataDeExpiracaoContrato > dataAtual)
                .OrderBy(c => c.IdContrato)
                .ToListAsync();

            return new PageResultModel<IEnumerable<ContratoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterPorRenovacaoAutomaticaAsync(int renovacaoAutomatica)
        {
            var result = await _context.Contrato
                .Include(c => c.UsuarioContrato)
                .Include(c => c.MotoContrato)
                .Where(c => c.RenovacaoAutomaticaContrato == renovacaoAutomatica)
                .OrderBy(c => c.IdContrato)
                .ToListAsync();

            return new PageResultModel<IEnumerable<ContratoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<ContratoEntity>>> ObterPorDataEntradaEntreAsync(DateTime dataInicio, DateTime dataFim)
        {
            var result = await _context.Contrato
                .Include(c => c.UsuarioContrato)
                .Include(c => c.MotoContrato)
                .Where(c => c.DataDeEntradaContrato >= dataInicio && c.DataDeEntradaContrato <= dataFim)
                .OrderBy(c => c.IdContrato)
                .ToListAsync();

            return new PageResultModel<IEnumerable<ContratoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }
    }
}