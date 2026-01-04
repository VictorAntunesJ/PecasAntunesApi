using PecasAntunes.Application.DTOs;
using PecasAntunes.Application.Interfaces;
using PecasAntunes.Domain.Entities;

namespace PecasAntunes.Application.Services;

public class AutoPecaService : IAutoPecaService
{
    private readonly IAutoPecaRepository _repository;

    public AutoPecaService(IAutoPecaRepository repository)
    {
        _repository = repository;
    }

    public async Task<AutoPecaResponseDto> CriarAsync(AutoPecaCreateDto dto)
    {
        var peca = new AutoPeca(
            dto.Codigo,
            dto.Nome,
            dto.Marca,
            dto.Preco,
            dto.QuantidadeEstoque,
            dto.Descricao
        );
        await _repository.AddAsync(peca);

        return new AutoPecaResponseDto
        {
            Id = peca.Id,
            Nome = peca.Nome,
            Codigo = peca.Codigo,
            Marca = peca.Marca,
            Preco = peca.Preco,
            EstoqueAtual = peca.QuantidadeEstoque,
            EmEstoque = peca.QuantidadeEstoque > 0
        };
    }
    public async Task<IEnumerable<AutoPecaResponseDto>> ListarTodasAsync()
    {
        var pecas = await _repository.GetAllAsync();

        return pecas.Select(p => new AutoPecaResponseDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Codigo = p.Codigo,
            Preco = p.Preco,
            EstoqueAtual = p.QuantidadeEstoque
        });
    }
}