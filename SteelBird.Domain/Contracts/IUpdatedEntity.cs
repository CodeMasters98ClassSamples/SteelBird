namespace SteelBird.Domain.Contracts;

public interface IUpdatedEntity
{
    public DateTime UpdateAt { get; set; }
    public long UpdateByUserId { get; set; }
}
