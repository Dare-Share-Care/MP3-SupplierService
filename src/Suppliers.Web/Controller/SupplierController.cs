using Microsoft.AspNetCore.Mvc;
using Suppliers.Web.Interfaces.DomainServices;
using Suppliers.Web.Model.Dto;

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

    [HttpPost("ConsumeSupply")]
    public IActionResult ConsumeSupply([FromBody] SupplyDto supply)
    {
        if (supply == null)
        {
            return BadRequest("Invalid supply data.");
        }

        try
        {
            // Call the service to consume the supply
            _supplierService.CreateSupplyAsync(supply);

            // Optionally, return a success response
            return Ok("Supply consumption initiated successfully.");
        }
        catch (Exception ex)
        {
            // Log the exception or handle it based on your application requirements
            return StatusCode(500, $"Error consuming supply: {ex.Message}");
        }
    }




    [HttpPost("SendSupply")]
    public async Task<IActionResult> SendSupply([FromBody] RestockSuppliesDto supply)
    {

        // Call the service to consume the supply
        var fisk= await _supplierService.SendSuppliesToInv(supply);
        return Ok(fisk);

    }
    
}