namespace Suppliers.Web.Model.Dto;

public enum DeliveryStatus
{
    NotDelivered,
    Delivered
}

public class SupplyDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
        
    // Use the DeliveryStatus enum for the Status property
    public DeliveryStatus Status { get; set; }
}