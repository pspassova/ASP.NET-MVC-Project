namespace Visions.Services.Contracts
{
    public interface IUploadService<T> 
        where T : class
    {
        void Upload(T item);
    }
}
