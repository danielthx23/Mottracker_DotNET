using Microsoft.EntityFrameworkCore;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using Mottracker.Infrastructure.AppData;

namespace Mottracker.Infrastructure.Data.Repositories
{   
    public class ContratoRepository : IContratoRepository
        {
            private readonly ApplicationContext _context;
    
            public ContratoRepository(ApplicationContext context)
            {
                _context = context;
            }
            
            public IEnumerable<ContratoEntity> ObterTodos()
            {
                var Contratos = _context.Contrato
                    .Include(c => c.UsuarioContrato)
                    .Include(c => c.MotoContrato)
                    .ToList();
    
                return Contratos;
            }
    
            public ContratoEntity? ObterPorId(int id)
            {
                var Contratos = _context.Contrato
                    .Include(c => c.UsuarioContrato)
                    .Include(c => c.MotoContrato)
                    .FirstOrDefault(c => c.IdContrato == id);;
    
                return Contratos;
            }
            
            public List<ContratoEntity> ObterPorIds(List<int> ids)
            {
                return _context.Contrato
                    .Where(c => ids.Contains(c.IdContrato))
                    .ToList();
            }
    
            public ContratoEntity? Salvar(ContratoEntity entity)
            {
                _context.Contrato.Add(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public ContratoEntity? Atualizar(ContratoEntity entity)
            {
                _context.Contrato.Update(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public ContratoEntity? Deletar(int id)
            {
                var entity = _context.Contrato.Find(id);
    
                if (entity is not null)
                {
                    _context.Contrato.Remove(entity);
                    _context.SaveChanges();
    
                    return entity;
                }
    
                return null;
            }
    
        }
}
