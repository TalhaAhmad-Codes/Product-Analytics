using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductAnalytics.Models;

namespace ProductAnalytics.Data.Configurations
{
    public sealed class LogsConfiguration : IEntityTypeConfiguration<Logs>
    {
        public void Configure(EntityTypeBuilder<Logs> builder)
        {
            /* Id - Config */
            builder.HasKey(l => l.Id);

            /* Product Id - Config */
            builder.HasOne(l => l.Product)
                   .WithMany(p => p.Logs)
                   .HasForeignKey(l => l.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(l => new { l.ProductId, l.SellDate })
                   .IsUnique();

            /* Sell Date - Config */
            builder.Property(l => l.SellDate)
                   .HasColumnName("SellDate")
                   .IsRequired();

            /* Quantity - Config */
            builder.Property(l => l.Quantity)
                   .HasColumnName("Quantity")
                   .IsRequired();

            /* Created At - Config */
            builder.Property(l => l.CreatedAt)
                   .HasColumnName("CreatedAt")
                   .IsRequired();
        }
    }
}
