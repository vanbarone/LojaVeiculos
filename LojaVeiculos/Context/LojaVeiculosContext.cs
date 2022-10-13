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
        
        public DbSet<ItemCompra> ItemCompra { get; set; }
        
        public DbSet<Marca> Marca { get; set; }
        
        public DbSet<Modelo> Modelo { get; set; }
        
        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        
        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Veiculo> Veiculo { get; set; }

        public DbSet<Compra> Compra { get; set; }
    }
}
