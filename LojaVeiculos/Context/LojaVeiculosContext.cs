using LojaVeiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVeiculos.Context
{
    public class LojaVeiculosContext: DbContext
    {

        public LojaVeiculosContext(DbContextOptions<LojaVeiculosContext> options): base(options)
        {
        }

        public DbSet<Veiculo> Veiculo { get; set; }
        public DbSet<Venda> Venda { get; set; }
        public DbSet<ItemVenda> ItemVenda { get; set; }

    }
}
