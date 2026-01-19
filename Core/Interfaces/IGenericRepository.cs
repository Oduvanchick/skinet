using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entitites;

namespace Core.Interfaces
{
    /// <summary>
    /// Defines a generic repository interface for CRUD operations and querying entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The entity type, which must inherit from <see cref="BaseEntity"/>.</typeparam>
    public interface IGenericRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The entity if found; otherwise, null.</returns>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves all entities of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>A read-only list of all entities.</returns>
        Task<IReadOnlyList<T>> ListAllAsync();

        /// <summary>
        /// Retrieves a single entity matching the given specification.
        /// </summary>
        /// <param name="specification">The specification to filter entities.</param>
        /// <returns>The entity if found; otherwise, null.</returns>
        Task<T?> GetEntityWithSpec(ISpecification<T> specification);

        /// <summary>
        /// Retrieves a list of entities matching the given specification.
        /// </summary>
        /// <param name="specification">The specification to filter entities.</param>
        /// <returns>A read-only list of matching entities.</returns>
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification);

        /// <summary>
        /// Retrieves a single projected entity matching the given specification.
        /// </summary>
        /// <typeparam name="TResult">The type of the projected result.</typeparam>
        /// <param name="specification">The specification to filter and project entities.</param>
        /// <returns>The projected entity if found; otherwise, null.</returns>
        Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> specification);

        /// <summary>
        /// Retrieves a list of projected entities matching the given specification.
        /// </summary>
        /// <typeparam name="TResult">The type of the projected results.</typeparam>
        /// <param name="specification">The specification to filter and project entities.</param>
        /// <returns>A read-only list of projected results.</returns>
        Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification);

        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        void Add(T entity);

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);

        /// <summary>
        /// Removes an entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        void Remove(T entity);

        /// <summary>
        /// Persists all changes to the data store asynchronously.
        /// </summary>
        /// <returns>True if changes were saved successfully; otherwise, false.</returns>
        Task<bool> SaveAllAsync();

        /// <summary>
        /// Checks if an entity with the specified identifier exists.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>True if the entity exists; otherwise, false.</returns>
        bool Exists(int id);

        /// <summary>
        /// Counts the number of entities matching the given specification.
        /// </summary>
        /// <param name="specification">The specification to filter entities.</param>
        /// <returns>The count of matching entities.</returns>
        Task<int> CountAsync(ISpecification<T> specification);
    }
}