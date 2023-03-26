using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using MsBairro.MsContext;
using MsBairro.Repositorys.Interfaces;
using MsBairro.Repositorys.Repository;
using MsBairro.Services.Interfaces;
using MsBairro.Services.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MsBairroContext>(opt => opt.UseNpgsql(connectionString));

builder.Services.AddScoped<IRepositoryBairro, RepositoryBairro>();
builder.Services.AddScoped<IBairroService, BairroService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Mypolicy",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000")
                            .WithHeaders(
                                    HeaderNames.ContentType,
                                    HeaderNames.Authorization)
                            .AllowAnyMethod()
                            .AllowCredentials();
                      });
});

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
