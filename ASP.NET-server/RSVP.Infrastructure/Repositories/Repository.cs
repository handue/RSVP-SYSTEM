using Microsoft.EntityFrameworkCore;
using RSVP.Core.Interfaces;
using RSVP.Infrastructure.Data;

namespace RSVP.Infrastructure.Repositories;

// ! Entity Framework Core (from Nuget package) = ORM(Object-Relational Mapping) = 객체와 관계형 데이터베이스의 데이터를 자동으로 매핑해주는 기술 

// ! LINQ = Language Integrated Query = 데이터베이스 쿼리를 객체 지향 프로그래밍 언어로 작성할 수 있게 해주는 기술

// Repository<T>: 제네릭 리포지토리 클래스로, 데이터베이스 작업을 위한 공통 메서드를 제공합니다.
// Repository<T>: A generic repository class that provides common methods for database operations.
// where T : class: T는 참조 타입(클래스)이어야 함을 명시하는 제약 조건입니다.
// where T : class: A constraint specifying that T must be a reference type (class).
// * 기본 CRUD(Create, Read, Update, Delete) 기능을 제공하는 레포지토리
public class Repository<T> : IRepository<T> where T : class
{
    // protected: 현재 클래스와 파생 클래스에서 접근 가능한 접근 제한자
    // protected: Access modifier that allows access from the current class and derived classes
    // private: 현재 클래스에서만 접근 가능하고 파생 클래스에서는 접근 불가능
    // private: Access modifier that allows access only from the current class, not from derived classes
    // readonly: 생성자에서만 값을 할당할 수 있고 이후에는 변경 불가능
    // readonly: Can only be assigned in the constructor and cannot be changed afterwards
    // ApplicationDbContext: 데이터베이스 컨텍스트 클래스
    // ApplicationDbContext: The database context class
    protected readonly ApplicationDbContext _context;
    // DbSet<T>: Entity Framework Core에서 제공하는 클래스로, 데이터베이스 컨텍스트에서 엔티티 집합을 나타냄
    // DbSet<T>: A class provided by Entity Framework Core that represents a set of entities in the database context
    protected readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    // virtual: 파생 클래스에서 이 메서드를 재정의(override)할 수 있게 해주는 키워드입니다.
    // 이를 통해 특정 엔티티 타입에 맞게 동작을 커스터마이즈할 수 있습니다.
    // virtual: A keyword that allows this method to be overridden in derived classes.
    // This enables customizing the behavior for specific entity types.
    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        // ToListAsync(): 데이터베이스에서 모든 엔티티를 비동기적으로 가져와 List<T> 컬렉션으로 변환합니다.
        // ToListAsync(): Asynchronously retrieves all entities from the database and converts them to a List<T> collection.
        return await _dbSet.ToListAsync();
    }

    // Expression<Func<T, bool>>: 쿼리 표현식을 나타내는 표현 트리로, Entity Framework에서 SQL로 변환됩니다.
    // Expression<Func<T, bool>>: An expression tree representing a query expression that gets translated to SQL in Entity Framework.
    // Func<T, bool>: T 타입을 입력받아 bool을 반환하는 대리자(함수)로, 조건식을 나타냅니다.
    // Func<T, bool>: A delegate (function) that takes input of type T and returns bool, representing a condition.
    // ex) var users = await userRepository.FindAsync(user => user.IsActive && user.Role == "Admin");, T = userRepository's entity type
    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public virtual async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public virtual void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public virtual void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public virtual async Task<bool> ExistsAsync(int id)
    {
        return await _dbSet.FindAsync(id) != null;
    }
}