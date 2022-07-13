using Model;

namespace Repository.Interface
{
    public interface IRepository<T>
        where T : ITerm
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        List<T> FindAll(string word);
        T Get(string word);
        List<T> GetAll();
    }
}
