namespace PecasAntunes.Application.DTOs;

public class AutoPecaCreateDto
{
    /// <summary> Nome da autopeça</summary>
    public string Nome { get; set; } = string.Empty;
    /// <summary>Código externo da peça</summary>
    public string Codigo { get; set; } = string.Empty;
    /// <summary> Marca da autopeça</summary>
    public string Marca { get; set; } = string.Empty;
    /// <summary> Preço da autopeça </summary>    
    public decimal Preco { get; set; }
    /// <summary> Quantidade em estoque da autopeça </summary>
    public int QuantidadeEstoque { get; set; }
    /// <summary> Descrição da autopeça </summary>
    public string? Descricao { get; set; }
}
