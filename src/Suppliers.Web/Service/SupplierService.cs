using Suppliers.Web.Entities;
using Suppliers.Web.Interfaces.DomainServices;
using Suppliers.Web.Interfaces.Repositories;
using Suppliers.Web.Model.Dto;
using Suppliers.Web.Producer;
using Suppliers.Web.Specifications;
using DeliveryStatus = Suppliers.Web.Entities.DeliveryStatus;

namespace Suppliers.Web.Service;

public class SupplierService : ISupplierService
{
    private readonly IRepository<Supply> _supplyRepository;
    private readonly IReadRepository<Supply> _productReadRepository;
    private readonly SupplyProducer _kafkaProducer = new();

    public SupplierService(IRepository<Supply> supplyRepository, IReadRepository<Supply> productReadRepository, SupplyProducer kafkaProducer)
    {
        _supplyRepository = supplyRepository;
        _productReadRepository = productReadRepository;
        _kafkaProducer = kafkaProducer;
    }

    //Method gets a request sipply from inventory and stores it in the DB as not delivered
    public async Task CreateSupplyAsync(SupplyDto supply)
    {
        var newSupply = new Supply
        {
            Name = supply.Name,
            Quantity = supply.Quantity,
            Status = DeliveryStatus.NotDelivered
        };

        // Save the supply to the database
        await _supplyRepository.AddAsync(newSupply);

        // Save changes to the database
        await _supplyRepository.SaveChangesAsync();
    }


    //Method produce a msg back to inventory that the supply has been delivered
    public async Task<RestockSuppliesDto> SendSuppliesToInv()
    {
        var spec = new RestockByStatusSpec();
        var supplies = await _productReadRepository.ListAsync(spec);

        // Assuming RestockSuppliesDto does not need an Id property
        // If it does, you need to determine how to set that Id
        var supplyDtoResult = new RestockSuppliesDto
        {
            // If you have an Id for RestockSuppliesDto, set it here
            // Id = ???,
            Supplies = supplies.Select(supply => new SupplyDto
            {
                Id = supply.Id,
                Name = supply.Name,
                Quantity = supply.Quantity,
                Status = (Model.Dto.DeliveryStatus)supply.Status // Assuming you have the same enum values in both places
            }).ToList()
        };

        // Send order created event
        await SendSuppliesEventAsync(supplyDtoResult);

        // Return order
        return supplyDtoResult;
    }
    


    private async Task SendSuppliesEventAsync(RestockSuppliesDto supplies)
    {
        await _kafkaProducer.ProduceAsync("mp3-restock", supplies);
    }
    
    }
    