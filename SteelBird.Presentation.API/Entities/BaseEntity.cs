namespace SteelBird.Presentation.API.Entities;

public abstract class BaseEntity<T>
{
    public T Id { get; set; }
    public DateTime CreateAt { get; set; }
    public long CreateByUserId { get; set; }
    public DateTime DeletedAt { get; set; }
    public long DeletedByUserId { get; set; }
    public DateTime UpdateAt { get; set; }
    public long UpdateByUserId { get; set; }
}
