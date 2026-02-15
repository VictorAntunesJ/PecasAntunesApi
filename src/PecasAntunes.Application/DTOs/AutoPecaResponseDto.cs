using System.Security.Cryptography.X509Certificates;

namespace PecasAntunes.Application.DTOs;

public class AutoPecaResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int EstoqueAtual { get; set; }
    public int QuantidadeEstoque { get; set; }
    public string? Descricao { get; set; }
    public string CodigoInterno { get; set; } = string.Empty;
}