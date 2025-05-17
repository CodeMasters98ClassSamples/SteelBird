using SteelBird.Presentation.API.Contracts;
using SteelBird.Presentation.API.Entities;

namespace SteelBird.Presentation.API.Service;

public class ProductService : IBaseService<Product>
{
    public List<Product> products = new List<Product>();

    public bool Add(Product item)
    {
        products.Add(item);
        return true;
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public List<Product> GetAll()
    {
        return products;
    }

    public Product GetById(int id)
    {
        return products.FirstOrDefault(x => x.Id == id);
    }

    public bool Update(Product item)
    {
        throw new NotImplementedException();
    }
}
