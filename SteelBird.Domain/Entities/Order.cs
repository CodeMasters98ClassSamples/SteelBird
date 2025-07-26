
using SteelBird.Domain.Contracts;
using SteelBird.Domain.Enums;

namespace SteelBird.Domain.Entities;

public class Order : IFullEntity
{
    public string Description { get; set; }
    public Guid Code { get; set; }
    public DateTime CreateAt { get; set; }
    public long CreateByUserId { get; set; }
    public DateTime DeletedAt { get; set; }
    public long DeletedByUserId { get; set; }
    public DateTime UpdateAt { get; set; }
    public long UpdateByUserId { get; set; }
    public OrderState OrderState { get; set; }
}
