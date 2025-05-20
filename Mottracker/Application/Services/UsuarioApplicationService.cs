using Mottracker.Application.Dtos;
using Mottracker.Application.Dtos.Usuario;
using Mottracker.Application.Dtos.Telefone;
using Mottracker.Application.Dtos.UsuarioPermissao;
using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;

namespace Mottracker.Application.Services
{
    public class UsuarioApplicationService : IUsuarioApplicationService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioApplicationService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public UsuarioResponseDto? ObterUsuarioPorId(int id)
        {
            var usuario = _repository.ObterPorId(id);
            if (usuario == null) return null;

            return MapToResponseDto(usuario);
        }

        public UsuarioResponseDto? ObterUsuarioPorEmail(string emailUsuario)
        {
            var usuario = _repository.ObterPorEmail(emailUsuario);
            if (usuario == null) return null;

            return MapToResponseDto(usuario);
        }

        public IEnumerable<UsuarioResponseDto> ObterTodosUsuarios()
        {
            var usuarios = _repository.ObterTodos();
            return usuarios.Select(MapToResponseDto);
        }

        public UsuarioResponseDto? SalvarDadosUsuario(UsuarioRequestDto entity)
        {
            var usuarioEntity = new UsuarioEntity
            {
                NomeUsuario = entity.NomeUsuario,
                CPFUsuario = entity.CPFUsuario,
                SenhaUsuario = entity.SenhaUsuario,
                CNHUsuario = entity.CNHUsuario,
                EmailUsuario = entity.EmailUsuario,
                TokenUsuario = entity.TokenUsuario,
                DataNascimentoUsuario = entity.DataNascimentoUsuario,
                CriadoEmUsuario = DateTime.UtcNow,
            };

            var salvo = _repository.Salvar(usuarioEntity);
            return salvo == null ? null : MapToResponseDto(salvo);
        }

        public UsuarioResponseDto? EditarDadosUsuario(int id, UsuarioRequestDto entity)
        {
            var usuarioExistente = _repository.ObterPorId(id);
            if (usuarioExistente == null) return null;

            usuarioExistente.NomeUsuario = entity.NomeUsuario;
            usuarioExistente.CPFUsuario = entity.CPFUsuario;
            usuarioExistente.SenhaUsuario = entity.SenhaUsuario;
            usuarioExistente.CNHUsuario = entity.CNHUsuario;
            usuarioExistente.EmailUsuario = entity.EmailUsuario;
            usuarioExistente.TokenUsuario = entity.TokenUsuario;
            usuarioExistente.DataNascimentoUsuario = entity.DataNascimentoUsuario;
            usuarioExistente.CriadoEmUsuario = entity.CriadoEmUsuario;

            var atualizado = _repository.Atualizar(usuarioExistente);
            return atualizado == null ? null : MapToResponseDto(atualizado);
        }

        public UsuarioResponseDto? DeletarDadosUsuario(int id)
        {
            var deletado = _repository.Deletar(id);
            return deletado == null ? null : MapToResponseDto(deletado);
        }

        private UsuarioResponseDto MapToResponseDto(UsuarioEntity usuario)
        {
            return new UsuarioResponseDto
            {
                IdUsuario = usuario.IdUsuario,
                NomeUsuario = usuario.NomeUsuario,
                CPFUsuario = usuario.CPFUsuario,
                SenhaUsuario = usuario.SenhaUsuario,
                CNHUsuario = usuario.CNHUsuario,
                EmailUsuario = usuario.EmailUsuario,
                TokenUsuario = usuario.TokenUsuario,
                DataNascimentoUsuario = usuario.DataNascimentoUsuario,
                CriadoEmUsuario = usuario.CriadoEmUsuario,

                ContratoUsuario = usuario.ContratoUsuario != null ? new ContratoDto
                {
                    IdContrato = usuario.ContratoUsuario.IdContrato,
                    ClausulasContrato = usuario.ContratoUsuario.ClausulasContrato,
                    DataDeEntradaContrato = usuario.ContratoUsuario.DataDeEntradaContrato,
                    HorarioDeDevolucaoContrato = usuario.ContratoUsuario.HorarioDeDevolucaoContrato,
                    DataDeExpiracaoContrato = usuario.ContratoUsuario.DataDeExpiracaoContrato,
                    RenovacaoAutomaticaContrato = usuario.ContratoUsuario.RenovacaoAutomaticaContrato,
                    DataUltimaRenovacaoContrato = usuario.ContratoUsuario.DataUltimaRenovacaoContrato,
                    NumeroRenovacoesContrato = usuario.ContratoUsuario.NumeroRenovacoesContrato,
                    AtivoContrato = usuario.ContratoUsuario.AtivoContrato,
                    ValorToralContrato = usuario.ContratoUsuario.ValorToralContrato,
                    QuantidadeParcelas = usuario.ContratoUsuario.QuantidadeParcelas
                } : null,

                Telefones = usuario.Telefones?.Select(t => new TelefoneDto
                {
                    IdTelefone = t.IdTelefone,
                    Numero = t.Numero,
                    Tipo = t.Tipo
                }).ToList() ?? new List<TelefoneDto>(),

                UsuarioPermissoes = usuario.UsuarioPermissoes?.Select(p => new UsuarioPermissaoDto
                {
                    IdUsuario = p.UsuarioId,
                    IdPermissao = p.PermissaoId,
                    PapelUsuarioPermissao = p.Papel
                }).ToList() ?? new List<UsuarioPermissaoDto>()
            };
        }
    }
}
