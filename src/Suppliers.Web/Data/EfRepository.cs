using Ardalis.Specification.EntityFrameworkCore;
using Suppliers.Web.Interfaces.Repositories;

namespace Suppliers.Web.Data;

public class EfRepository<T> : RepositoryBase<T>, IRepository<T>, IReadRepository<T> where T : class
{
    public readonly SupplierContext _dbContext;
    
    public EfRepository(SupplierContext dbContext) : base(dbContext) 
    {
        _dbContext = dbContext;
    }
    
   
}