using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API_EDII.Entities;

public partial class EdiiDbContext : DbContext
{
    public EdiiDbContext()
    {
    }

    public EdiiDbContext(DbContextOptions<EdiiDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=;database=edii_db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("PRIMARY");

            entity.ToTable("tbl_user");

            entity.Property(e => e.Userid)
                .HasColumnType("int(11)")
                .HasColumnName("userid");
            entity.Property(e => e.Namalengkap)
                .HasMaxLength(45)
                .HasColumnName("namalengkap");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .HasColumnName("password");
            entity.Property(e => e.Status)
                .HasMaxLength(45)
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(45)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
