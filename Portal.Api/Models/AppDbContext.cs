using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Portal.Api.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UcHarfKelime> UcHarfKelimes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UcHarfKelime>(entity =>
        {
            entity.HasKey(e => e.UcHarfKelimeId).HasName("uc_harf_kelime_pkey");

            entity.ToTable("uc_harf_kelime");

            entity.Property(e => e.UcHarfKelimeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("uc_harf_kelime_id");
            entity.Property(e => e.Tanim).HasColumnName("tanim");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
