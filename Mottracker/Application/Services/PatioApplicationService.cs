using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;

namespace Mottracker.Application.Services
{   
    public class PatioApplicationService : IPatioApplicationService
    {
        private readonly IPatioRepository _repository;
      
        public PatioApplicationService(IPatioRepository repository)
        {
            _repository = repository;
        }
      
        public PatioEntity? ObterPatioPorId(int id)
        {
            return _repository.ObterPorId(id);
        }
      
        public IEnumerable<PatioEntity> ObterTodosPatios() 
        {
            return _repository.ObterTodos();
        }
              
        public PatioEntity? SalvarDadosPatio(PatioEntity entity)
        {
            return _repository.Salvar(entity);
        }
              
        public PatioEntity? EditarDadosPatio(int id, PatioEntity entity)
        {
            var patioExistente = _repository.ObterPorId(id);
      
            if (patioExistente == null)
                return null;
                  
            patioExistente.NomePatio = entity.NomePatio;
            patioExistente.MotosTotaisPatio = entity.MotosTotaisPatio;
            patioExistente.MotosDisponiveisPatio = entity.MotosDisponiveisPatio;
            patioExistente.DataPatio = entity.DataPatio;
            patioExistente.MotosPatio = entity.MotosPatio;
            patioExistente.LayoutPatio = entity.LayoutPatio;
            patioExistente.EnderecoPatio = entity.EnderecoPatio;
            
            return _repository.Atualizar(patioExistente);
        }
              
        public PatioEntity? DeletarDadosPatio(int id)
        {
            return _repository.Deletar(id);
        }
    }
}
