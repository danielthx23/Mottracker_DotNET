using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.Data.AppData;
using Mottracker.Application.Models;

namespace Mottracker.Infrastructure.Data.Repositories
{
    public class UsuarioPermissaoRepository : IUsuarioPermissaoRepository
    {
        private readonly ApplicationContext _context;

        public UsuarioPermissaoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PageResultModel<IEnumerable<UsuarioPermissaoEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var totalRegistros = await _context.UsuarioPermissao.CountAsync();

            var result = await _context.UsuarioPermissao
                .Include(up => up.UsuarioPermissaoUsuario)
                .Include(up => up.UsuarioPermissaoPermissao)
                .OrderBy(up => up.UsuarioPermissaoUsuarioId)
                .Skip(Deslocamento)
                .Take(RegistrosRetornado)
                .ToListAsync();

            return new PageResultModel<IEnumerable<UsuarioPermissaoEntity>>
            {
                Data = result,
                Deslocamento = Deslocamento,
                RegistrosRetornado = RegistrosRetornado,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<UsuarioPermissaoEntity?> ObterPorIdAsync(int usuarioId, int permissaoId)
        {
            return await _context.UsuarioPermissao
                .Include(up => up.UsuarioPermissaoUsuario)
                .Include(up => up.UsuarioPermissaoPermissao)
                .FirstOrDefaultAsync(up => up.UsuarioPermissaoUsuarioId == usuarioId && up.UsuarioPermissaoPermissaoId == permissaoId);
        }

        public async Task<UsuarioPermissaoEntity?> SalvarAsync(UsuarioPermissaoEntity entity)
        {
            _context.UsuarioPermissao.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<UsuarioPermissaoEntity?> AtualizarAsync(UsuarioPermissaoEntity entity)
        {
            _context.UsuarioPermissao.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<UsuarioPermissaoEntity?> DeletarAsync(int usuarioId, int permissaoId)
        {
            var entity = await _context.UsuarioPermissao
                .FirstOrDefaultAsync(up => up.UsuarioPermissaoUsuarioId == usuarioId && up.UsuarioPermissaoPermissaoId == permissaoId);
            
            if (entity is not null)
            {
                _context.UsuarioPermissao.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }

            return null;
        }
    }
}