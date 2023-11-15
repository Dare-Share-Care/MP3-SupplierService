using Ardalis.Specification;
using Suppliers.Web.Entities;


namespace Suppliers.Web.Specifications;

public class RestockByStatusSpec : Specification<Supply>
{
   
    // Get all supplies that have not been delivered to a List
    public RestockByStatusSpec()
    {
        Query.Where(p => p.Status != DeliveryStatus.Delivered);
    }
    
   

}



