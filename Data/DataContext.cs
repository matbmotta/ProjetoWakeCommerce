using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Identity.Client;
using ProjetoWakeCommerce.Entidades;
using WakeCommerce.Utils.Enums;

namespace WakeCommerce.Database
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }

        public DataContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
            Seed();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().ToTable("Produtos");
        }

        public void Seed()
        {
            if (!Produtos.Any())
            {
                var produtos = new List<Produto>()
        {   
            new Produto
            {
                Nome = "Camiseta",
                Estoque = 10,
                Tipo = TipoEnum.Vestuario,
                Valor = 35.99m,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now
            },
            new Produto
            {
                Nome = "Bermuda",
                Estoque = 15,
                Tipo = TipoEnum.Vestuario,
                Valor = 55.99m,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now
            },
            new Produto
            {
                Nome = "Celular",
                Estoque = 10,
                Tipo = TipoEnum.Eletronico,
                Valor = 1099.99m,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now
            },
            new Produto
            {
                Nome = "Geladeira",
                Estoque = 15,
                Tipo = TipoEnum.Eletrodomesticos,
                Valor = 2599.99m,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now
            },
            new Produto
            {
                Nome = "Toalha",
                Estoque = 30,
                Tipo = TipoEnum.CamaMesaBanho,
                Valor = 25.99m,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now
            },
        };

                Produtos.AddRange(produtos);
                SaveChanges();
            }
        }

        public DbSet<Produto> Produtos { get; set; }
    }
}
