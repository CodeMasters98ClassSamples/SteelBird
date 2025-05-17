namespace SteelBird.Presentation.API.Interfaces;

public interface IUpdatedEntity
{
    public DateTime UpdateAt { get; set; }
    public long UpdateByUserId { get; set; }
}
