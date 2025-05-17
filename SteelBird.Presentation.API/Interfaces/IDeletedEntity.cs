namespace SteelBird.Presentation.API.Interfaces;

public interface IDeletedEntity
{
    public DateTime DeletedAt { get; set; }
    public long DeletedByUserId { get; set; }
}
