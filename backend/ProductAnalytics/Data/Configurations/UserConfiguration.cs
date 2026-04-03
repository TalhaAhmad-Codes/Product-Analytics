using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductAnalytics.Data.LengthLimits;
using ProductAnalytics.Enums;
using ProductAnalytics.Models;

namespace ProductAnalytics.Data.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            /* Id - Config */
            builder.HasKey(u => u.Id);

            /* Profile Picture - Config */
            builder.Property(u => u.ProfilePicture)
                   .HasColumnName("ProfilePic");

            /* Username - Config */
            builder.Property(u => u.Username)
                   .HasColumnName("Username")
                   .HasMaxLength(Maximum.Username)
                   .IsRequired();

            builder.HasIndex(u => u.Username)
                   .IsUnique();

            /* Password - Config */
            builder.Property(u => u.PasswordHash)
                   .HasColumnName("Password")
                   .IsRequired();

            /* Role - Config */
            builder.Property(u => u.Role)
                   .HasColumnName("Role")
                   .HasDefaultValue(Role.Owner)
                   .IsRequired();

            /* Created At - Config */
            builder.Property(u => u.CreatedAt)
                   .HasColumnName("CreatedAt")
                   .IsRequired();
        }
    }
}
