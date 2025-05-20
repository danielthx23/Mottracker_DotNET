using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

namespace Mottracker.Infrastructure.Data.Repositories
{   
    public class PatioRepository : IPatioRepository
        {
            private readonly ApplicationContext _context;
    
            public PatioRepository(ApplicationContext context)
            {
                _context = context;
            }
            
            public IEnumerable<PatioEntity> ObterTodos()
            {
                return _context.Patio
                    .Include(p => p.LayoutPatio)
                    .Include(p => p.EnderecoPatio)
                    .Include(p => p.MotosPatioAtual)
                    .Include(p => p.CamerasPatio)
                    .ToList();
            }

            public PatioEntity? ObterPorId(int id)
            {
                return _context.Patio
                    .Include(p => p.LayoutPatio)
                    .Include(p => p.EnderecoPatio)
                    .Include(p => p.MotosPatioAtual)
                    .Include(p => p.CamerasPatio)
                    .FirstOrDefault(p => p.IdPatio == id);
            }
    
            public PatioEntity? Salvar(PatioEntity entity)
            {
                _context.Patio.Add(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public PatioEntity? Atualizar(PatioEntity entity)
            {
                _context.Patio.Update(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public PatioEntity? Deletar(int id)
            {
                var entity = _context.Patio.Find(id);
    
                if (entity is not null)
                {
                    _context.Patio.Remove(entity);
                    _context.SaveChanges();
    
                    return entity;
                }
    
                return null;
            }

            public IEnumerable<PatioEntity> ObterPorNomeContendo(string nomePatio)
            {
                return _context.Patio
                    .Include(p => p.LayoutPatio)
                    .Include(p => p.EnderecoPatio)
                    .Include(p => p.MotosPatioAtual)
                    .Include(p => p.CamerasPatio)
                    .Where(p => EF.Functions.Like(p.NomePatio.ToLower(), $"%{nomePatio.ToLower()}%"))
                    .ToList();
            }

            public IEnumerable<PatioEntity> ObterComMotosDisponiveisAcimaDe(int quantidade)
            {
                return _context.Patio
                    .Include(p => p.LayoutPatio)
                    .Include(p => p.EnderecoPatio)
                    .Include(p => p.MotosPatioAtual)
                    .Include(p => p.CamerasPatio)
                    .Where(p => p.MotosPatioAtual.Count > quantidade)
                    .ToList();
            }

            public IEnumerable<PatioEntity> ObterPorDataPosterior(DateTime data)
            {
                return _context.Patio
                    .Include(p => p.LayoutPatio)
                    .Include(p => p.EnderecoPatio)
                    .Include(p => p.MotosPatioAtual)
                    .Include(p => p.CamerasPatio)
                    .Where(p => p.DataPatio > data)
                    .ToList();
            }

            public IEnumerable<PatioEntity> ObterPorDataAnterior(DateTime data)
            {
                return _context.Patio
                    .Include(p => p.LayoutPatio)
                    .Include(p => p.EnderecoPatio)
                    .Include(p => p.MotosPatioAtual)
                    .Include(p => p.CamerasPatio)
                    .Where(p => p.DataPatio < data)
                    .ToList();
            }
        }
}
