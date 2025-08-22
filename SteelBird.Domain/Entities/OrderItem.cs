using SteelBird.Domain.ValueObjects;

namespace SteelBird.Domain.Entities;

public class OrderItem
{
    public int ProductId { get; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; }

    public Money LineTotal => UnitPrice.Multiply(Quantity);

    internal OrderItem(int productId, int qty, Money unitPrice)
    {
        ProductId = productId;
        Quantity = qty;
        UnitPrice = unitPrice;
    }

    internal void Increase(int qty) => Quantity += qty;
}
