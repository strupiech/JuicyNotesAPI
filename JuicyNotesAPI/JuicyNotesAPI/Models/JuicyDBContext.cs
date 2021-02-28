using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace JuicyNotesAPI.Models
{
    public partial class JuicyDBContext : DbContext
    {
        public JuicyDBContext()
        {
        }

        public JuicyDBContext(DbContextOptions<JuicyDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<CollectionItem> CollectionItems { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserCollection> UserCollections { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Collection>(entity =>
            {
                entity.HasKey(e => e.IdCollection)
                    .HasName("Collection_pk");

                entity.ToTable("Collection");

                entity.Property(e => e.IdCollection).HasColumnName("idCollection");

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("color");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creationDate");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<CollectionItem>(entity =>
            {
                entity.HasKey(e => new { e.IdItem, e.IdCollection })
                    .HasName("CollectionItem_pk");

                entity.ToTable("CollectionItem");

                entity.Property(e => e.IdItem).HasColumnName("idItem");

                entity.Property(e => e.IdCollection).HasColumnName("idCollection");

                entity.HasOne(d => d.IdCollectionNavigation)
                    .WithMany(p => p.CollectionItems)
                    .HasForeignKey(d => d.IdCollection)
                    .HasConstraintName("Collection_CollectionItem");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.CollectionItems)
                    .HasForeignKey(d => d.IdItem)
                    .HasConstraintName("Item_CollectionItem");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.IdItem)
                    .HasName("Item_pk");

                entity.ToTable("Item");

                entity.Property(e => e.IdItem).HasColumnName("idItem");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("content");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creationDate");

                entity.Property(e => e.KnowledgeRating).HasColumnName("knowledgeRating");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("User_pk");

                entity.ToTable("User");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("salt");

                entity.Property(e => e.Surname)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("surname");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<UserCollection>(entity =>
            {
                entity.HasKey(e => new { e.IdUser, e.IdCollection })
                    .HasName("UserCollection_pk");

                entity.ToTable("UserCollection");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.IdCollection).HasColumnName("idCollection");

                entity.HasOne(d => d.IdCollectionNavigation)
                    .WithMany(p => p.UserCollections)
                    .HasForeignKey(d => d.IdCollection)
                    .HasConstraintName("UserCollection_Collection");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserCollections)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("UserCollection_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
