namespace SteelBird.Presentation.API.Contracts;

public interface IBaseService<T>
{
    T GetById(int id);
    List<T> GetAll();
    bool Add(T item);
    bool Update(T item);
    bool Delete(int id);
}
