using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CartService.Database.Entities;

namespace CartService.Database.Configuration
{
    public class CartEntityConfiguration : IEntityTypeConfiguration<CartEntity>
    {
        public void Configure(EntityTypeBuilder<CartEntity> builder)
        {
            builder.ToTable("cart_entity");

            builder.HasKey(x => x.Id)
                .HasName("PK_cart_entity");

            builder.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(x => x.Offers)
                .HasColumnName("offers");
        }
    }
}
