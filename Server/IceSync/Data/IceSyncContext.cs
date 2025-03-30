namespace IceSync.Data;

using Entities;

using Microsoft.EntityFrameworkCore;

public class IceSyncContext : DbContext
{
    public DbSet<Workflow> Workflows { get; set; }

    public IceSyncContext(DbContextOptions<IceSyncContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=IceSync;TrustServerCertificate=True;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Workflow>()
               .Property(et => et.WorkflowId)
               .ValueGeneratedNever();

        builder.Entity<Workflow>()
               .Property(et => et.WorkflowName)
               .HasMaxLength(300);

        builder.Entity<Workflow>()
               .Property(et => et.MultiExecBehavior)
               .HasMaxLength(50);
    }
}
