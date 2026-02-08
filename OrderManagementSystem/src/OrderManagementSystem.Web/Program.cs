using OrderManagementSystem.Core.Events;
using OrderManagementSystem.Infrastructure.DependencyInjection;
using OrderManagementSystem.Modules.Catalog.Api.DependencyInjection;
using OrderManagementSystem.Modules.Inventory.Api.DependencyInjection;
using OrderManagementSystem.Modules.Inventory.Infrastructure.EventHandlers;
using OrderManagementSystem.Modules.Orders.Api.DependencyInjection;
using OrderManagementSystem.Modules.Orders.Core.Events;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration).ReadFrom.Services(services));

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddInfrastructure();
builder.Services.AddCatalogModule(builder.Configuration);
builder.Services.AddOrdersModule(builder.Configuration);
builder.Services.AddInventoryModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<OrderPlacedEvent, OrderPlacedEventHandler>();

app.Run();
