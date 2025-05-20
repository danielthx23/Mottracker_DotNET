using Mottracker.Application.Dtos.Endereco;
using Mottracker.Application.Dtos.Patio;
using Mottracker.Application.Interfaces;
using Mottracker.Domain.Entities;
using Mottracker.Domain.Interfaces;

public class EnderecoApplicationService : IEnderecoApplicationService
{
    private readonly IEnderecoRepository _repository;

    public EnderecoApplicationService(IEnderecoRepository repository)
    {
        _repository = repository;
    }

    public EnderecoResponseDto? ObterEnderecoPorId(int id)
    {
        var endereco = _repository.ObterPorId(id);
        if (endereco == null)
            return null;

        return MapToResponseDto(endereco);
    }

    public IEnumerable<EnderecoResponseDto> ObterTodosEnderecos()
    {
        var enderecos = _repository.ObterTodos();
        return enderecos.Select(MapToResponseDto);
    }

    public EnderecoResponseDto? SalvarDadosEndereco(EnderecoRequestDto entity)
    {
        var enderecoEntity = new EnderecoEntity
        {
            Logradouro = entity.Logradouro,
            Numero = entity.Numero,
            Complemento = entity.Complemento,
            Bairro = entity.Bairro,
            Cidade = entity.Cidade,
            Estado = entity.Estado,
            CEP = entity.CEP,
            Referencia = entity.Referencia,
            PatioEnderecoId = entity.PatioEnderecoId
        };

        var saved = _repository.Salvar(enderecoEntity);
        if (saved == null)
            return null;

        return MapToResponseDto(saved);
    }

    public EnderecoResponseDto? EditarDadosEndereco(int id, EnderecoRequestDto entity)
    {
        var enderecoExistente = _repository.ObterPorId(id);
        if (enderecoExistente == null)
            return null;

        enderecoExistente.Logradouro = entity.Logradouro;
        enderecoExistente.Numero = entity.Numero;
        enderecoExistente.Complemento = entity.Complemento;
        enderecoExistente.Bairro = entity.Bairro;
        enderecoExistente.Cidade = entity.Cidade;
        enderecoExistente.Estado = entity.Estado;
        enderecoExistente.CEP = entity.CEP;
        enderecoExistente.Referencia = entity.Referencia;
        enderecoExistente.PatioEnderecoId = entity.PatioEnderecoId;

        var updated = _repository.Atualizar(enderecoExistente);
        if (updated == null)
            return null;

        return MapToResponseDto(updated);
    }

    public EnderecoResponseDto? DeletarDadosEndereco(int id)
    {
        var deleted = _repository.Deletar(id);
        if (deleted == null)
            return null;

        return MapToResponseDto(deleted);
    }

    // ? Novas funcionalidades

    public EnderecoResponseDto? ObterEnderecoPorCep(string cep)
    {
        var endereco = _repository.ObterPorCep(cep);
        return endereco is not null ? MapToResponseDto(endereco) : null;
    }

    public IEnumerable<EnderecoResponseDto> ObterEnderecosPorEstado(string estado)
    {
        var enderecos = _repository.ObterPorEstado(estado);
        return enderecos.Select(MapToResponseDto);
    }

    public IEnumerable<EnderecoResponseDto> ObterEnderecosPorCidade(string cidade)
    {
        var enderecos = _repository.ObterPorCidade(cidade);
        return enderecos.Select(MapToResponseDto);
    }

    public IEnumerable<EnderecoResponseDto> ObterEnderecosPorCidadeEEstado(string cidade, string estado)
    {
        var enderecos = _repository.ObterPorCidadeEEstado(cidade, estado);
        return enderecos.Select(MapToResponseDto);
    }

    public IEnumerable<EnderecoResponseDto> ObterEnderecosPorBairro(string bairro)
    {
        var enderecos = _repository.ObterPorBairro(bairro);
        return enderecos.Select(MapToResponseDto);
    }

    public IEnumerable<EnderecoResponseDto> ObterEnderecosPorLogradouroContendo(string logradouro)
    {
        var enderecos = _repository.ObterPorLogradouroContendo(logradouro);
        return enderecos.Select(MapToResponseDto);
    }

    public EnderecoResponseDto? ObterEnderecoPorIdPatio(long patioId)
    {
        var endereco = _repository.ObterPorIdPatio(patioId);
        return endereco is not null ? MapToResponseDto(endereco) : null;
    }
    private EnderecoResponseDto MapToResponseDto(EnderecoEntity endereco)
    {
        return new EnderecoResponseDto
        {
            IdEndereco = endereco.IdEndereco,
            Logradouro = endereco.Logradouro,
            Numero = endereco.Numero,
            Complemento = endereco.Complemento,
            Bairro = endereco.Bairro,
            Cidade = endereco.Cidade,
            Estado = endereco.Estado,
            CEP = endereco.CEP,
            Referencia = endereco.Referencia,

            PatioEndereco = endereco.PatioEndereco != null ? new PatioDto
            {
                IdPatio = endereco.PatioEndereco.IdPatio,
                NomePatio = endereco.PatioEndereco.NomePatio,
                MotosTotaisPatio = endereco.PatioEndereco.MotosTotaisPatio,
                MotosDisponiveisPatio = endereco.PatioEndereco.MotosDisponiveisPatio,
                DataPatio = endereco.PatioEndereco.DataPatio
            } : null
        };
    }
}
