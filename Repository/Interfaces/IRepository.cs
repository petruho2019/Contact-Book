namespace Contacts.Repository.Interfaces
{
    public interface IRepository<T> where T: class
    {
        T Add(T entity);
        int SaveShanges();
        T? GetById(int id);
    }
}
