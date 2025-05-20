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
            return _context.Endereco
                .Include(e => e.PatioEndereco)
                .ToList();
        }

        public EnderecoEntity? ObterPorId(int id)
        {
            return _context.Endereco
                .Include(e => e.PatioEndereco)
                .FirstOrDefault(e => e.IdEndereco == id);
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

        public IEnumerable<EnderecoEntity> ObterPorCidade(string cidade)
        {
            return _context.Endereco
                .Include(e => e.PatioEndereco)
                .Where(e => e.Cidade!.ToLower() == cidade.ToLower())
                .ToList();
        }

        public IEnumerable<EnderecoEntity> ObterPorCidadeEEstado(string cidade, string estado)
        {
            return _context.Endereco
                .Include(e => e.PatioEndereco)
                .Where(e => e.Cidade!.ToLower() == cidade.ToLower() &&
                            e.Estado!.ToLower() == estado.ToLower())
                .ToList();
        }

        public IEnumerable<EnderecoEntity> ObterPorEstado(string estado)
        {
            return _context.Endereco
                .Include(e => e.PatioEndereco)
                .Where(e => e.Estado!.ToLower() == estado.ToLower())
                .ToList();
        }

        public EnderecoEntity? ObterPorCep(string cep)
        {
            return _context.Endereco
                .Include(e => e.PatioEndereco)
                .FirstOrDefault(e => e.CEP == cep);
        }

        public IEnumerable<EnderecoEntity> ObterPorBairro(string bairro)
        {
            return _context.Endereco
                .Include(e => e.PatioEndereco)
                .Where(e => e.Bairro!.ToLower() == bairro.ToLower())
                .ToList();
        }

        public IEnumerable<EnderecoEntity> ObterPorLogradouroContendo(string logradouro)
        {
            return _context.Endereco
                .Include(e => e.PatioEndereco)
                .Where(e => EF.Functions.Like(e.Logradouro!, $"%{logradouro}%"))
                .ToList();
        }

        public EnderecoEntity? ObterPorIdPatio(long patioId)
        {
            return _context.Endereco
                .Include(e => e.PatioEndereco)
                .FirstOrDefault(e => e.PatioEndereco!.IdPatio == patioId);
        }
    }
}
