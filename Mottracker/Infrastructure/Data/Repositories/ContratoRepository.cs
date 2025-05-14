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
                var Contrato = _context.Contrato.ToList();
    
                return Contrato;
            }
    
            public ContratoEntity? ObterPorId(int id)
            {
                var Contrato = _context.Contrato.Find(id);
    
                return Contrato;
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
