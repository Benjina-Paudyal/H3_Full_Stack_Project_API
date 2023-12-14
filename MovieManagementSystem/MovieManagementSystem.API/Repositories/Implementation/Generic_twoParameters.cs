using MovieManagementSystem.API.Data;
using MovieManagementSystem.API.Repositories.Interfaces.Generic_twoParameters;

namespace MovieManagementSystem.API.Repositories.Implementation
{
    public class Generic_twoParameters<T1Entity, T2Entity> : IGenericParam<T1Entity, T2Entity> where T1Entity : class where T2Entity : class
    {
        private readonly MovieDbContext _db;
        public Generic_twoParameters(MovieDbContext db)
        {
            _db = db;
        }

        //Methods for operations on the first entity 
        public void create(T1Entity entity)
        {
            _db.Set<T1Entity>().Add(entity);
            _db.SaveChanges();
        }

        public void create(T2Entity entity)
        {
            _db.Set<T2Entity>().Add(entity);
            _db.SaveChanges();
        }

        public void Delete(T1Entity entity)
        {
            _db.Set<T1Entity>().Remove(entity);
            _db.SaveChanges();
        }

        public void Delete(T2Entity entity)
        {

            _db.Set<T2Entity>().Remove(entity);
            _db.SaveChanges();
        }

        public IEnumerable<T2Entity> GetAll()
        {
            return _db.Set<T2Entity>().ToList();
        }

        public IEnumerable<T1Entity> GetAllEntities()
        {
            return _db.Set<T1Entity>().ToList();
        }

        public T2Entity GetEntity(int id)
        {
            return _db.Set<T2Entity>().Find(id);
        }

        public T1Entity GetEntityByID(int id)
        {
            return _db.Set<T1Entity>().Find(id);
        }

        public void UpdateEntity(T1Entity entity)
        {
            _db.Set<T1Entity>().Update(entity);
            _db.SaveChanges();
        }

        public void UpdateEntity(T2Entity entity)
        {
            _db.Set<T2Entity>().Update(entity);
            _db.SaveChanges();
        }
    }
}
