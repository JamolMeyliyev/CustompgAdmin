
using CustompgAdmin.DataAccess.Entities;

using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CustompgAdmin.DataAccess.Context;


public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Column> Columns { get; set; }

    public virtual DbSet<ConnectionEntity> Connections { get; set; }

    public virtual DbSet<DatabaseEntity> Databases { get; set; }

    public virtual DbSet<QueryEntity> QueryStories { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=postgres;Database=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Column>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("columns_pkey");

            entity.HasOne(d => d.Table).WithMany(p => p.Columns)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("columns_table_id_fkey");
        });

        modelBuilder.Entity<ConnectionEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("connections_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<DatabaseEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("databases_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<QueryEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("query_stories_pkey");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tables_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Database).WithMany(p => p.Tables)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("tables_database_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
