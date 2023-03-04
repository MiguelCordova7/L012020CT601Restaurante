using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
   
namespace L012020CT601.Models
{
    public class restauranteContext : DbContext
    {
            public restauranteContext(DbContextOptions<restauranteContext> options) : base(options)
            {

         
            
            }

            public DbSet<restauranteContext> restaurantes { get; set; }
    
    }
}
