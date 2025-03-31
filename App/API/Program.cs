using API.Configurations;
using API.Filters;
using API.Helpers;
using API.Middlewares;
using LibreriaVirtualData.Library.Context;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidacionModelStateFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibreriaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbLibreria"));
});


builder.Services.AddScoped<IManejarRespuestaDeErrorService, ManejarRespuestaDeErrorService>();
builder.Services.AddScoped<ManejadorExcepcionesMiddleware>();
builder.Services.AddConfigurations(builder);
builder.Services.AddPersistencia();
builder.Services.AddLogica();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<LibreriaContext>();
        context.Database.Migrate();
        //Agregar Datos de prueba
        DatosDePrueba.SeedDatabase(context);
    }


    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ManejadorExcepcionesMiddleware>();

app.MapControllers();

app.Run();
