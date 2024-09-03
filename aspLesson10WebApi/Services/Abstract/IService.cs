using System.Linq.Expressions;

namespace aspLesson10WebApi.Services.Abstract;
public interface IService<T> where T : class,new()
{
    // public methods will be implemented in classes : 
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetAsync(Expression<Func<T, bool>> expression);
    Task AddAsync (T entity);
    Task DeleteAsync(T entity);
    Task UpdateAsync(T entity);
}
