using API.Configurations;
using API.Helpers;
using API.Middlewares;
using LibreriaVirtualData.Library.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Library.Mensajes.Mensajes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibreriaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbLibreria"));
});

builder.Services.Configure<MensajesConfiguracion>(builder.Configuration.GetSection("Mensajes"));
builder.Services.AddTransient<IManejarRespuestaDeErrorService, ManejarRespuestaDeErrorService>();
builder.Services.AddTransient<ManejadorExcepcionesMiddleware>();
builder.Services.AddPersistencia();
builder.Services.AddLogica();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LibreriaContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ManejadorExcepcionesMiddleware>();

app.MapControllers();

app.Run();
