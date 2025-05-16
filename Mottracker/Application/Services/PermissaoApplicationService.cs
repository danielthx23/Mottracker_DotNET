using Mottracker.Application.Dtos.Permissao;
using Mottracker.Application.Dtos.UsuarioPermissao;
using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Mottracker.Application.Services
{
    public class PermissaoApplicationService : IPermissaoApplicationService
    {
        private readonly IPermissaoRepository _repository;

        public PermissaoApplicationService(IPermissaoRepository repository)
        {
            _repository = repository;
        }

        public PermissaoResponseDto? ObterPermissaoPorId(int id)
        {
            var permissao = _repository.ObterPorId(id);
            if (permissao == null) return null;

            return MapToResponseDto(permissao);
        }

        public IEnumerable<PermissaoResponseDto> ObterTodosPermissoes()
        {
            var permissoes = _repository.ObterTodos();
            return permissoes.Select(MapToResponseDto);
        }

        public PermissaoResponseDto? SalvarDadosPermissao(PermissaoRequestDto entity)
        {
            var permissaoEntity = new PermissaoEntity
            {
                NomePermissao = entity.NomePermissao,
                Descricao = entity.Descricao,
            };

            var salvo = _repository.Salvar(permissaoEntity);
            if (salvo == null) return null;

            return MapToResponseDto(salvo);
        }

        public PermissaoResponseDto? EditarDadosPermissao(int id, PermissaoRequestDto entity)
        {
            var permissaoExistente = _repository.ObterPorId(id);
            if (permissaoExistente == null) return null;

            permissaoExistente.NomePermissao = entity.NomePermissao;
            permissaoExistente.Descricao = entity.Descricao;

            var atualizado = _repository.Atualizar(permissaoExistente);
            if (atualizado == null) return null;

            return MapToResponseDto(atualizado);
        }

        public PermissaoResponseDto? DeletarDadosPermissao(int id)
        {
            var deletado = _repository.Deletar(id);
            return deletado == null ? null : MapToResponseDto(deletado);
        }

        private PermissaoResponseDto MapToResponseDto(PermissaoEntity permissao)
        {
            return new PermissaoResponseDto
            {
                IdPermissao = permissao.IdPermissao,
                NomePermissao = permissao.NomePermissao,
                Descricao = permissao.Descricao,
                UsuarioPermissoes = permissao.UsuarioPermissoes?
                    .Select(up => new UsuarioPermissaoDto
                    {
                        IdPermissao = up.PermissaoId,
                        IdUsuario = up.UsuarioId,
                        PapelUsuarioPermissao = up.Papel
                    }).ToList() ?? new List<UsuarioPermissaoDto>()
            };
        }
    }
}
