using Microsoft.EntityFrameworkCore;
using Suppliers.Web.Consumer;
using Suppliers.Web.Data;
using Suppliers.Web.Interfaces.DomainServices;
using Suppliers.Web.Interfaces.Repositories;
using Suppliers.Web.Producer;
using Suppliers.Web.Service;

const string policyName = "AllowOrigin";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
        builder =>
        {
            builder
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});


//DBContext
builder.Services.AddDbContext<SupplierContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"));
});

//Build services

//Domain Services
builder.Services.AddScoped<ISupplierService, SupplierService>();

//Background services

// build kafka producer
builder.Services.AddSingleton<SupplyProducer>();

//builder.Services.AddSingleton<RequestSupplies>();

//kafka consumer
builder.Services.AddHostedService<SupplyConsumer>();

//Repositories
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(policyName);
app.UseAuthorization();

app.MapControllers();


app.Run();