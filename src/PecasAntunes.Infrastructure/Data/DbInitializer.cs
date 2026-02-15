using PecasAntunes.Domain.Entities;

namespace PecasAntunes.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.AutoPecas.Any())
                return;

            var pecas = new List<AutoPeca>
            {
                new AutoPeca(
                    codigo: "FO-001",
                    nome: "Filtro de Óleo",
                    marca: "Bosch",
                    preco: 29.90m,
                    quantidadeEstoque: 10,
                    descricao: "Filtro de óleo para motores 1.0 a 2.0"
                ),

                new AutoPeca(
                    codigo: "PF-002",
                    nome: "Pastilha de Freio",
                    marca: "Cobreq",
                    preco: 120.00m,
                    quantidadeEstoque: 5,
                    descricao: "Pastilha de freio dianteira"
                ),

                new AutoPeca(
                    codigo: "VI-003",
                    nome: "Vela de Ignição",
                    marca: "NGK",
                    preco: 45.50m,
                    quantidadeEstoque: 20,
                    descricao: "Vela de ignição para motores flex"
                )
            };

            context.AutoPecas.AddRange(pecas);
            context.SaveChanges();
        }
    }
}
