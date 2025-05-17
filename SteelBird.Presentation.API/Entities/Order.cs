using SteelBird.Presentation.API.Interfaces;

namespace SteelBird.Presentation.API.Entities
{
    public class Order : IFullEntity
    {
        public Guid Code { get; set; }
        public DateTime CreateAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public long CreateByUserId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime DeletedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public long DeletedByUserId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime UpdateAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public long UpdateByUserId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
