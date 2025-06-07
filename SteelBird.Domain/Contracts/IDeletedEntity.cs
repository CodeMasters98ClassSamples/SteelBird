namespace SteelBird.Domain.Contracts;
public interface IDeletedEntity
{
    public DateTime DeletedAt { get; set; }
    public long DeletedByUserId { get; set; }
}
