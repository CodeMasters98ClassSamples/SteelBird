
using SteelBird.Domain.Contracts;
using SteelBird.Domain.Enums;
using SteelBird.Domain.ValueObjects;

namespace SteelBird.Domain.Entities;

public class Order : IFullEntity
{
    public int Id { get; set; }
    public string Description { get; set; }
    public Guid Code { get; set; }
    public DateTime CreateAt { get; set; }
    public long CreateByUserId { get; set; }
    public DateTime DeletedAt { get; set; }
    public long DeletedByUserId { get; set; }
    public DateTime UpdateAt { get; set; }
    public long UpdateByUserId { get; set; }
    public OrderState OrderState { get; set; }

    private readonly List<OrderItem> _items = new();

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public Money Total => _items.Aggregate(Money.Zero("USD"), (sum, l) => sum.Add(l.LineTotal));

    private Order() { } // for ORM

    public Order(int id) => Id = id;

    public void AddItem(int productId, int qty, Money unitPrice)
    {
        EnsureDraft();
        if (qty <= 0) throw new Exception("Qty must be positive.");

        var existing = _items.FirstOrDefault(l => l.ProductId == productId);
        if (existing is not null)
        {
            existing.Increase(qty);
        }
        else
        {
            _items.Add(new OrderItem(productId, qty, unitPrice));
        }
    }

    public void RemoveItem(int productId)
    {
        EnsureDraft();
        _items.RemoveAll(l => l.ProductId == productId);
        if (_items.Count == 0)
            throw new Exception("Order must have at least one line.");
    }

    public void Checkout()  // invariant gate
    {
        EnsureDraft();
        if (_items.Count == 0) throw new Exception("Empty order.");
        OrderState = OrderState.PENDING;

        // Raise a domain event (for payment, inventory, etc.)
        // DomainEvents.Raise(new OrderPlaced(Id, Total));
    }

    private void EnsureDraft()
    {
        if (OrderState != OrderState.PENDING)
            throw new Exception("Order is not modifiable.");
    }
}
