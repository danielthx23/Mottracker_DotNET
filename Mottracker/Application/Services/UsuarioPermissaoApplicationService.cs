using Mottracker.Application.Dtos.Usuario;
using Mottracker.Application.Dtos.Permissao;
using Mottracker.Application.Dtos.UsuarioPermissao;
using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;

namespace Mottracker.Application.Services
{
    public class UsuarioPermissaoApplicationService : IUsuarioPermissaoApplicationService
    {
        private readonly IUsuarioPermissaoRepository _repository;

        public UsuarioPermissaoApplicationService(IUsuarioPermissaoRepository repository)
        {
            _repository = repository;
        }

        public UsuarioPermissaoResponseDto? ObterUsuarioPermissaoPorId(int usuarioId, int permissaoId)
        {
            var entity = _repository.ObterPorId(usuarioId, permissaoId);
            return entity == null ? null : MapToResponseDto(entity);
        }

        public IEnumerable<UsuarioPermissaoResponseDto> ObterUsuarioPermissoesPorUsuarioId(long usuarioId)
        {
            var entidades = _repository.ObterPorIdUsuario(usuarioId);
            return entidades.Select(MapToResponseDto);
        }

        public IEnumerable<UsuarioPermissaoResponseDto> ObterUsuarioPermissoesPorPermissaoId(long permissaoId)
        {
            var entidades = _repository.ObterPorIdPermissao(permissaoId);
            return entidades.Select(MapToResponseDto);
        }

        public IEnumerable<UsuarioPermissaoResponseDto> ObterTodosUsuarioPermissoes()
        {
            return _repository.ObterTodos().Select(MapToResponseDto);
        }

        public UsuarioPermissaoResponseDto? SalvarDadosUsuarioPermissao(UsuarioPermissaoRequestDto dto)
        {
            var entity = new UsuarioPermissaoEntity
            {
                Papel = dto.PapelUsuarioPermissao,
                UsuarioId = dto.IdUsuario,
                PermissaoId = dto.IdPermissao
            };

            var salvo = _repository.Salvar(entity);
            return salvo == null ? null : MapToResponseDto(salvo);
        }

        public UsuarioPermissaoResponseDto? EditarDadosUsuarioPermissao(int usuarioId, int permissaoId, UsuarioPermissaoRequestDto dto)
        {
            var existente = _repository.ObterPorId(usuarioId, permissaoId);
            if (existente == null) return null;

            existente.Papel = dto.PapelUsuarioPermissao;
            existente.UsuarioId = dto.IdUsuario;
            existente.PermissaoId = dto.IdPermissao;

            var atualizado = _repository.Atualizar(existente);
            return atualizado == null ? null : MapToResponseDto(atualizado);
        }

        public UsuarioPermissaoResponseDto? DeletarDadosUsuarioPermissao(int usuarioId, int permissaoId)
        {
            var deletado = _repository.Deletar(usuarioId, permissaoId);
            return deletado == null ? null : MapToResponseDto(deletado);
        }

        private UsuarioPermissaoResponseDto MapToResponseDto(UsuarioPermissaoEntity entity)
        {
            return new UsuarioPermissaoResponseDto
            {
                IdUsuario = entity.UsuarioId,
                IdPermissao = entity.PermissaoId,
                PapelUsuarioPermissao = entity.Papel,

                Usuario = entity.Usuario != null
                    ? new UsuarioDto
                    {
                        IdUsuario = entity.Usuario.IdUsuario,
                        NomeUsuario = entity.Usuario.NomeUsuario,
                        CPFUsuario = entity.Usuario.CPFUsuario,
                        SenhaUsuario = entity.Usuario.SenhaUsuario,
                        CNHUsuario = entity.Usuario.CNHUsuario,
                        EmailUsuario = entity.Usuario.EmailUsuario,
                        TokenUsuario = entity.Usuario.TokenUsuario,
                        DataNascimentoUsuario = entity.Usuario.DataNascimentoUsuario,
                        CriadoEmUsuario = entity.Usuario.CriadoEmUsuario
                    }
                    : null,

                Permissao = entity.Permissao != null
                    ? new PermissaoDto
                    {
                        IdPermissao = entity.Permissao.IdPermissao,
                        NomePermissao = entity.Permissao.NomePermissao,
                        Descricao = entity.Permissao.Descricao
                    }
                    : null
            };
        }
    }
}
