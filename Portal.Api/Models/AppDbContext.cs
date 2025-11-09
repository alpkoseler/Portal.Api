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

    public virtual DbSet<Dil> Dils { get; set; }

    public virtual DbSet<Kategori> Kategoris { get; set; }

    public virtual DbSet<Kelime> Kelimes { get; set; }

    public virtual DbSet<KelimeKategori> KelimeKategoris { get; set; }

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
        modelBuilder.Entity<Dil>(entity =>
        {
            entity.HasKey(e => e.DilId).HasName("dil_pkey");

            entity.ToTable("dil");

            entity.Property(e => e.DilId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("dil_id");
            entity.Property(e => e.Tanim).HasColumnName("tanim");
        });

        modelBuilder.Entity<Kategori>(entity =>
        {
            entity.HasKey(e => e.KategoriId).HasName("kategori_pkey");

            entity.ToTable("kategori");

            entity.Property(e => e.KategoriId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("kategori_id");
            entity.Property(e => e.Tanim).HasColumnName("tanim");
        });

        modelBuilder.Entity<Kelime>(entity =>
        {
            entity.HasKey(e => e.KelimeId).HasName("kelime_pkey");

            entity.ToTable("kelime");

            entity.Property(e => e.KelimeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("kelime_id");
            entity.Property(e => e.DilId).HasColumnName("dil_id");
            entity.Property(e => e.Tanim).HasColumnName("tanim");
        });

        modelBuilder.Entity<KelimeKategori>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("kelime_kategori");

            entity.Property(e => e.KategoriId).HasColumnName("kategori_id");
            entity.Property(e => e.KelimeId).HasColumnName("kelime_id");

            entity.HasOne(d => d.Kategori).WithMany()
                .HasForeignKey(d => d.KategoriId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("kelime_kategori_kategori_id_fkey");

            entity.HasOne(d => d.Kelime).WithMany()
                .HasForeignKey(d => d.KelimeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("kelime_kategori_kelime_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
