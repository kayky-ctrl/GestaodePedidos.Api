using Microsoft.EntityFrameworkCore;
using ShopGestProjeto.Api.Models;

namespace ShopGestProjeto.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes{ get; set; }

        public DbSet<Pedido> Pedidos { get; set; }
    }
}
