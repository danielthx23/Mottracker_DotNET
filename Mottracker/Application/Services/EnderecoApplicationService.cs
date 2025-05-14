using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;

namespace Mottracker.Application.Services
{   
    public class EnderecoApplicationService : IEnderecoApplicationService
    {
        private readonly IEnderecoRepository _repository;
      
        public EnderecoApplicationService(IEnderecoRepository repository)
        {
            _repository = repository;
        }
      
        public EnderecoEntity? ObterEnderecoPorId(int id)
        {
            return _repository.ObterPorId(id);
        }
      
        public IEnumerable<EnderecoEntity> ObterTodosEnderecos()
        {
            return _repository.ObterTodos();
        }
              
        public EnderecoEntity? SalvarDadosEndereco(EnderecoEntity entity)
        {
            return _repository.Salvar(entity);
        }
              
        public EnderecoEntity? EditarDadosEndereco(int id, EnderecoEntity entity)
        {
            var enderecoExistente = _repository.ObterPorId(id);
      
            if (enderecoExistente == null)
                return null;
                  
            enderecoExistente.Logradouro = entity.Logradouro;
            enderecoExistente.Numero = entity.Numero;
            enderecoExistente.Complemento = entity.Complemento;
            enderecoExistente.Bairro = entity.Bairro;
            enderecoExistente.Cidade = entity.Cidade;
            enderecoExistente.Estado = entity.Estado;
            enderecoExistente.CEP = entity.CEP;
            enderecoExistente.Referencia = entity.Referencia;
            enderecoExistente.PatioEndereco = entity.PatioEndereco;
      
            return _repository.Atualizar(enderecoExistente);
        }
              
        public EnderecoEntity? DeletarDadosEndereco(int id)
        {
            return _repository.Deletar(id);
        }
    }
}
