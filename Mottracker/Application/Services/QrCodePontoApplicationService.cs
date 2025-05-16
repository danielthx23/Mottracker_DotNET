using Mottracker.Application.Dtos.QrCodePonto;
using Mottracker.Application.Dtos.LayoutPatio;
using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Mottracker.Application.Services
{
    public class QrCodePontoApplicationService : IQrCodePontoApplicationService
    {
        private readonly IQrCodePontoRepository _repository;

        public QrCodePontoApplicationService(IQrCodePontoRepository repository)
        {
            _repository = repository;
        }

        public QrCodePontoResponseDto? ObterQrCodePontoPorId(int id)
        {
            var qrCode = _repository.ObterPorId(id);
            if (qrCode == null) return null;

            return MapToResponseDto(qrCode);
        }

        public IEnumerable<QrCodePontoResponseDto> ObterTodosQrCodePontos()
        {
            var qrCodes = _repository.ObterTodos();
            return qrCodes.Select(MapToResponseDto);
        }

        public QrCodePontoResponseDto? SalvarDadosQrCodePonto(QrCodePontoRequestDto entity)
        {
            var qrCodeEntity = new QrCodePontoEntity
            {
                IdentificadorQrCode = entity.IdentificadorQrCode,
                PosX = entity.PosX,
                PosY = entity.PosY,
                LayoutPatioId = entity.LayoutPatioId
            };

            var salvo = _repository.Salvar(qrCodeEntity);
            return salvo == null ? null : MapToResponseDto(salvo);
        }

        public QrCodePontoResponseDto? EditarDadosQrCodePonto(int id, QrCodePontoRequestDto entity)
        {
            var existente = _repository.ObterPorId(id);
            if (existente == null) return null;

            existente.IdentificadorQrCode = entity.IdentificadorQrCode;
            existente.PosX = entity.PosX;
            existente.PosY = entity.PosY;
            existente.LayoutPatioId = entity.LayoutPatioId;

            var atualizado = _repository.Atualizar(existente);
            return atualizado == null ? null : MapToResponseDto(atualizado);
        }

        public QrCodePontoResponseDto? DeletarDadosQrCodePonto(int id)
        {
            var deletado = _repository.Deletar(id);
            return deletado == null ? null : MapToResponseDto(deletado);
        }

        private QrCodePontoResponseDto MapToResponseDto(QrCodePontoEntity qr)
        {
            return new QrCodePontoResponseDto
            {
                IdQrCodePonto = qr.IdQrCodePonto,
                IdentificadorQrCode = qr.IdentificadorQrCode,
                PosX = qr.PosX,
                PosY = qr.PosY,
                LayoutPatio = qr.LayoutPatio != null ? new LayoutPatioDto
                {
                    IdLayoutPatio = qr.LayoutPatio.IdLayoutPatio,
                    Descricao = qr.LayoutPatio.Descricao,
                    DataCriacao = qr.LayoutPatio.DataCriacao,
                    Largura = qr.LayoutPatio.Largura,
                    Comprimento = qr.LayoutPatio.Comprimento,
                    Altura = qr.LayoutPatio.Altura
                } : null
            };
        }
    }
}
