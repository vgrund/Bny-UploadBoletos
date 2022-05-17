namespace Bny.UploadBoletos.SharedKernel.Interfaces
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        void AddRange(ICollection<T> entity);
        Task AddRangeAsync(ICollection<T> entity, CancellationToken cancellationToken = default);
        Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken = default);
        void DeleteAll();
    }
}
