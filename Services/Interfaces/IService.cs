namespace Contacts.Services.Interfaces
{
    public interface IService<T> where T: class
    {
        T Add(T entity);
        int SaveChanges();
        void Delete(T entity);
    }
}
