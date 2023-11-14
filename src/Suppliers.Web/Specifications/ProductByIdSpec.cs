using Ardalis.Specification;
using Suppliers.Web.Entities;

namespace Suppliers.Web.Specifications;

public sealed class ProductByIdSpec : Specification<Supply>
{
    public ProductByIdSpec(int id)
    {
        Query.Where(p => p.Id == id);
    }
}