namespace Bny.UploadBoletos.SharedKernel.Interfaces
{
    public interface IRepository<T>: IAggregateRoot where T : class
    {
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
