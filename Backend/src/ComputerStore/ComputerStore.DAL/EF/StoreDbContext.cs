using ComputerStore.DAL.Entities;
using ComputerStore.DAL.Entities.CategoryCharacteristics.Double;
using ComputerStore.DAL.Entities.CategoryCharacteristics.Int;
using ComputerStore.DAL.Entities.CategoryCharacteristics.String;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.DAL.EF
{
    public sealed class StoreDbContext : IdentityDbContext<UserEntity>
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> optionsBuilder)
            : base(optionsBuilder)
        {
        }

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductCategoryEntity> ProductCategories { get; set; }
        public DbSet<CategoryCharacteristicDoubleEntity> CategoryCharacteristicDouble { get; set; }
        public DbSet<CategoryCharacteristicIntEntity> CategoryCharacteristicInt { get; set; }
        public DbSet<CategoryCharacteristicStringEntity> CategoryCharacteristicString { get; set; }
        public DbSet<CharacteristicValueDoubleEntity> CharacteristicValuesDouble { get; set; }
        public DbSet<CharacteristicValueIntEntity> CharacteristicValuesInt { get; set; }
        public DbSet<CharacteristicValueStringEntity> CharacteristicValuesString { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEntity>()
                .HasOne<ProductCategoryEntity>()
                .WithMany()
                .HasForeignKey(product => product.ProductCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            //-------------------------------------------------------------------------------------------------

            modelBuilder.Entity<CategoryCharacteristicDoubleEntity>()
                .HasOne<ProductCategoryEntity>()
                .WithMany(pc => pc.CategoryCharacteristicsDouble)
                .HasForeignKey(characteristic => characteristic.ProductCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CategoryCharacteristicIntEntity>()
                .HasOne<ProductCategoryEntity>()
                .WithMany(pc => pc.CategoryCharacteristicsInt)
                .HasForeignKey(characteristic => characteristic.ProductCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CategoryCharacteristicStringEntity>()
                .HasOne<ProductCategoryEntity>()
                .WithMany(pc => pc.CategoryCharacteristicsString)
                .HasForeignKey(characteristic => characteristic.ProductCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            //-------------------------------------------------------------------------------------------------

            modelBuilder.Entity<CharacteristicValueDoubleEntity>()
                .HasOne<CategoryCharacteristicDoubleEntity>()
                .WithMany(c => c.CharacteristicValuesDouble)
                .HasForeignKey(value => value.CategoryCharacteristicDoubleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CharacteristicValueIntEntity>()
                .HasOne<CategoryCharacteristicIntEntity>()
                .WithMany(c => c.CharacteristicValuesInt)
                .HasForeignKey(value => value.CategoryCharacteristicIntId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CharacteristicValueStringEntity>()
                .HasOne<CategoryCharacteristicStringEntity>()
                .WithMany(c => c.CharacteristicValuesString)
                .HasForeignKey(value => value.CategoryCharacteristicStringId)
                .OnDelete(DeleteBehavior.NoAction);

            //-------------------------------------------------------------------------------------------------

            modelBuilder.Entity<ProductEntity>()
                .HasMany(p => p.CategoryCharacteristicsDouble)
                .WithMany(c => c.Products)
                .UsingEntity(e => e.ToTable("ProductsValuesDouble"));

            modelBuilder.Entity<ProductEntity>()
                .HasMany(p => p.CategoryCharacteristicsInt)
                .WithMany(c => c.Products)
                .UsingEntity(e => e.ToTable("ProductsValuesInt"));

            modelBuilder.Entity<ProductEntity>()
                .HasMany(p => p.CategoryCharacteristicsString)
                .WithMany(c => c.Products)
                .UsingEntity(e => e.ToTable("ProductsValuesString"));

            //-------------------------------------------------------------------------------------------------

            modelBuilder.Entity<OrderEntity>()
                .HasOne<UserEntity>()
                .WithMany(u => u.Orders)
                .HasForeignKey(order => order.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderEntity>()
                .HasOne<ProductEntity>()
                .WithMany(p => p.Orders)
                .HasForeignKey(order => order.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
