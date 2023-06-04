namespace tk_web.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task Create(T obj);

        IQueryable<T> GetAll();

        Task Delete(T obj);

        Task<T> Update(T obj);
    }
}
