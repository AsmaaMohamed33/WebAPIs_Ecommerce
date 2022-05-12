using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication8.Model
{
    public class ITIEntities:IdentityDbContext<ApplicationUSer>
    {
        public ITIEntities()
        {

        }
        public ITIEntities(DbContextOptions options):base(options)
        {

        }
       
        public DbSet<Product> Products { get; set; }
      


    }
}
