namespace Visions.Services.Contracts
{
    public interface IDeleteService<T>
        where T : class
    {
        void Delete(T item);
    }
}
