using Microsoft.EntityFrameworkCore;
using Sentinel.Identity.Domain.Repositories;
using Sentinel.Identity.Infrastructure.Persistence;
using Sentinel.Identity.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Sentinel.Identity.Infrastructure")
    );
});

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssemblyContaining<Sentinel.Identity.Application.AssemblyReference>());

builder.Services.AddAutoMapper(cfg => { }, typeof(Sentinel.Identity.Application.AssemblyReference));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();