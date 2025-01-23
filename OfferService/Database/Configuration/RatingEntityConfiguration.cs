using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfferService.Database.Entities;

namespace OfferService.Database.Configuration;

public sealed class RatingEntityConfiguration : IEntityTypeConfiguration<RatingEntity>
{
    public void Configure(EntityTypeBuilder<RatingEntity> builder)
    {
        builder.ToTable("rating_entity");

        builder.HasKey(x => x.Id)
            .HasName("PK_rating_entity");
        
        builder.Property(x => x.Title)
            .HasColumnName("title")
            .IsRequired()
            .HasMaxLength(255);
        
        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(2000);
        
        builder.Property(x => x.CreatedBy)
            .HasColumnName("created_by")
            .IsRequired();
        
        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();
        
        builder.Property(x => x.ModifiedAt)
            .HasColumnName("modified_at")
            .HasDefaultValue(null);

        builder.Property(x => x.Rating)
            .HasColumnName("rating")
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .HasColumnName("deleted")
            .IsRequired()
            .HasDefaultValue(false);
        
        builder.HasOne(x => x.Offer)
            .WithMany(o => o.Ratings)
            .HasForeignKey(x => x.OfferId)
            .HasConstraintName("FK_RatingEntity_OfferEntity")
            .OnDelete(DeleteBehavior.Cascade);
    }
}