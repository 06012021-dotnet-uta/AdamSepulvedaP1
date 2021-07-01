using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ModelsLibrary
{
    public partial class P1DatabaseContext : DbContext
    {
        public P1DatabaseContext()
        {
        }

        public P1DatabaseContext(DbContextOptions<P1DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<StoreInventory> StoreInventories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=P1Database;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.Username, "UQ__Customer__536C85E48A349862")
                    .IsUnique();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UserPassord)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TotalPrice).HasColumnType("smallmoney");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Orders__Customer__412EB0B6");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__Orders__StoreId__4222D4EF");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("PK__OrderIte__08D097A33834E2A7");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderItem__Order__45F365D3");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderItem__Produ__46E78A0C");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).ValueGeneratedNever();

                entity.Property(e => e.Price).HasColumnType("smallmoney");

                entity.Property(e => e.ProductDesc)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.StoreId).ValueGeneratedNever();

                entity.Property(e => e.StoreCity).HasMaxLength(100);

                entity.Property(e => e.StoreState).HasMaxLength(3);

                entity.Property(e => e.StoreStreet).HasMaxLength(100);
            });

            modelBuilder.Entity<StoreInventory>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.ProductId })
                    .HasName("PK__StoreInv__F0C23D6D7C9E16E9");

                entity.ToTable("StoreInventory");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.StoreInventories)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StoreInve__Produ__4AB81AF0");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreInventories)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StoreInve__Store__49C3F6B7");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
