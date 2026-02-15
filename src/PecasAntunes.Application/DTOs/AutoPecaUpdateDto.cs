namespace PecasAntunes.Application.DTOs;

public class AutoPecaUpdateDto
{
    public required string Codigo { get; set; }
    public required string Nome { get; set; }
    public required string Marca { get; set; }

    public decimal Preco { get; set; }
    public int QuantidadeEstoque { get; set; }
    public string? Descricao { get; set; }
}


