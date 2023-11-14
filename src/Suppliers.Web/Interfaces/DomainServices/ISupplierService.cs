using Suppliers.Web.Model.Dto;

namespace Suppliers.Web.Interfaces.DomainServices;

public interface ISupplierService
{
    Task<List<ProductDto>> GetProducts();

    Task<ProductDto> GetProductById(int id);
}