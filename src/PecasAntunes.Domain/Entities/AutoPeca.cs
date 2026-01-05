namespace PecasAntunes.Domain.Entities;

public class AutoPeca
{
    public int Id { get; private set; }

    public string Codigo { get; private set; } = string.Empty;
    public string Nome { get; private set; } = string.Empty;
    public string Marca { get; private set; } = string.Empty;

    public decimal Preco { get; private set; }
    public int QuantidadeEstoque { get; private set; }

    public string? Descricao { get; private set; }
    public DateTime CriadoEm { get; private set; }

    protected AutoPeca() { }

    public AutoPeca(
    string codigo,
    string nome,
    string marca,
    decimal preco,
    int quantidadeEstoque,
    string? descricao = null)
{
    ValidarCodigo(codigo);
    ValidarNome(nome);
    ValidarMarca(marca);
    ValidarPreco(preco);
    ValidarEstoque(quantidadeEstoque);
    ValidarDescricao(descricao);

    Codigo = codigo;
    Nome = nome;
    Marca = marca;
    Preco = preco;
    QuantidadeEstoque = quantidadeEstoque;
    Descricao = descricao;
    CriadoEm = DateTime.UtcNow;
}

    private void ValidarDescricao(string? descricao)
    {
        if (descricao != null && descricao.Length > 200)
            throw new ArgumentException("A descrição deve ter no máximo 200 caracteres.");
    }

private void ValidarEstoque(int quantidadeEstoque)
    {
        if (quantidadeEstoque < 0)
            throw new ArgumentException("A quantidade em estoque não pode ser negativa.");
    }

    private void ValidarMarca(string marca)
    {
        if (string.IsNullOrWhiteSpace(marca))
            throw new ArgumentException("A marca é obrigatória.");
         
        if (marca.Length < 2)
            throw new ArgumentException("A marca deve ter no mínimo 2 caracteres.");
        
        if (marca.Length > 100)
            throw new ArgumentException("A marca deve ter no máximo 100 caracteres.");
    }

    private void ValidarPreco(decimal preco)
    {
        if (preco <= 0)
            throw new ArgumentException("O preço deve ser maior que zero.");

        if (preco < 0)
            throw new ArgumentException("O preço da peça não pode ser negativo.", nameof(preco));
    }


    private void ValidarCodigo(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo) || codigo.Length < 3)
            throw new ArgumentException("Código inválido.");
    }

    private void ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome) || nome.Length < 3)
            throw new ArgumentException("Nome inválido.");
    }

    public void AtualizarEstoque(int quantidade)
    {
        if (quantidade < 0)
            throw new ArgumentException("Quantidade inválida.");

        QuantidadeEstoque = quantidade;
    }

}
