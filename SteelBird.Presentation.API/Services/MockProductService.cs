using SteelBird.Presentation.API.Contracts;
using SteelBird.Presentation.API.Entities;

namespace SteelBird.Presentation.API.Services
{
    public class MockProductService : IBaseService<Product>
    {
        public bool Add(Product item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Product item)
        {
            throw new NotImplementedException();
        }
    }
}
