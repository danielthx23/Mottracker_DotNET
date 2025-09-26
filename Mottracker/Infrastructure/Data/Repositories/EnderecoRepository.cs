using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

namespace Mottracker.Infrastructure.Data.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly ApplicationContext _context;

        public EnderecoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PageResultModel<IEnumerable<EnderecoEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var totalRegistros = await _context.Endereco.CountAsync();

            var result = await _context.Endereco
                .Include(e => e.PatioEndereco)
                .OrderBy(e => e.IdEndereco)
                .Skip(Deslocamento)
                .Take(RegistrosRetornado)
                .ToListAsync();

            return new PageResultModel<IEnumerable<EnderecoEntity>>
            {
                Data = result,
                Deslocamento = Deslocamento,
                RegistrosRetornado = RegistrosRetornado,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<EnderecoEntity?> ObterPorIdAsync(int id)
        {
            return await _context.Endereco
                .Include(e => e.PatioEndereco)
                .FirstOrDefaultAsync(e => e.IdEndereco == id);
        }

        public async Task<EnderecoEntity?> SalvarAsync(EnderecoEntity entity)
        {
            _context.Endereco.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EnderecoEntity?> AtualizarAsync(EnderecoEntity entity)
        {
            _context.Endereco.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EnderecoEntity?> DeletarAsync(int id)
        {
            var entity = await _context.Endereco.FindAsync(id);
            if (entity is not null)
            {
                _context.Endereco.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }

            return null;
        }

        // Métodos de consulta específicos (sem paginação)
        public async Task<PageResultModel<IEnumerable<EnderecoEntity>>> ObterTodasAsync()
        {
            var result = await _context.Endereco
                .Include(e => e.PatioEndereco)
                .OrderBy(e => e.IdEndereco)
                .ToListAsync();

            return new PageResultModel<IEnumerable<EnderecoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<EnderecoEntity>>> ObterPorCepAsync(string cep)
        {
            var result = await _context.Endereco
                .Include(e => e.PatioEndereco)
                .Where(e => e.CEP.Contains(cep))
                .OrderBy(e => e.IdEndereco)
                .ToListAsync();

            return new PageResultModel<IEnumerable<EnderecoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<EnderecoEntity>>> ObterPorEstadoAsync(string estado)
        {
            var result = await _context.Endereco
                .Include(e => e.PatioEndereco)
                .Where(e => e.Estado == estado)
                .OrderBy(e => e.IdEndereco)
                .ToListAsync();

            return new PageResultModel<IEnumerable<EnderecoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<EnderecoEntity>>> ObterPorCidadeAsync(string cidade)
        {
            var result = await _context.Endereco
                .Include(e => e.PatioEndereco)
                .Where(e => e.Cidade.Contains(cidade))
                .OrderBy(e => e.IdEndereco)
                .ToListAsync();

            return new PageResultModel<IEnumerable<EnderecoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<EnderecoEntity>>> ObterPorCidadeEstadoAsync(string cidade, string estado)
        {
            var result = await _context.Endereco
                .Include(e => e.PatioEndereco)
                .Where(e => e.Cidade.Contains(cidade) && e.Estado == estado)
                .OrderBy(e => e.IdEndereco)
                .ToListAsync();

            return new PageResultModel<IEnumerable<EnderecoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<EnderecoEntity>>> ObterPorBairroAsync(string bairro)
        {
            var result = await _context.Endereco
                .Include(e => e.PatioEndereco)
                .Where(e => e.Bairro.Contains(bairro))
                .OrderBy(e => e.IdEndereco)
                .ToListAsync();

            return new PageResultModel<IEnumerable<EnderecoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<EnderecoEntity>>> ObterPorLogradouroContendoAsync(string logradouro)
        {
            var result = await _context.Endereco
                .Include(e => e.PatioEndereco)
                .Where(e => e.Logradouro.Contains(logradouro))
                .OrderBy(e => e.IdEndereco)
                .ToListAsync();

            return new PageResultModel<IEnumerable<EnderecoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<EnderecoEntity>>> ObterPorPatioIdAsync(int patioId)
        {
            var result = await _context.Endereco
                .Include(e => e.PatioEndereco)
                .Where(e => e.PatioEnderecoId == patioId)
                .OrderBy(e => e.IdEndereco)
                .ToListAsync();

            return new PageResultModel<IEnumerable<EnderecoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }
    }
}