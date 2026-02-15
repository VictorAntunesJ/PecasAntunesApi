using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Net.Sockets;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml;
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
        var ultimoCodigo = await _repository.ObterUltimoCodigoInternoAsync();
        var codigoInterno = GerarCodigoInterno(ultimoCodigo);

        var peca = new AutoPeca(
            dto.Codigo,
            dto.Nome,
            dto.Marca,
            dto.Preco,
            dto.QuantidadeEstoque,
            dto.Descricao
        );

        peca.DefinirCodigoInterno(codigoInterno);

        await _repository.AddAsync(peca);

        return new AutoPecaResponseDto
        {
            Id = peca.Id,
            CodigoInterno = peca.CodigoInterno,
            Codigo = peca.Codigo,
            Nome = peca.Nome,
            Marca = peca.Marca,
            Preco = peca.Preco,
            QuantidadeEstoque = peca.QuantidadeEstoque,
            Descricao = peca.Descricao
        };
    }
    public async Task<IEnumerable<AutoPecaResponseDto>> ListarTodasAsync()
    {
        var pecas = await _repository.GetAllAsync();

        return pecas.Select(p => new AutoPecaResponseDto
        {
            Id = p.Id,
            CodigoInterno = p.CodigoInterno,
            Nome = p.Nome,
            Codigo = p.Codigo,
            Marca = p.Marca,
            Preco = p.Preco,
            QuantidadeEstoque = p.QuantidadeEstoque,
            Descricao = p.Descricao
        });

    }

   public async Task<AutoPecaResponseDto> BuscarPorIdAsync(int id)
{
    var peca = await _repository.GetByIdAsync(id);

    if (peca == null)
        throw new KeyNotFoundException("Peça não encontrada");

    return new AutoPecaResponseDto
    {
        Id = peca.Id,
        CodigoInterno = peca.CodigoInterno,
        Nome = peca.Nome,
        Codigo = peca.Codigo,
        Marca = peca.Marca,
        Preco = peca.Preco,
        QuantidadeEstoque = peca.QuantidadeEstoque,
        Descricao = peca.Descricao
    };
}

    public async Task AtualizarAsync(int id, AutoPecaUpdateDto dto)
    {
        var peca = await _repository.GetByIdAsync(id);

        if (peca == null)
            throw new Exception("Peça não encontrada");

        peca.AtualizarDados(
            dto.Nome,
            dto.Codigo,
            dto.Marca,
            dto.Preco,
            dto.QuantidadeEstoque,
            dto.Descricao
        );

        await _repository.UpdateAsync(peca);
    }
    public async Task DeletarAsync(int id)
    {
        var sucesso = await _repository.DeleteAsync(id);

        if (!sucesso)
            throw new Exception("Peça não encontrada");
    }

    private string GerarCodigoInterno(string? ultimoCodigo)
    {
        int numero = 0;
        int volta = 0;

        if (!string.IsNullOrEmpty(ultimoCodigo))
        {
            var partes = ultimoCodigo.Split(',');
            numero = int.Parse(partes[0]);
            volta = int.Parse(partes[1]);
        }

        if (numero < 9999)
        {
            numero++;
        }
        else
        {
            numero = 0;
            volta++;
        }

        return $"{numero:D4},{volta}";
    }

}