using System.Collections.Generic;

namespace AspNetCoreRepodb.Repositories
{
    public interface IRepository<T>
    {
        string ConnectionString { get; }
        void Add(T item);
        void Remove(int id);
        void Update(T item);
        T FindByID(int id);
        IEnumerable<T> FindAll();
    }
    
}