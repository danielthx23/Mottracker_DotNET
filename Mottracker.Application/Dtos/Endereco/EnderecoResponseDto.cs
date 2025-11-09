using Mottracker.Application.Dtos.Patio;

namespace Mottracker.Application.Dtos.Endereco;

public class EnderecoResponseDto : EnderecoDto
{
    public PatioDto? PatioEndereco { get; set; } = new();
}