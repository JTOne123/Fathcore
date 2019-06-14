﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Fathcore.EntityFramework
{
    /// <summary>
    /// Provides the interface for generic cached repository pattern.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity being queried.</typeparam>
    public interface ICachedRepository<TEntity>
        where TEntity : BaseEntity<TEntity>, IBaseEntity
    {
        /// <summary>
        /// Select all entities. If the entities is exists in cache, then it is returned immediately without making a request to the database.
        /// Otherwise, a query is made to the database for the entities, if found, is attached to the context and returned.
        /// If no entity is found, then zero collection is returned.
        /// </summary>
        /// <returns>The entities found, or zero collection.</returns>
        IEnumerable<TEntity> SelectList();

        /// <summary>
        /// Select all entities with the given navigation property values. If the entities is exists in cache, then it is returned immediately without making a request to the database.
        /// Otherwise, a query is made to the database for the entities, if found, is attached to the context and returned.
        /// The navigation property to be included is specified starting with the type of entity being queried (TEntity).
        /// If you wish to include additional types based on the navigation properties of the type being included, then chain a call with comma separated.
        /// </summary>
        /// <param name="navigationProperties">A lambda expression representing the navigation property to be included (t => t.Property1).</param>
        /// <returns>The entities found, or zero collection.</returns>
        IEnumerable<TEntity> SelectList(params Expression<Func<TEntity, object>>[] navigationProperties);

        /// <summary>
        /// Select all entities with the given navigation property values. If the entities is exists in cache, then it is returned immediately without making a request to the database.
        /// Otherwise, a query is made to the database for the entities, if found, is attached to the context and returned.
        /// The navigation property to be included is specified starting with the type of entity being queried (TEntity).
        /// If you wish to include additional types based on the navigation properties of the type being included, then chain a call with comma separated.
        /// </summary>
        /// <param name="navigationProperties">A string representing the navigation property to be included ("Property1").</param>
        /// <returns>The entities found, or zero collection.</returns>
        IEnumerable<TEntity> SelectList(params string[] navigationProperties);

        /// <summary>
        /// Select all entities and filters a sequence of values based on a predicate. If the entities is exists in cache, then it is returned immediately without making a request to the database.
        /// Otherwise, a query is made to the database for the entities, if found, is attached to the context and returned.
        /// If no entity is found, then zero collection is returned.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The entities found that contains elements from the input sequence that satisfy the condition specified by predicate predicate, or zero collection.</returns>
        IEnumerable<TEntity> SelectList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Select all entities with the given navigation property values and filters a sequence of values based on a predicate. If the entities is exists in cache, then it is returned immediately without making a request to the database.
        /// Otherwise, a query is made to the database for the entities, if found, is attached to the context and returned.
        /// The navigation property to be included is specified starting with the type of entity being queried (TEntity).
        /// If you wish to include additional types based on the navigation properties of the type being included, then chain a call with comma separated.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="navigationProperties">A lambda expression representing the navigation property to be included (t => t.Property1).</param>
        /// <returns>The entities found that contains elements from the input sequence that satisfy the condition specified by predicate predicate, or zero collection.</returns>
        IEnumerable<TEntity> SelectList(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] navigationProperties);

        /// <summary>
        /// Select all entities with the given navigation property values and filters a sequence of values based on a predicate. If the entities is exists in cache, then it is returned immediately without making a request to the database.
        /// Otherwise, a query is made to the database for the entities, if found, is attached to the context and returned.
        /// The navigation property to be included is specified starting with the type of entity being queried (TEntity).
        /// If you wish to include additional types based on the navigation properties of the type being included, then chain a call with comma separated.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="navigationProperties">A string representing the navigation property to be included ("Property1").</param>
        /// <returns>The entities found that contains elements from the input sequence that satisfy the condition specified by predicate predicate, or zero collection.</returns>
        IEnumerable<TEntity> SelectList(Expression<Func<TEntity, bool>> predicate, params string[] navigationProperties);

        /// <summary>
        /// Select an entity and filters a sequence of values based on a predicate. If the entity is exists in cache, then it is returned immediately without making a request to the database.
        /// Otherwise, a query is made to the database for the entity, if found, is attached to the context and returned.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The entity found that contains elements from the input sequence that satisfy the condition specified by predicate predicate, or null.</returns>
        TEntity Select(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Select an entity with the given navigation property values and filters a sequence of values based on a predicate. If the entity is exists in cache, then it is returned immediately without making a request to the database.
        /// Otherwise, a query is made to the database for the entity, if found, is attached to the context and returned.
        /// The navigation property to be included is specified starting with the type of entity being queried (TEntity).
        /// If you wish to include additional types based on the navigation properties of the type being included, then chain a call with comma separated.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="navigationProperties">A lambda expression representing the navigation property to be included (t => t.Property1).</param>
        /// <returns>The entity found that contains elements from the input sequence that satisfy the condition specified by predicate predicate, or null.</returns>
        TEntity Select(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] navigationProperties);

        /// <summary>
        /// Select an entity with the given navigation property values and filters a sequence of values based on a predicate. If the entity is exists in cache, then it is returned immediately without making a request to the database.
        /// Otherwise, a query is made to the database for the entity, if found, is attached to the context and returned.
        /// The navigation property to be included is specified starting with the type of entity being queried (TEntity).
        /// If you wish to include additional types based on the navigation properties of the type being included, then chain a call with comma separated.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="navigationProperties">A string representing the navigation property to be included ("Property1").</param>
        /// <returns>The entity found that contains elements from the input sequence that satisfy the condition specified by predicate predicate, or null.</returns>
        TEntity Select(Expression<Func<TEntity, bool>> predicate, params string[] navigationProperties);

        /// <summary>
        /// Finds an entity with the given primary key values. If an entity with the given primary key values is exists in cache, then it is returned immediately without making a request to the database.
        /// Otherwise, a query is made to the database for an entity with the given primary key values and this entity, if found, is attached to the context and returned.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <param name="keyValue">The values of the primary key for the entity to be found.</param>
        /// <returns>The entity found, or null.</returns>
        TEntity Select(object keyValue);

        /// <summary>
        /// Begins tracking the given entity, and any other reachable entities that are not already being tracked,
        /// in the EntityState.Added state such that they will be inserted into the database when SaveChanges is called.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <returns>Returns the entity being tracked by this entry.</returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Begins tracking the given entities, and any other reachable entities that are not already being tracked,
        /// in the EntityState.Added state such that they will be inserted into the database when SaveChanges is called.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        /// <returns>Returns the entities being tracked by this entry.</returns>
        IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Begins tracking the given entity in the EntityState.Modified state such that it will be updated in the database when SaveChanges is called.
        /// A recursive search of the navigation properties will be performed to find reachable entities that are not already exists in cache.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>Returns the entity being tracked by this entry.</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Begins tracking the given entities in the EntityState.Modified state such that they will be updated in the database when SaveChanges is called.
        /// A recursive search of the navigation properties will be performed to find reachable entities that are not already exists in cache.
        /// </summary>
        /// <param name="entities">The entities to update.</param>
        /// <returns>Returns the entities being tracked by this entry.</returns>
        IEnumerable<TEntity> Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Begins tracking the given entity in the EntityState.Deleted state such that it will be removed from the database when SaveChanges is called.
        /// </summary>
        /// <param name="keyValue">The values of the primary key for the entity to be found.</param>
        void Delete(object keyValue);

        /// <summary>
        /// Begins tracking the given entity in the EntityState.Deleted state such that it will be removed from the database when SaveChanges is called.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Begins tracking the given entity in the EntityState.Deleted state such that they will be removed from the database when SaveChanges is called.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        int SaveChanges();
    }
}