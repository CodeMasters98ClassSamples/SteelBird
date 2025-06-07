namespace SteelBird.Domain.Contracts;

public interface ICreatedEntity
{
    public DateTime CreateAt { get; set; }
    public long CreateByUserId { get; set; }
}
