namespace MovieManagementSystem.API.Repositories.Interfaces.Generic_twoParameters
{
    public interface IGenericParam<T1Entity, T2Entity> where T1Entity : class where T2Entity : class
    {
        //Methods on the first entity
        public void create(T1Entity entity);
        public void Delete(T1Entity entity);
        public IEnumerable<T1Entity> GetAllEntities();
        public T1Entity GetEntityByID(int id);
        public void UpdateEntity(T1Entity entity);

        //Methods on the second entity 

        public void create(T2Entity entity);
        public void Delete(T2Entity entity);
        public IEnumerable<T2Entity> GetAll();
        public T2Entity GetEntity(int id);
        public void UpdateEntity(T2Entity entity);

    }
}
