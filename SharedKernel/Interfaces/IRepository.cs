namespace SharedKernel.Interfaces;
public interface IRepository<T> where T : IAggregateRoot
{
    //Task<T> GetById(int id);
    IUnitOfWork UnitOfWork { get; }
}
