using Microsoft.EntityFrameworkCore;

namespace YourSoundCompany.RelationalData
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> GetById(long? Id);
        Task SaveChanges();
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task Delete(long id);
    }
}
