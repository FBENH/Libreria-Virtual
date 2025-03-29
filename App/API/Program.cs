using API.Middlewares;
using API.Services;
using LibreriaVirtualData.Library.Context;
using LibreriaVirtualData.Library.Data;
using LibreriaVirtualData.Library.Data.Helpers;
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
builder.Services.AddSingleton<IMensajesService, MensajesService>();

builder.Services.AddTransient<IUsuarioData, UsuarioData>();
builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddTransient<IAutorData, AutorData>();
builder.Services.AddTransient<IAutorService, AutorService>();
builder.Services.AddTransient<ILibroService, LibroService>();
builder.Services.AddTransient<ILibroData, LibroData>();
builder.Services.AddTransient<IDataHelper, DataHelper>();
builder.Services.AddTransient<ManejadorExcepcionesMiddleware>();
builder.Services.AddTransient<IManejarRespuestaDeErrorService, ManejarRespuestaDeErrorService>();
builder.Services.AddTransient<IReviewData, ReviewData>();
builder.Services.AddTransient<IReviewService, ReviewService>();

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
