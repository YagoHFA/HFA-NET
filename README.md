# HFA Tools

A library of generic resources commonly used in software development.

Features:

- Repository (Generic CRUD operations using DbContext, simplifying data access logic)
- Auth (Provides models for authentication and authorization of resource access)
- Unit of works (Implements the Unit of Work pattern to coordinate repository operations within a secure transactional boundary)

## Repository

A generic repository for common SQL operations using Entity Framework Core.

This repository provides support for:

- CRUD operations
- Custom deletes
- Querying entities with predicates
- Counting and existence checks

## Class Diagram (Repository)

```mermaid 
classDiagram 
  
class ICrudRepository{
    <<interface>>
    +Add(entity: TEntity)void
    +Delete(entity: TEntity)void
    +Update(entity: TEntity)void
    +FIndById(keyvalues: object[]) TEntity?
} 

class IDeleteRepository{
    <<interface>>
    +DeleteMany(entities: IQueryable<TEntity>)void
    +DeleteWhere(predicate: Expression<Func<TEntity, bool>>)
    +DeleteByID(id: object)void
}

class IFindRepository{
    <<interface>>
    +Find(predicate: Expression<Func<TEntity, bool>>) Entity?
    +FindAll()IQueryable<TEntity>?
    +FindAll(predicate: Expression<Func<TEntity, bool>>)IQueryable<TEntity>?
    +FindAll(predicate: Expression<Func<TEntity, bool>>, orderBy: Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>>?)IQueryable<TEntity>?
}

class IRepository{
    <<interface>>
    +AddMany(IQueryable<TEntity>)void
    +Any(predicate: Expression<Func<TEntity, bool>>)bool
    +Exists()bool
    +Count(predicate: Expression<Func<TEntity, bool>>)int
}

class Repository{
    #DbContect context
    #DBSet<TEntiry> dbSet
    +Repository(dbContext : DBContext)
}


ICrudRepository   <|.. IRepository
IDeleteRepository <|.. IRepository
IFindRepository   <|.. IRepository
IRepository       <|.. Repository
```

## Auth

## Class Diagram (Auth)