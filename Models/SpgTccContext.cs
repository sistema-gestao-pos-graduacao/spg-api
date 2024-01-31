using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace spg_api.Models;

public partial class SpgTccContext : DbContext
{
    public SpgTccContext()
    {
    }

    public SpgTccContext(DbContextOptions<SpgTccContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Person> People { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=tcp:spg-tcc.database.windows.net,1433;Initial Catalog=spg-tcc;Persist Security Info=False;User ID=adm;Password=31@Set2024;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("person_pk");

            entity.ToTable("person", "spg");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
