using SteelBird.Domain.Entities;
using SteelBird.Presentation.API.Contracts;
using SteelBird.Infrastructure.Persistence.Contexts;

namespace SteelBird.Presentation.API.Service;

public class ProductService : IBaseService<Product>
{

    private readonly CoreDatabaseContext _context;
    public ProductService(CoreDatabaseContext context)
    {
        _context = context;
    }

    public bool Add(Product item)
    {
        Console.WriteLine(_context.Entry(item).State);
        _context.Set<Product>().Add(item);
        Console.WriteLine(_context.Entry(item).State);
        _context.SaveChanges();
        Console.WriteLine(_context.Entry(item).State);
        return true;
    }

    public bool Delete(int id)
    {
        //_context.Set<Product>().Remove(item);
        return false;
    }

    public List<Product> GetAll()
    {
        var products = _context.Set<Product>().ToList();
        //Api Call Service

        return products;
    }

    public Product GetById(int id)
    {


        return _context.Set<Product>().Where(x => x.Id == id).FirstOrDefault();
    }

    public bool Update(Product item)
    {
        throw new NotImplementedException();
    }
}
