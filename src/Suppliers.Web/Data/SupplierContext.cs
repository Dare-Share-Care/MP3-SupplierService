using Microsoft.EntityFrameworkCore;
using Suppliers.Web.Entities;

namespace Suppliers.Web.Data;

public class SupplierContext : DbContext
{ 
    public DbSet<Product> Products {get; set;} = null!;
    
    public SupplierContext(DbContextOptions<SupplierContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Set primary keys
        modelBuilder.Entity<Product>().HasKey(e => e.Id);
    }
}