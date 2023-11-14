using Suppliers.Web.Entities;
using Suppliers.Web.Interfaces.DomainServices;
using Suppliers.Web.Interfaces.Repositories;
using Suppliers.Web.Model.Dto;
using Suppliers.Web.Specifications;

namespace Suppliers.Web.Service;

public class SupplierService : ISupplierService
{
    
    private readonly IReadRepository<Product> _productReadRepository;

    public SupplierService(IReadRepository<Product> productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<List<ProductDto>> GetProducts()
    {
        var products = await _productReadRepository.ListAsync();
        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            product = p.product,
            quantity = p.quantity
        }).ToList();
    }
    
    
    public async Task<ProductDto> GetProductById(int id)
    {
        var product = await _productReadRepository.FirstOrDefaultAsync(new ProductByIdSpec(id));
        return new ProductDto
        {
            Id = product.Id,
            product = product.product,
            quantity = product.quantity
        };
    }
    
}