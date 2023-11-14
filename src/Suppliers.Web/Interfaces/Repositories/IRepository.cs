using Ardalis.Specification;

namespace Suppliers.Web.Interfaces.Repositories;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
    
}