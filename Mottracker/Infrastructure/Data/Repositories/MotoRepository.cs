using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Enums;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

namespace Mottracker.Infrastructure.Data.Repositories
{   
    public class MotoRepository : IMotoRepository
        {
            private readonly ApplicationContext _context;
    
            public MotoRepository(ApplicationContext context)
            {
                _context = context;
            }
            
            public IEnumerable<MotoEntity> ObterTodos()
            {
                var Moto = _context.Moto
                    .Include(m => m.MotoPatioAtual)
                    .Include(m => m.ContratoMoto)
                    .ToList();
    
                return Moto;
            }
    
            public MotoEntity? ObterPorId(int id)
            {
                var Moto = _context.Moto
                    .Include(m => m.MotoPatioAtual)
                    .Include(m => m.ContratoMoto)
                    .FirstOrDefault(m => m.IdMoto == id);;
    
                return Moto;
            }
            
            public List<MotoEntity> ObterPorIds(List<int> ids)
            {
                return _context.Moto
                    .Where(m => ids.Contains(m.IdMoto))
                    .ToList();
            }
    
            public MotoEntity? Salvar(MotoEntity entity)
            {
                _context.Moto.Add(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public MotoEntity? Atualizar(MotoEntity entity)
            {
                _context.Moto.Update(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public MotoEntity? Deletar(int id)
            {
                var entity = _context.Moto.Find(id);
    
                if (entity is not null)
                {
                    _context.Moto.Remove(entity);
                    _context.SaveChanges();
    
                    return entity;
                }
    
                return null;
            }

            public MotoEntity? ObterPorPlaca(string placaMoto)
            {
                return _context.Moto
                    .Include(m => m.MotoPatioAtual)
                    .Include(m => m.ContratoMoto)
                    .FirstOrDefault(m => m.PlacaMoto.ToLower() == placaMoto.ToLower());
            }

            public IEnumerable<MotoEntity> ObterPorEstado(Estados estadoMoto)
            {
                return _context.Moto
                    .Include(m => m.MotoPatioAtual)
                    .Include(m => m.ContratoMoto)
                    .Where(m => m.EstadoMoto == estadoMoto)
                    .ToList();
            }

            public IEnumerable<MotoEntity> ObterPorIdContrato(long contratoId)
            {
                return _context.Moto
                    .Include(m => m.MotoPatioAtual)
                    .Include(m => m.ContratoMoto)
                    .Where(m => m.ContratoMoto != null && m.ContratoMoto.IdContrato == contratoId)
                    .ToList();
            }

        }
}
