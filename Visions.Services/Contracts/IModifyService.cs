namespace Visions.Services.Contracts
{
    public interface IModifyService<T> 
        where T : class
    {
        void Edit(T item);
    }
}
