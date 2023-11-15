namespace Suppliers.Web.Model.Dto;

public class RestockSuppliesDto
{
    public int Id { get; set; }
    
    public List<SupplyDto> Supplies { get; set; } = new();
}