using Microsoft.AspNetCore.Mvc;
using Suppliers.Web.Interfaces.DomainServices;

namespace Suppliers.Web.Controller;

[ApiController]
[Route("api/[controller]")]
public class SupplierController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SupplierController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetProductsAsync()
    {
        var products = await _supplierService.GetProducts();
        return Ok(products);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductByIdAsync(int id)
    {
        var product = await _supplierService.GetProductById(id);
        return Ok(product);
    }
}