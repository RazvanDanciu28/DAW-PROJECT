using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AngularApp1.Server.DataContext;
using AngularApp1.Server.Models;
using AngularApp1.Server.Services.UnitOfWorkService;
using AngularApp1.Server.Services.ProductService;
using AngularApp1.Server.Services.OrderItemService;
using AngularApp1.Server.Services.OrderService;





var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("myAppCors", policy =>
    {
        policy.AllowAnyOrigin() 
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var appConnectionString = builder.Configuration.GetConnectionString("dBConnection") ?? throw new InvalidOperationException("Connection string not found");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("dBConnection"));

builder.Services.AddIdentity<AppUser, IdentityRole>(options => options.User.RequireUniqueEmail = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IOrderItemService, OrderItemService>();


builder.Services.AddTransient<SeedData>();

var app = builder.Build();
app.UseCors("myAppCors");



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

using (var serviceScope = app.Services.CreateScope())
{
    var seeder = serviceScope.ServiceProvider.GetRequiredService<SeedData>();
    seeder.Initialize();
}

app.Run();
