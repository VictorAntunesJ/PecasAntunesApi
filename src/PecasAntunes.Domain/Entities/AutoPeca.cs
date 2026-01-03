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
        ValidarNome(nome);

        Codigo = codigo;
        Nome = nome;
        Preco = preco;
        QuantidadeEstoque = quantidadeEstoque;
        Descricao = descricao;
        CriadoEm = DateTime.UtcNow;
    }

    private void ValidarNome(string nome)
    {
       if (nome == null)
            throw new ArgumentNullException(nameof(nome), "O nome não pode ser nulo.");

        if (nome == string.Empty)
            throw new Exception("O nome não pode ser uma string vazia.");

        if (string.IsNullOrWhiteSpace(nome))
            throw new Exception("O nome não pode consistir apenas em espaços em branco.");

        if (nome.Length < 3)
            throw new Exception("O nome da peça deve ter no mínimo 3 caracteres.");

        if (nome.Length > 100)
            throw new Exception("O nome da peça deve ter no máximo 100 caracteres.");
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
