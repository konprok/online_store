using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfferService.Database.Entities;

namespace OfferService.Database.Configuration;

public sealed class OfferEntityConfiguration : IEntityTypeConfiguration<OfferEntity>
{
    public void Configure(EntityTypeBuilder<OfferEntity> builder)
    {
        builder.ToTable("offer_entity");

        builder.HasKey(x => x.Id)
            .HasName("PK_offer_entity");
        
        builder.Property(x => x.Title)
            .HasColumnName("title")
            .IsRequired()
            .HasMaxLength(255);
        
        builder.Property(x => x.CategoryId)
            .HasColumnName("category")
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(2000);
        
        builder.Property(x => x.Logo)
            .HasColumnName("logo")
            .HasDefaultValue(null);
        
        builder.Property(x => x.Price)
            .HasColumnName("price")
            .IsRequired();
        
        builder.Property(x => x.CreatedBy)
            .HasColumnName("created_by")
            .IsRequired();
        
        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();
        
        builder.Property(x => x.ModifiedAt)
            .HasColumnName("modified_at")
            .HasDefaultValue(null);

        builder.Property(x => x.IsDeleted)
            .HasColumnName("deleted")
            .IsRequired()
            .HasDefaultValue(false);
    }
}