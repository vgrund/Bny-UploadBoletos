namespace Bny.UploadBoletos.SharedKernel.Interfaces
{
    public interface IRepository<T>: IAggregateRoot where T : class
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    }
}
