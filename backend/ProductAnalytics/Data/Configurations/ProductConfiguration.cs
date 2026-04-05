using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductAnalytics.Data.LengthLimits;
using ProductAnalytics.Models;

namespace ProductAnalytics.Data.Configurations
{
    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            /* Id - Config */
            builder.HasKey(p => p.Id);

            /* Profile Picture - Config */
            builder.Property(p => p.Image)
                   .HasColumnName("Image");

            /* Username - Config */
            builder.Property(p => p.Name)
                   .HasColumnName("Name")
                   .HasMaxLength(Maximum.ProductName)
                   .IsRequired();

            builder.HasIndex(p => p.Name)
                   .IsUnique();

            /* Price - Config */
            builder.Property(p => p.Price)
                   .HasColumnName("Price")
                   .HasPrecision(18, 2)
                   .IsRequired();

            /* Stock Status - Config */
            builder.Property(p => p.StockStatus)
                   .HasColumnName("Status")
                   .IsRequired();

            /* Created At - Config */
            builder.Property(p => p.CreatedAt)
                   .HasColumnName("CreatedAt")
                   .IsRequired();

            /* Logs - Config */
            builder.HasMany(p => p.Logs)
                   .WithOne(l => l.Product)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
