namespace Server.Models.Interfaces
{
    public interface IRepository<T>
    {
        ICollection<T> Entities { get; }

        void Add(T entity);

        bool Remove(T entity);
    }
}
