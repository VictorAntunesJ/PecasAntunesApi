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
        // 1. Mapeamento usando o seu CONSTRUTOR (O jeito profissional que você definiu)
        var peca = new AutoPeca(
            dto.Codigo,
            dto.Nome,
            dto.Preco,
            dto.EstoqueAtual // No DTO está EstoqueAtual, no Domain é QuantidadeEstoque
        );

        // 2. Chama o repositório
        await _repository.AddAsync(peca);

        // 3. Mapeamento para o Response
        return new AutoPecaResponseDto
        {
            Id = peca.Id,
            Nome = peca.Nome,
            Codigo = peca.Codigo,
            Marca = dto.Marca, 
            Preco = peca.Preco,
            EstoqueAtual = peca.QuantidadeEstoque
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