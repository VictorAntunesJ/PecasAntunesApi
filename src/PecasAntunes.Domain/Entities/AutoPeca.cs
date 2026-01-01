namespace PecasAntunes.Domain.Entities;

public class AutoPeca
{
    public int Id { get; private set; }
    public string Codigo { get; private set; }
    public string Nome { get; private set; }
    public string? Descricao { get; private set; }
    public decimal Preco { get; private set; }
    public int QuantidadeEstoque { get; private set; }
    public DateTime CriadoEm { get; private set; }

    protected AutoPeca()
    {
        Codigo = string.Empty;
        Nome = string.Empty;
    }


    public AutoPeca(
        string codigo,
        string nome,
        decimal preco,
        int quantidadeEstoque,
        string? descricao = null)
    {
        Codigo = codigo;
        Nome = nome;
        Preco = preco;
        QuantidadeEstoque = quantidadeEstoque;
        Descricao = descricao;
        CriadoEm = DateTime.UtcNow;
    }

    public void AtualizarPreco(decimal novoPreco)
    {
        if (novoPreco <= 0)
            throw new ArgumentException("O preço deve ser maior que zero.");

        Preco = novoPreco;
    }

    public void AtualizarEstoque(int quantidade)
    {
        if (quantidade < 0)
            throw new ArgumentException("Quantidade inválida.");

        QuantidadeEstoque = quantidade;
    }
}
