using OfferService.Models;

namespace OfferService.Database.Entities;

public sealed class CategoryEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public CategoryEntity(Category category)
    {
        Id = new int();
        Name = category.Name;
    }

    public CategoryEntity()
    {
    }
}