using Microsoft.EntityFrameworkCore;
namespace GestionTurnosAfiliados.Web.Data
{
    public class ApplicationDbContext : DbContext
    {


        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }
    }
}