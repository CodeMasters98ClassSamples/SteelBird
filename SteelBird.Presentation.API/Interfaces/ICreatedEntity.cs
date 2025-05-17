namespace SteelBird.Presentation.API.Interfaces;

public interface ICreatedEntity
{
    public DateTime CreateAt { get; set; }
    public long CreateByUserId { get; set; }
}
