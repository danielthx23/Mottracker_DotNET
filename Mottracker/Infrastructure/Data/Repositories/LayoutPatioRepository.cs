using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

namespace Mottracker.Infrastructure.Data.Repositories
{   
    public class LayoutPatioRepository : ILayoutPatioRepository
        {
            private readonly ApplicationContext _context;
    
            public LayoutPatioRepository(ApplicationContext context)
            {
                _context = context;
            }
            
            public IEnumerable<LayoutPatioEntity> ObterTodos()
            {
                var LayoutPatio = _context.LayoutPatio
                    .Include(lp => lp.PatioLayoutPatio)
                    .Include(lp => lp.QrCodesLayoutPatio)
                    .ToList();
    
                return LayoutPatio;
            }
    
            public LayoutPatioEntity? ObterPorId(int id)
            {
                var LayoutPatio = _context.LayoutPatio
                    .Include(lp => lp.PatioLayoutPatio)
                    .Include(lp => lp.QrCodesLayoutPatio)
                    .FirstOrDefault(lp => lp.IdLayoutPatio == id);
    
                return LayoutPatio;
            }
            
            public List<LayoutPatioEntity> ObterPorIds(List<int> ids)
            {
                return _context.LayoutPatio
                    .Where(lp => ids.Contains(lp.IdLayoutPatio))
                    .ToList();
            }
    
            public LayoutPatioEntity? Salvar(LayoutPatioEntity entity)
            {
                _context.LayoutPatio.Add(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public LayoutPatioEntity? Atualizar(LayoutPatioEntity entity)
            {
                _context.LayoutPatio.Update(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public LayoutPatioEntity? Deletar(int id)
            {
                var entity = _context.LayoutPatio.Find(id);
    
                if (entity is not null)
                {
                    _context.LayoutPatio.Remove(entity);
                    _context.SaveChanges();
    
                    return entity;
                }
    
                return null;
            }

            public IEnumerable<LayoutPatioEntity> ObterPorIdPatio(long patioId)
            {
                return _context.LayoutPatio
                    .Include(lp => lp.PatioLayoutPatio)
                    .Include(lp => lp.QrCodesLayoutPatio)
                    .Where(lp => lp.PatioLayoutPatio!.IdPatio == patioId)
                    .ToList();
            }

            public IEnumerable<LayoutPatioEntity> ObterPorDataCriacaoEntre(DateTime dataInicio, DateTime dataFim)
            {
                return _context.LayoutPatio
                    .Include(lp => lp.PatioLayoutPatio)
                    .Include(lp => lp.QrCodesLayoutPatio)
                    .Where(lp => lp.DataCriacao >= dataInicio && lp.DataCriacao <= dataFim)
                    .ToList();
            }

        }
}
