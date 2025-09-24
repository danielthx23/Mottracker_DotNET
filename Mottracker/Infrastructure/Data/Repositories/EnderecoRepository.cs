using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.Data.AppData;
using Mottracker.Application.Models;

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
    }
}