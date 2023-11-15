using Suppliers.Web.Model.Dto;

namespace Suppliers.Web.Interfaces.DomainServices;

public interface ISupplierService
{
    
    Task CreateSupplyAsync(SupplyDto supplies);
    
    Task<RestockSuppliesDto> SendSuppliesToInv();
}