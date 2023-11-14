using Ardalis.Specification;

namespace Suppliers.Web.Interfaces.Repositories;

public interface IReadRepository<T> : IRepositoryBase<T> where T : class
{
    
}