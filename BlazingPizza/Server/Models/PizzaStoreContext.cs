using Microsoft.EntityFrameworkCore;
using BlazingPizza.Shared;

namespace BlazingPizza.Server.Models
{
    public class PizzaStoreContext : DbContext
    {
        public DbSet<PizzaSpecial> Specials { get; set; }
        public PizzaStoreContext(DbContextOptions options)
        : base(options)
        {
        }

    }
}
