using API.Helpers;
using LibreriaVirtual.Logica.Services;
using LibreriaVirtualData.Library.Context;
using LibreriaVirtualData.Library.Data;
using LibreriaVirtualData.Library.Data.Helpers;
using Shared.Library.Mensajes.Mensajes;

namespace API.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistencia(this IServiceCollection services)
        {
            services.AddDbContext<LibreriaContext>();
            services.AddTransient<IAutorData, AutorData>();
            services.AddTransient<ILibroData, LibroData>();
            services.AddTransient<IReviewData, ReviewData>();
            services.AddTransient<IUsuarioData, UsuarioData>();
            services.AddTransient<IDataHelper, DataHelper>();

            return services;
        }

        public static IServiceCollection AddLogica(this IServiceCollection services)
        {
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IAutorService, AutorService>();
            services.AddTransient<ILibroService, LibroService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddSingleton<IMensajesService, MensajesService>();
            services.AddSingleton<IEmailSender, EmailSender>();

            return services;
        }
    }
}
