using System.Text.Json;
using Confluent.Kafka;
using Suppliers.Web.Data;
using Suppliers.Web.Entities;
using Suppliers.Web.Interfaces.DomainServices;
using Suppliers.Web.Interfaces.Repositories;
using Suppliers.Web.Model.Dto;
using Suppliers.Web.Specifications;

namespace Suppliers.Web.Service;

public class SupplierService : ISupplierService
{
    private readonly IRepository<Supply> _supplyRepository;
    private readonly IReadRepository<Supply> _productReadRepository;
    
    public SupplierService(IRepository<Supply> supplyRepository, IReadRepository<Supply> productReadRepository)
    {
        _supplyRepository = supplyRepository;
        _productReadRepository = productReadRepository;
    }

    public async Task CreateSupplyAsync(SupplyDto supply)
    {
        var newSupply = new Supply
        {
            Name = supply.Name,
            Quantity = supply.Quantity
        };

        // Save the supply to the database
        await _supplyRepository.AddAsync(newSupply);

        // Save changes to the database
        await _supplyRepository.SaveChangesAsync();
    }

   //public async Task<SupplyDto> GetSupply(int id)
   //{
   //    var supply = await _supplyRepository.GetByIdAsync(id);

   //    if (supply != null)
   //    {
   //        return new SupplyDto
   //        {
   //            Id = supply.Id,
   //            Name = supply.Name,
   //            Quantity = supply.Quantity
   //        };
   //    }

   //    return null; // or throw an exception, handle as needed
    }
    
