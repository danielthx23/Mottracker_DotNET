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
            
            public IEnumerable<EnderecoEntity> ObterTodos()
            {
                var Endereco = _context.Endereco
                    .Include(e => e.PatioEndereco)
                    .ToList();
    
                return Endereco;
            }
    
            public EnderecoEntity? ObterPorId(int id)
            {
                var Endereco = _context.Endereco
                    .Include(e => e.PatioEndereco)
                    .FirstOrDefault(e => e.IdEndereco == id);
    
                return Endereco;
            }
            
            public List<EnderecoEntity> ObterPorIds(List<int> ids)
            {
                return _context.Endereco
                    .Where(e => ids.Contains(e.IdEndereco))
                    .ToList();
            }
    
            public EnderecoEntity? Salvar(EnderecoEntity entity)
            {
                _context.Endereco.Add(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public EnderecoEntity? Atualizar(EnderecoEntity entity)
            {
                _context.Endereco.Update(entity);
                _context.SaveChanges();
    
                return entity;
            }
            
            public EnderecoEntity? Deletar(int id)
            {
                var entity = _context.Endereco.Find(id);
    
                if (entity is not null)
                {
                    _context.Endereco.Remove(entity);
                    _context.SaveChanges();
    
                    return entity;
                }
    
                return null;
            }
    
        }
}
