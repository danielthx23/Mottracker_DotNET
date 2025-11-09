using Mottracker.Application.Dtos.Telefone;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Mappers
{
    public static class TelefoneMapper
    {
        public static TelefoneEntity ToTelefoneEntity(this TelefoneRequestDto obj)
        {
            return new TelefoneEntity
            {
                IdTelefone = obj.IdTelefone,
                Numero = obj.Numero,
                Tipo = obj.Tipo,
                UsuarioId = obj.UsuarioId
            };
        }

        public static TelefoneDto ToTelefoneDto(this TelefoneEntity obj)
        {
            return new TelefoneDto
            {
                IdTelefone = obj.IdTelefone,
                Numero = obj.Numero,
                Tipo = obj.Tipo
            };
        }

        public static TelefoneResponseDto ToTelefoneResponseDto(this TelefoneEntity obj)
        {
            return new TelefoneResponseDto
            {
                IdTelefone = obj.IdTelefone,
                Numero = obj.Numero,
                Tipo = obj.Tipo,
                Usuario = obj.Usuario?.ToUsuarioDto()
            };
        }
    }
}
