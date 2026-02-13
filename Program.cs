using Microsoft.EntityFrameworkCore;
using VHBurguer3.Applications.Sercives;
using VHBurguer3.Contexts;
using VHBurguer3.Interfaces;
using VHBurguer3.Repositoreis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//chamar nossa conexão com o banco aqui na program
builder.Services.AddDbContext<VH_BurguerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

// Usuário
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();

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
