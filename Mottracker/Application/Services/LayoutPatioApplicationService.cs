using Mottracker.Application.Dtos.LayoutPatio;
using Mottracker.Application.Dtos.Patio;
using Mottracker.Application.Dtos.QrCodePonto;
using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Mottracker.Application.Services
{
    public class LayoutPatioApplicationService : ILayoutPatioApplicationService
    {
        private readonly ILayoutPatioRepository _repository;

        public LayoutPatioApplicationService(ILayoutPatioRepository repository)
        {
            _repository = repository;
        }

        public LayoutPatioResponseDto? ObterLayoutPatioPorId(int id)
        {
            var layout = _repository.ObterPorId(id); 
            if (layout == null)
                return null;

            return MapToResponseDto(layout);
        }

        public IEnumerable<LayoutPatioResponseDto> ObterTodosLayoutsPatios()
        {
            var layouts = _repository.ObterTodos();
            return layouts.Select(MapToResponseDto);
        }

        public LayoutPatioResponseDto? SalvarDadosLayoutPatio(LayoutPatioRequestDto entity)
        {
            var layoutEntity = new LayoutPatioEntity
            {
                Descricao = entity.Descricao,
                DataCriacao = entity.DataCriacao,
                Largura = entity.Largura,
                Comprimento = entity.Comprimento,
                Altura = entity.Altura,
                PatioLayoutPatioId = entity.PatioLayoutPatioId,
            };

            var saved = _repository.Salvar(layoutEntity);
            if (saved == null)
                return null;
            
            return MapToResponseDto(saved);
        }

        public LayoutPatioResponseDto? EditarDadosLayoutPatio(int id, LayoutPatioRequestDto entity)
        {
            var existing = _repository.ObterPorId(id);
            if (existing == null)
                return null;

            existing.Descricao = entity.Descricao;
            existing.DataCriacao = entity.DataCriacao;
            existing.Largura = entity.Largura;
            existing.Comprimento = entity.Comprimento;
            existing.Altura = entity.Altura;
            existing.PatioLayoutPatioId = entity.PatioLayoutPatioId;

            var updated = _repository.Atualizar(existing);
            if (updated == null)
                return null;
            
            return MapToResponseDto(updated);
        }

        public LayoutPatioResponseDto? DeletarDadosLayoutPatio(int id)
        {
            var deleted = _repository.Deletar(id);
            if (deleted == null)
                return null;

            return MapToResponseDto(deleted);
        }

        private LayoutPatioResponseDto MapToResponseDto(LayoutPatioEntity entity)
        {
            return new LayoutPatioResponseDto
            {
                IdLayoutPatio = entity.IdLayoutPatio,
                Descricao = entity.Descricao,
                DataCriacao = entity.DataCriacao,
                Largura = entity.Largura,
                Comprimento = entity.Comprimento,
                Altura = entity.Altura,

                PatioLayoutPatio = entity.PatioLayoutPatio != null ? new PatioDto
                {
                    IdPatio = entity.PatioLayoutPatio.IdPatio,
                    NomePatio = entity.PatioLayoutPatio.NomePatio,
                    MotosTotaisPatio = entity.PatioLayoutPatio.MotosTotaisPatio,
                    MotosDisponiveisPatio = entity.PatioLayoutPatio.MotosDisponiveisPatio,
                    DataPatio = entity.PatioLayoutPatio.DataPatio
                } : null,

                QrCodesLayoutPatio = entity.QrCodesLayoutPatio?.Select(qr => new QrCodePontoDto
                {
                    IdQrCodePonto = qr.IdQrCodePonto,
                    IdentificadorQrCode = qr.IdentificadorQrCode,
                    PosX = qr.PosX,
                    PosY = qr.PosY
                }).ToList() ?? new List<QrCodePontoDto>()
            };
        }
    }
}
