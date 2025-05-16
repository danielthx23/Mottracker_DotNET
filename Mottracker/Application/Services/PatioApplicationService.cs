using Mottracker.Application.Dtos.Moto;
using Mottracker.Application.Dtos.Patio;
using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Mottracker.Application.Dtos.Camera;
using Mottracker.Application.Dtos.Endereco;
using Mottracker.Application.Dtos.LayoutPatio;

namespace Mottracker.Application.Services
{
    public class PatioApplicationService : IPatioApplicationService
    {
        private readonly IPatioRepository _repository;
        private readonly IMotoRepository _motoRepository;
        private readonly ICameraRepository _cameraRepository;

        public PatioApplicationService(IPatioRepository repository,
            IMotoRepository motoRepository,
            ICameraRepository cameraRepository)
        {
            _repository = repository;
            _motoRepository = motoRepository;
            _cameraRepository = cameraRepository;
        }

        public PatioResponseDto? ObterPatioPorId(int id)
        {
            var patio = _repository.ObterPorId(id);
            if (patio == null) return null;

            return MapToResponseDto(patio);
        }

        public IEnumerable<PatioResponseDto> ObterTodosPatios()
        {
            var patios = _repository.ObterTodos();
            return patios.Select(MapToResponseDto);
        }

        public PatioResponseDto? SalvarDadosPatio(PatioRequestDto entity)
        {
            var patioEntity = new PatioEntity
            {
                NomePatio = entity.NomePatio,
                MotosTotaisPatio = entity.MotosTotaisPatio,
                MotosDisponiveisPatio = entity.MotosDisponiveisPatio,
                DataPatio = entity.DataPatio,
            };

            var salvo = _repository.Salvar(patioEntity);
            if (salvo == null) return null;

            return MapToResponseDto(salvo);
        }

        public PatioResponseDto? EditarDadosPatio(int id, PatioRequestDto entity)
        {
            var patioExistente = _repository.ObterPorId(id);
            if (patioExistente == null) return null;

            patioExistente.NomePatio = entity.NomePatio;
            patioExistente.MotosTotaisPatio = entity.MotosTotaisPatio;
            patioExistente.MotosDisponiveisPatio = entity.MotosDisponiveisPatio;
            patioExistente.DataPatio = entity.DataPatio;

            var atualizado = _repository.Atualizar(patioExistente);
            if (atualizado == null) return null;

            return MapToResponseDto(atualizado);
        }

        public PatioResponseDto? DeletarDadosPatio(int id)
        {
            var deletado = _repository.Deletar(id);
            return deletado == null ? null : MapToResponseDto(deletado);
        }
        
        private PatioResponseDto MapToResponseDto(PatioEntity patio)
{
    return new PatioResponseDto
    {
        IdPatio = patio.IdPatio,
        NomePatio = patio.NomePatio,
        MotosTotaisPatio = patio.MotosTotaisPatio,
        MotosDisponiveisPatio = patio.MotosDisponiveisPatio,
        DataPatio = patio.DataPatio,

        MotosPatioAtual = patio.MotosPatioAtual?
            .Select(m => new MotoDto
            {
                IdMoto = m.IdMoto,
                PlacaMoto = m.PlacaMoto,
                ModeloMoto = m.ModeloMoto,
                AnoMoto = m.AnoMoto,
                IdentificadorMoto = m.IdentificadorMoto,
                QuilometragemMoto = m.QuilometragemMoto,
                EstadoMoto = m.EstadoMoto,
                CondicoesMoto = m.CondicoesMoto,
            }).ToList() ?? new List<MotoDto>(),

        CamerasPatio = patio.CamerasPatio?
            .Select(c => new CameraDto
            {
                IdCamera = c.IdCamera,
                NomeCamera = c.NomeCamera,
                PosY = c.PosY,
                PosX = c.PosX,
                IpCamera = c.IpCamera
            }).ToList() ?? new List<CameraDto>(),

        LayoutPatio = patio.LayoutPatio != null ? new LayoutPatioDto
        {
            IdLayoutPatio = patio.LayoutPatio.IdLayoutPatio,
            Descricao = patio.LayoutPatio.Descricao,
            DataCriacao = patio.LayoutPatio.DataCriacao,
            Largura = patio.LayoutPatio.Largura,
            Comprimento = patio.LayoutPatio.Comprimento,
            Altura = patio.LayoutPatio.Altura
        } : null,

        EnderecoPatio = patio.EnderecoPatio != null ? new EnderecoDto
        {
            IdEndereco = patio.EnderecoPatio.IdEndereco,
            Logradouro = patio.EnderecoPatio.Logradouro,
            Numero = patio.EnderecoPatio.Numero,
            Complemento = patio.EnderecoPatio.Complemento,
            Bairro = patio.EnderecoPatio.Bairro,
            Cidade = patio.EnderecoPatio.Cidade,
            Estado = patio.EnderecoPatio.Estado,
            CEP = patio.EnderecoPatio.CEP,
            Referencia = patio.EnderecoPatio.Referencia
        } : null
    };
}

    }
}
