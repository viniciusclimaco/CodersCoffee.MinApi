using Microsoft.EntityFrameworkCore;

namespace CodersCoffee.MinApi
{

    public class PedidoDbContext : DbContext
    {
        public PedidoDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Pedido> Pedidos => Set<Pedido>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>().HasData(
                new Pedido
                {
                    Id = 1,
                    Descricao = "Café Expresso",                    
                    Created = DateTime.Now                    
                },
                new Pedido
                {
                    Id = 2,
                    Descricao = "Cappuccino",                    
                    Created = DateTime.Now                    
                }
            );
            
        }
    }
}
