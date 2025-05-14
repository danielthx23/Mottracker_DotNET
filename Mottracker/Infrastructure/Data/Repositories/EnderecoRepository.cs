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
                var Endereco = _context.Endereco.ToList();
    
                return Endereco;
            }
    
            public EnderecoEntity? ObterPorId(int id)
            {
                var Endereco = _context.Endereco.Find(id);
    
                return Endereco;
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
