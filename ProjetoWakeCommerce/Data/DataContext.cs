using Microsoft.EntityFrameworkCore;
using ProjetoWakeCommerce.Entidades;

namespace ProjetoWakeCommerce.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
    }
}
