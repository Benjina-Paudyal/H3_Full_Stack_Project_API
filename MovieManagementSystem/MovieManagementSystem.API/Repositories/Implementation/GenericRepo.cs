using MovieManagementSystem.API.Data;
using MovieManagementSystem.API.Repositories.Interfaces;

namespace MovieManagementSystem.API.Repositories.Implementation
{
    public class GenericRepo<T> : IGeneric<T> where T : class
    {
        private readonly MovieDbContext _db;  

        public GenericRepo(MovieDbContext db) // injecting database (DI)
        {
            _db = db;
        }

        /*
          Set<T>: is a method provided by the DbContext class that allows us to access and perform operations on a specific 
            entity type 'T'. The Set<T> method returns a DbSet<T> object, which represents the collection of entitties for the
            given type in the context of a database.
            entities: objects representing database tables

        DbSet<T>: generic class that represents a collection of entitites of type 'T' that can be queried and manipulated.
         
        */
        public void Create(T entity)
        {
            try
            {
                _db.Set<T>().Add(entity);// using Set<T> to access entities of type T.
                _db.SaveChanges();

            }
            catch (Exception ex)

            {
                throw new Exception("Error occured while using saving changes ", ex);

            }

        }

        public void Delete(int id)
        {
            var entityToDelete = _db.Set<T>().Find(id);
            if (entityToDelete != null)
            {
                _db.Set<T>().Remove(entityToDelete);
                _db.SaveChanges();
            }

            //_db.Set<T>().Remove(entity);
            //_db.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public T GetbyId(int id)
        {
            return _db.Set<T>().Find(id);
            
        }

        /*
         EntityEntry : its a class which is used to represent an entity being tracked by the  Entity Framework's change 
         tracker.It provides a way to interact with the state and values of the entity, and its commonly used for tasks 
         such as updating, attaching or detaching entities.

        Entry(existingEntity) : Entry() method which returns an EntityEntry object for the given entity. EntityEntry object
        represents the state of the entity within the context of Entity Framework's change tracker.
         
         */
        public void Update(int id, T entity)
        {
            var existingEntity = _db.Set<T>().Find(id); // retrieves the existing entity based on the primary key
            if (existingEntity != null)
            {
                _db.Entry(existingEntity).CurrentValues.SetValues(entity); // updates the property values(column of a table) from the provided entity
                _db.SaveChanges();
            }
        }

    }
}
