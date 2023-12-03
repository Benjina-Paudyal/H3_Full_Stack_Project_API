namespace MovieManagementSystem.API.Repositories.Interfaces
{
    /*  
        The IGeneric<T> interface provides a generic abstraction(it's like having a magical universal tool that we can use
        for different tasks without worrying about the details) for common data operations that can be implemented by various
        data repositories.
        "T: class" constraint ensures that the type parameter 'T' must be a reference type (class)
        Implementing classes or repositories that implement this interface will defie the specific behaviour for each mehtod 
        according to the requirements of the data storage mechanism( e.g. database, file system).
     */
    public interface IGeneric<T> where T : class 
    {
        IEnumerable<T> GetAll(); // retrieves all entities of type T
        T GetbyId(int id); 

        void Create(T entity); // instance of T as a parameter

        void Update(int id, T entity); 

        void Delete(int id);
    }
}
