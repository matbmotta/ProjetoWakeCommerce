using Microsoft.EntityFrameworkCore;
using ProjetoWakeCommerce.Application.Interfaces;
using ProjetoWakeCommerce.Application.Services;
using ProjetoWakeCommerce.Repositorio.Interfaces;
using ProjetoWakeCommerce.Repositorio.Repositorios;
using WakeCommerce.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProdutosService, ProdutosService>();
builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();

//builder.Services.AddScoped<ITeste>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
