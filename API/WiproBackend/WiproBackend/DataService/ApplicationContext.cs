using Microsoft.EntityFrameworkCore;
using WiproBackend.Models;

namespace WiproBackend.DataService
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
         : base(options)
        { }
        public DbSet<Cotacao> Cotacao { get; set; }
    }
}
