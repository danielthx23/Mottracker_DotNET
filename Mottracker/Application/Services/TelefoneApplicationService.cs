using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;

namespace Mottracker.Application.Services
{   
    public class TelefoneApplicationService : ITelefoneApplicationService
    {
        private readonly ITelefoneRepository _repository;
      
        public TelefoneApplicationService(ITelefoneRepository repository)
        {
            _repository = repository;
        }
      
        public TelefoneEntity? ObterTelefonePorId(int id)
        {
            return _repository.ObterPorId(id);
        }
      
        public IEnumerable<TelefoneEntity> ObterTodosTelefones()
        {
            return _repository.ObterTodos();
        }
              
        public TelefoneEntity? SalvarDadosTelefone(TelefoneEntity entity)
        {
            return _repository.Salvar(entity);
        }
              
        public TelefoneEntity? EditarDadosTelefone(int id, TelefoneEntity entity)
        {
            var telefoneExistente = _repository.ObterPorId(id);

            if (telefoneExistente == null)
                return null;
            
            telefoneExistente.Numero = entity.Numero;
            telefoneExistente.Tipo = entity.Tipo;
            telefoneExistente.Usuario = entity.Usuario;

            return _repository.Atualizar(telefoneExistente);
        }

              
        public TelefoneEntity? DeletarDadosTelefone(int id)
        {
            return _repository.Deletar(id);
        }
    }
}
