using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ch.gibz.m151.projekt.Models
{
    public partial class BuenzliTreffContext : DbContext
    {
        public BuenzliTreffContext()
        {
        }

        public BuenzliTreffContext(DbContextOptions<BuenzliTreffContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Beitrag> Beitrags { get; set; }
        public virtual DbSet<BeitragDatei> BeitragDateis { get; set; }
        public virtual DbSet<BeitragLike> BeitragLikes { get; set; }
        public virtual DbSet<Datei> Dateis { get; set; }
        public virtual DbSet<Kommentar> Kommentars { get; set; }
        public virtual DbSet<KommentarLike> KommentarLikes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BuenzliTreff;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Beitrag>(entity =>
            {
                entity.ToTable("Beitrag");

                entity.Property(e => e.ErstelltAm)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Inhalt)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Titel)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Autor)
                    .WithMany(p => p.Beitrags)
                    .HasForeignKey(d => d.AutorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Autor_Beitraege");
            });

            modelBuilder.Entity<BeitragDatei>(entity =>
            {
                entity.ToTable("BeitragDatei");
            });

            modelBuilder.Entity<BeitragLike>(entity =>
            {
                entity.ToTable("BeitragLike");

                entity.HasOne(d => d.Beitrag)
                    .WithMany(p => p.BeitragLikes)
                    .HasForeignKey(d => d.BeitragId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Beitrag_Likes");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BeitragLikes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_BeitragLikes");
            });

            modelBuilder.Entity<Datei>(entity =>
            {
                entity.ToTable("Datei");

                entity.Property(e => e.File).IsRequired();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Kommentar>(entity =>
            {
                entity.ToTable("Kommentar");

                entity.Property(e => e.Inhalt)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Titel)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Autor)
                    .WithMany(p => p.Kommentars)
                    .HasForeignKey(d => d.AutorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Autor_Kommentare");

                entity.HasOne(d => d.Beitrag)
                    .WithMany(p => p.Kommentars)
                    .HasForeignKey(d => d.BeitragId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Beitrag_Kommentare");
            });

            modelBuilder.Entity<KommentarLike>(entity =>
            {
                entity.ToTable("KommentarLike");

                entity.HasOne(d => d.Kommentar)
                    .WithMany(p => p.KommentarLikes)
                    .HasForeignKey(d => d.KommentarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Kommentar_Likes");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.KommentarLikes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_KommentarLikes");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
