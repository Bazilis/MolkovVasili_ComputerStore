using ComputerStore.DAL.Entities;
using ComputerStore.DAL.Entities.CategoryCharacteristics.Double;
using ComputerStore.DAL.Entities.CategoryCharacteristics.Int;
using ComputerStore.DAL.Entities.CategoryCharacteristics.String;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.DAL.EF
{
    public sealed class StoreDbContext : IdentityDbContext<IdentityUser>
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
                .WithMany()
                .HasForeignKey(characteristic => characteristic.ProductCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CategoryCharacteristicIntEntity>()
                .HasOne<ProductCategoryEntity>()
                .WithMany()
                .HasForeignKey(characteristic => characteristic.ProductCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CategoryCharacteristicStringEntity>()
                .HasOne<ProductCategoryEntity>()
                .WithMany()
                .HasForeignKey(characteristic => characteristic.ProductCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            //-------------------------------------------------------------------------------------------------

            modelBuilder.Entity<CharacteristicValueDoubleEntity>()
                .HasOne<CategoryCharacteristicDoubleEntity>()
                .WithMany()
                .HasForeignKey(value => value.CategoryCharacteristicDoubleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CharacteristicValueIntEntity>()
                .HasOne<CategoryCharacteristicIntEntity>()
                .WithMany()
                .HasForeignKey(value => value.CategoryCharacteristicIntId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CharacteristicValueStringEntity>()
                .HasOne<CategoryCharacteristicStringEntity>()
                .WithMany()
                .HasForeignKey(value => value.CategoryCharacteristicStringId)
                .OnDelete(DeleteBehavior.NoAction);

            //-------------------------------------------------------------------------------------------------

            modelBuilder.Entity<CharacteristicValueDoubleEntity>()
                .HasMany(c => c.Products)
                .WithMany(p => p.CategoryCharacteristicsDouble)
                .UsingEntity(e => e.ToTable("ProductsValuesDouble"));

            modelBuilder.Entity<CharacteristicValueIntEntity>()
                .HasMany(c => c.Products)
                .WithMany(p => p.CategoryCharacteristicsInt)
                .UsingEntity(e => e.ToTable("ProductsValuesInt"));

            modelBuilder.Entity<CharacteristicValueStringEntity>()
                .HasMany(c => c.Products)
                .WithMany(p => p.CategoryCharacteristicsString)
                .UsingEntity(e => e.ToTable("ProductsValuesString"));

            //-------------------------------------------------------------------------------------------------

            modelBuilder.Entity<OrderEntity>()
                .HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(order => order.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderEntity>()
                .HasOne<ProductEntity>()
                .WithMany()
                .HasForeignKey(order => order.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
