using Microsoft.EntityFrameworkCore;
using Suppliers.Web.Entities;

namespace Suppliers.Web.Data;

public class SupplierContext : DbContext
{ 
    public DbSet<Supply> Supplies {get; set;} = null!;
    
    public SupplierContext(DbContextOptions<SupplierContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Set primary keys
        modelBuilder.Entity<Supply>().HasKey(e => e.Id);
    }
}