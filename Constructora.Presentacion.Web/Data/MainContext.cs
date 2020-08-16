using Constructora.Presentacion.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Constructora.Presentacion.Web.Data
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<File> File { get; set; }
        public DbSet<Token> Token { get; set; }
    }
}
