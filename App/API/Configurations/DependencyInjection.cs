using API.Helpers;
using LibreriaVirtual.Logica.Services;
using LibreriaVirtualData.Library.Context;
using LibreriaVirtualData.Library.Data;
using LibreriaVirtualData.Library.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using Shared.Library.Mensajes.Mensajes;

namespace API.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistencia(this IServiceCollection services)
        {
            services.AddDbContext<LibreriaContext>();
            services.AddScoped<IAutorData, AutorData>();
            services.AddScoped<ILibroData, LibroData>();
            services.AddScoped<IReviewData, ReviewData>();
            services.AddScoped<IUsuarioData, UsuarioData>();
            services.AddScoped<IDataHelper, DataHelper>();

            return services;
        }

        public static IServiceCollection AddLogica(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAutorService, AutorService>();
            services.AddScoped<ILibroService, LibroService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddSingleton<IMensajesService, MensajesService>();
            services.AddSingleton<IEmailSender, EmailSender>();

            return services;
        }

        public static IServiceCollection AddConfigurations(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.Configure<MensajesConfiguracion>(builder.Configuration.GetSection("Mensajes"));

            return services;
        }
    }
}
