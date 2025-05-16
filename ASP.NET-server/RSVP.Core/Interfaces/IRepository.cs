using System.Linq.Expressions;

namespace RSVP.Core.Interfaces;

public interface IRepository<T> where T : class
{
    // IEnumerable: 컬렉션을 반복할 수 있게 해주는 인터페이스. 데이터를 순차적으로 접근할 수 있음
    // IEnumerable: An interface that allows iteration over collections. Provides sequential data access.

    // LINQ: C#에서 데이터 쿼리 기능을 제공하는 .NET 라이브러리. SQL과 유사한 문법으로 컬렉션 데이터를 필터링, 정렬, 그룹화 가능
    // LINQ: A .NET library providing data query capabilities in C#. Allows filtering, sorting, and grouping collection data with SQL-like syntax.

    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);

    // AddRangeAsync: 여러 엔티티를 한 번에 데이터베이스에 비동기적으로 추가하는 메서드. 일괄 처리로 성능 향상
    // AddRangeAsync: A method that asynchronously adds multiple entities to the database at once. Improves performance through batch processing.
    Task AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    Task<bool> ExistsAsync(int id);
}