using Manager.Domain.Entities;
using Manager.Infra.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Manager.Infra.Context
{
    public class ManagerContext : DbContext
    {
        
        public ManagerContext(DbContextOptions<ManagerContext> options) : base(options)
        {
            
        }
        
        
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ManagerContext).Assembly);
        }
        
    }
}