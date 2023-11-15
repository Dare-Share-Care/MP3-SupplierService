using Ardalis.Specification;
using Suppliers.Web.Entities;


namespace Suppliers.Web.Specifications;

public sealed class RestockByStatusSpec : Specification<Supply>
{
   
    public RestockByStatusSpec()
    {
        Query.Where(p => p.Status != DeliveryStatus.Delivered);

    }

}



