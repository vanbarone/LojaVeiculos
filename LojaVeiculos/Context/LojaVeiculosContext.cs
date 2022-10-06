using LojaVeiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVeiculos.Context
{
    public class LojaVeiculosContext: DbContext
    {

        public LojaVeiculosContext(DbContextOptions<LojaVeiculosContext> options): base(options)
        {
        }

        public DbSet<Cartao> Cartao { get; set; }
        
        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<Concessionaria> Concessionaria { get; set; }
        
        public DbSet<ItemVenda> ItemVenda { get; set; }
        
        public DbSet<Marca> Marca { get; set; }
        
        public DbSet<Modelo> Modelo { get; set; }
        
        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        
        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Veiculo> Veiculo { get; set; }

        public DbSet<Venda> Venda { get; set; }
    }
}
