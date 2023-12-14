using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.API.Data;
using MovieManagementSystem.API.Data.Domain;
using MovieManagementSystem.API.Repositories.Interfaces;
using System.Linq.Expressions;

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

           
        }

        

        public IEnumerable<T> FindEntities(Func<T, bool> predicate)
        {
            return _db.Set<T>().Where(predicate).ToList();
        }

        public IEnumerable<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public IEnumerable<object> GetAllData()
        {
            var query = from actor in _db.Set<Actor>()
                        join movieActor in _db.Set<MovieActor>() on actor.ActorId equals movieActor.ActorId into actorMovies
                        join award in _db.Set<Award>() on actor.ActorId equals award.MovieId into actorAwards
                        join movie in _db.Set<Movie>() on actor.ActorId equals movie.DirectorId into actorMovieDirectors
                        //join genre in _db.Set<Genre>() on actor.Id equals genre.MoviesGenre.FirstOrDefault().MoviesId into actorGenres 
                        join country in _db.Set<Country>() on actor.ActorId equals country.MoviesProduced.FirstOrDefault().MovieId into actorProducedMovies
                        join review in _db.Set<Review>() on actor.ActorId equals review.Movie.MovieId into actorReviews
                        join language in _db.Set<Language>() on actor.ActorId equals language.Movies.FirstOrDefault().MovieId into actorLanguages

                        select new
                        {
                            Actor = actor,
                            Movies = actorMovies,
                            Awards = actorAwards,
                            MovieDirectors = actorMovieDirectors,
                            ProducedMovies = actorProducedMovies,
                            Reviews = actorReviews,
                            Language = actorLanguages
                        };

            return query.ToList();
        }

        public T GetbyId(int id)
        {
            return _db.Set<T>().Find(id);
            
        }



        public IEnumerable<T> GetEntitiesWithIncludes(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _db.Set<T>().AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.Where(predicate).ToList();
        }

        
        public void Update(int id, T entity)
        {
            var existingEntity = _db.Set<T>().Find(id);
            if (existingEntity != null)
            {
                _db.Entry(existingEntity).CurrentValues.SetValues(entity);
                _db.SaveChanges();
            }
        }
            /*
             EntityEntry : its a class which is used to represent an entity being tracked by the  Entity Framework's change 
             tracker.It provides a way to interact with the state and values of the entity, and its commonly used for tasks 
             such as updating, attaching or detaching entities.

            Entry(existingEntity) : Entry() method which returns an EntityEntry object for the given entity. EntityEntry object
            represents the state of the entity within the context of Entity Framework's change tracker.

             */
          

    }
}
