using Mottracker.Domain.Entities;

namespace Mottracker.Domain.Interfaces
{   
        public interface IPatioRepository
        {
            IEnumerable<PatioEntity> ObterTodos();
            PatioEntity? ObterPorId(int id);
            PatioEntity? Salvar(PatioEntity entity);
            PatioEntity? Atualizar(PatioEntity entity);
            PatioEntity? Deletar(int id);
            IEnumerable<PatioEntity> ObterPorNomeContendo(string nomePatio);
            IEnumerable<PatioEntity> ObterComMotosDisponiveisAcimaDe(int quantidade);
            IEnumerable<PatioEntity> ObterPorDataPosterior(DateTime data);
            IEnumerable<PatioEntity> ObterPorDataAnterior(DateTime data);
        }
}
