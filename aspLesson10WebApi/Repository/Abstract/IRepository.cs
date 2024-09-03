using System.Linq.Expressions;

namespace aspLesson10WebApi.Repository.Abstract;
public interface IRepository<T> where T : class,new()
{
    // public methods which will be implemented in classes : 
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetAsync(Expression<Func<T, bool>> expression);
    Task AddAsync(T entity);    
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
