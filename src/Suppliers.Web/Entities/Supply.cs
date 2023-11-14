namespace Suppliers.Web.Entities;

public class Supply
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
}