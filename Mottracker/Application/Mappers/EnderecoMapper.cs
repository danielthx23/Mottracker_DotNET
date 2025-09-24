using Mottracker.Application.Dtos.Endereco;
using Mottracker.Domain.Entities;

namespace Mottracker.Application.Mappers
{
    public static class EnderecoMapper
    {
        public static EnderecoEntity ToEnderecoEntity(this EnderecoRequestDto obj)
        {
            return new EnderecoEntity
            {
                IdEndereco = obj.IdEndereco,
                Logradouro = obj.Logradouro,
                Numero = obj.Numero,
                Complemento = obj.Complemento,
                Bairro = obj.Bairro,
                Cidade = obj.Cidade,
                Estado = obj.Estado,
                CEP = obj.CEP,
                Referencia = obj.Referencia,
                PatioEnderecoId = obj.PatioId
            };
        }

        public static EnderecoDto ToEnderecoDto(this EnderecoEntity obj)
        {
            return new EnderecoDto
            {
                IdEndereco = obj.IdEndereco,
                Logradouro = obj.Logradouro,
                Numero = obj.Numero,
                Complemento = obj.Complemento,
                Bairro = obj.Bairro,
                Cidade = obj.Cidade,
                Estado = obj.Estado,
                CEP = obj.CEP,
                Referencia = obj.Referencia
            };
        }

        public static EnderecoResponseDto ToEnderecoResponseDto(this EnderecoEntity obj)
        {
            return new EnderecoResponseDto
            {
                IdEndereco = obj.IdEndereco,
                Logradouro = obj.Logradouro,
                Numero = obj.Numero,
                Complemento = obj.Complemento,
                Bairro = obj.Bairro,
                Cidade = obj.Cidade,
                Estado = obj.Estado,
                CEP = obj.CEP,
                Referencia = obj.Referencia,
                PatioEndereco = obj.PatioEndereco?.ToPatioDto()
            };
        }
    }
}
