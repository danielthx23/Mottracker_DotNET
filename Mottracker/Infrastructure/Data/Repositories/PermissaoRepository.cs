using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

namespace Mottracker.Infrastructure.Data.Repositories
{
    public class PermissaoRepository : IPermissaoRepository
    {
        private readonly ApplicationContext _context;

        public PermissaoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PageResultModel<IEnumerable<PermissaoEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var totalRegistros = await _context.Permissao.CountAsync();

            var result = await _context.Permissao
                .Include(p => p.UsuarioPermissoes)
                .OrderBy(p => p.IdPermissao)
                .Skip(Deslocamento)
                .Take(RegistrosRetornado)
                .ToListAsync();

            return new PageResultModel<IEnumerable<PermissaoEntity>>
            {
                Data = result,
                Deslocamento = Deslocamento,
                RegistrosRetornado = RegistrosRetornado,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<PermissaoEntity?> ObterPorIdAsync(int id)
        {
            return await _context.Permissao
                .Include(p => p.UsuarioPermissoes)
                .FirstOrDefaultAsync(p => p.IdPermissao == id);
        }

        public async Task<PermissaoEntity?> SalvarAsync(PermissaoEntity entity)
        {
            _context.Permissao.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<PermissaoEntity?> AtualizarAsync(PermissaoEntity entity)
        {
            _context.Permissao.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<PermissaoEntity?> DeletarAsync(int id)
        {
            var entity = await _context.Permissao.FindAsync(id);
            if (entity is not null)
            {
                _context.Permissao.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }

            return null;
        }

        // Métodos de consulta específicos (sem paginação)
        public async Task<PageResultModel<IEnumerable<PermissaoEntity>>> ObterTodasAsync()
        {
            var result = await _context.Permissao
                .Include(p => p.UsuarioPermissoes)
                .OrderBy(p => p.IdPermissao)
                .ToListAsync();

            return new PageResultModel<IEnumerable<PermissaoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<PermissaoEntity>>> ObterPorNomeAsync(string nomePermissao)
        {
            var result = await _context.Permissao
                .Include(p => p.UsuarioPermissoes)
                .Where(p => p.NomePermissao.Contains(nomePermissao))
                .OrderBy(p => p.IdPermissao)
                .ToListAsync();

            return new PageResultModel<IEnumerable<PermissaoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }

        public async Task<PageResultModel<IEnumerable<PermissaoEntity>>> ObterPorDescricaoAsync(string descricao)
        {
            var result = await _context.Permissao
                .Include(p => p.UsuarioPermissoes)
                .Where(p => p.Descricao.Contains(descricao))
                .OrderBy(p => p.IdPermissao)
                .ToListAsync();

            return new PageResultModel<IEnumerable<PermissaoEntity>>
            {
                Data = result,
                Deslocamento = 0,
                RegistrosRetornado = result.Count,
                TotalRegistros = result.Count
            };
        }
    }
}