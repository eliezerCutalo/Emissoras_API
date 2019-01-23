using Core_MVC.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Core_MVC.Data

{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        #region DbSets

        public DbSet<Emissora> Emissora { get; set; }
        public DbSet<Audiencia> Audiencia { get; set; }
        #endregion
    }
}
