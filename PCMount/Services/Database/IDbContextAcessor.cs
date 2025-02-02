namespace PCMount.Services.Database;

using System.Linq.Expressions;

public interface IDbContextAcessor<T> where T : class {
    Task<T[]> GetArrayAsync();

    Task<List<T>> GetListAsync();

    Task<T?> DeleteAsync(int id);

    Task<T?> UpdateAsync(int id, T item);

    Task<T?> CreateAsync(T item);

    bool Any(Expression<Func<T, bool>> predicate);

    Task<List<T>> IncludeListAsync<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath);

    Task<T?> FindAsync(int id);

    Task<T?> FindOneAsync(Expression<Func<T, bool>> predicate);

    Task<T[]> FindAllAsync(Expression<Func<T, bool>> predicate);
}