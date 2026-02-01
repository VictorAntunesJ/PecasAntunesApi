namespace PecasAntunes.Application.DTOs;

public class AutoPecaUpdateDto
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public decimal Preco { get; set; }

    public int QuantidadeEstoque { get; set; }
    public string? Descricao { get; set; }
}

