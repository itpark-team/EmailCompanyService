using System;
using System.Collections.Generic;
using ConsoleAppServerSide.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppServerSide.DbConnector;

public partial class EcsDbContext : DbContext
{
    public EcsDbContext()
    {
    }

    public EcsDbContext(DbContextOptions<EcsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Mail> Mails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=194.67.105.79:5432;Database=ecs_db;Username=ecs_user;Password=12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mails_pk");

            entity.ToTable("mails");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_datetime");
            entity.Property(e => e.IdFrom).HasColumnName("id_from");
            entity.Property(e => e.IdTo).HasColumnName("id_to");
            entity.Property(e => e.IsOpened).HasColumnName("is_opened");
            entity.Property(e => e.Message)
                .IsRequired()
                .HasMaxLength(4096)
                .HasColumnName("message");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("title");

            entity.HasOne(d => d.IdFromNavigation).WithMany(p => p.MailIdFromNavigations)
                .HasForeignKey(d => d.IdFrom)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("mails_users_id_fk");

            entity.HasOne(d => d.IdToNavigation).WithMany(p => p.MailIdToNavigations)
                .HasForeignKey(d => d.IdTo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("mails_users_id_fk_2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pk");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(32)
                .HasColumnName("email");
            entity.Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(32)
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(32)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(32)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
