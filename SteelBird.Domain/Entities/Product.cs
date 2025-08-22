using SteelBird.Domain.Contracts;
using SteelBird.Domain.ValueObjects;

namespace SteelBird.Domain.Entities;

public class Product : BaseEntity<int>
{
    public string Name { get; set; }
    public Money Price { get; set; }
    public string Description { get; set; }
    public string Barcode { get; set; }
    public bool IsActivate { get; private set; }

    public Category Category { get; set; }
    public int CategoryId { get; set; } //DB

    public void Activate()
        => IsActivate = true;

    public void DeActivate()
        => IsActivate = false;
}
