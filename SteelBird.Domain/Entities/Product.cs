using SteelBird.Domain.Contracts;

namespace SteelBird.Domain.Entities;

public class Product : BaseEntity<int>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Barcode { get; set; }
    public bool IsActivate { get; private set; }

    public void Activate()
        => IsActivate = true;

    public void DeActivate()
        => IsActivate = false;
}
