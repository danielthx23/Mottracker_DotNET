using Mottracker.Application.Dtos.Telefone;
using Mottracker.Application.Dtos.Usuario;
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

        public TelefoneResponseDto? ObterTelefonePorId(int id)
        {
            var telefone = _repository.ObterPorId(id);
            if (telefone == null) return null;

            return MapToResponseDto(telefone);
        }

        public IEnumerable<TelefoneResponseDto> ObterTelefonesPorNumero(string numero)
        {
            var telefones = _repository.ObterPorNumero(numero);
            return telefones.Select(MapToResponseDto);
        }

        public IEnumerable<TelefoneResponseDto> ObterTelefonesPorUsuarioId(long usuarioId)
        {
            var telefones = _repository.ObterPorIdUsuario(usuarioId);
            return telefones.Select(MapToResponseDto);
        }

        public IEnumerable<TelefoneResponseDto> ObterTelefonesPorTipo(string tipo)
        {
            var telefones = _repository.ObterPorTipo(tipo);
            return telefones.Select(MapToResponseDto);
        }

        public IEnumerable<TelefoneResponseDto> ObterTodosTelefones()
        {
            var telefones = _repository.ObterTodos();
            return telefones.Select(MapToResponseDto);
        }

        public TelefoneResponseDto? SalvarDadosTelefone(TelefoneRequestDto entity)
        {
            var telefoneEntity = new TelefoneEntity
            {
                Numero = entity.Numero,
                Tipo = entity.Tipo,
                UsuarioId = entity.UsuarioId
            };

            var salvo = _repository.Salvar(telefoneEntity);
            return salvo == null ? null : MapToResponseDto(salvo);
        }

        public TelefoneResponseDto? EditarDadosTelefone(int id, TelefoneRequestDto entity)
        {
            var telefoneExistente = _repository.ObterPorId(id);
            if (telefoneExistente == null) return null;

            telefoneExistente.Numero = entity.Numero;
            telefoneExistente.Tipo = entity.Tipo;
            telefoneExistente.UsuarioId = entity.UsuarioId;

            var atualizado = _repository.Atualizar(telefoneExistente);
            return atualizado == null ? null : MapToResponseDto(atualizado);
        }

        public TelefoneResponseDto? DeletarDadosTelefone(int id)
        {
            var deletado = _repository.Deletar(id);
            return deletado == null ? null : MapToResponseDto(deletado);
        }

        private TelefoneResponseDto MapToResponseDto(TelefoneEntity telefone)
        {
            return new TelefoneResponseDto
            {
                IdTelefone = telefone.IdTelefone,
                Numero = telefone.Numero,
                Tipo = telefone.Tipo,
                Usuario = telefone.Usuario != null ? new UsuarioDto
                {
                    IdUsuario = telefone.Usuario.IdUsuario,
                    NomeUsuario = telefone.Usuario.NomeUsuario,
                    CPFUsuario = telefone.Usuario.CPFUsuario,
                    SenhaUsuario = telefone.Usuario.SenhaUsuario,
                    CNHUsuario = telefone.Usuario.CNHUsuario,
                    EmailUsuario = telefone.Usuario.EmailUsuario,
                    TokenUsuario = telefone.Usuario.TokenUsuario,
                    DataNascimentoUsuario = telefone.Usuario.DataNascimentoUsuario,
                    CriadoEmUsuario = telefone.Usuario.CriadoEmUsuario
                } : null
            };
        }
    }
}
