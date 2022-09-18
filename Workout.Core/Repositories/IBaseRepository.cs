using Workout.Core.Entities;
using Workout.Core.Querying;

namespace Workout.Core.Repositories;

/// <summary>
/// Base mechanism for all repositories.
/// </summary>
/// <typeparam name="T">Type of the entity that this repository is responsible of.</typeparam>
/// <typeparam name="TId">Type of the primary key of the entity.</typeparam>
public interface IBaseRepository<T, TId> where T : IBaseEntity<TId>
{
    /// <summary>
    /// Retrieves specific record that has given primary key value.
    /// </summary>
    /// <param name="id">Primary key value.</param>
    /// <returns>Entity that has the given id.</returns>
    Task<T> GetByIdAsync(TId id);

    /// <summary>
    /// Retrieves paginated list of the entities.
    /// </summary>
    /// <param name="pagingArgs">Paging arguments.</param>
    /// <returns>Paginated list of entities.</returns>
    Task<IReadOnlyList<T>> ListAllAsync(PagingArgs pagingArgs);

    /// <summary>
    /// Adds given entities to the database.
    /// </summary>
    /// <param name="entities">Entities that will be added to database.</param>
    /// <returns>Added entities.</returns>
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

    /// <summary>
    /// Updates data of the entity with given id.
    /// </summary>
    /// <param name="id">Primary key value of the entity.</param>
    /// <param name="entity">New data for the entity.</param>
    /// <returns>Updated entity.</returns>
    Task<T> UpdateAsync(TId id, T entity);

    /// <summary>
    /// Adds new entity to the database.
    /// </summary>
    /// <param name="entity">Entity data.</param>
    /// <returns>Newly added entity.</returns>
    Task<T> SaveAsync(T entity);

    /// <summary>
    /// Deletes entity from the database.
    /// </summary>
    /// <param name="id">Primary key value of the entity.</param>
    /// <returns>Succession state for the operation.</returns>
    Task<bool> DeleteAsync(TId id);

    /// <summary>
    /// Returns count of the records.
    /// </summary>
    /// <returns>Record count.</returns>
    Task<int> CountAsync();
}
