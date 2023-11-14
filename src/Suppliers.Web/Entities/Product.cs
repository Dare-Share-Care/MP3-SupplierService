namespace Suppliers.Web.Entities;

public class Product : EntityBase
{
    public string product { get; set; }
    public long quantity { get; set; }
}