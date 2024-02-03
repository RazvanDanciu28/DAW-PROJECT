using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AngularApp1.Server.DataContext;
using AngularApp1.Server.Models;
using AngularApp1.Server.Services.UnitOfWorkService;
//++ services





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
    options.UseSqlServer("dbConnection"));

builder.Services.AddIdentity<AppUser, IdentityRole>(options => options.User.RequireUniqueEmail = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

///TO ADD SERVICES
builder.Services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();


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
