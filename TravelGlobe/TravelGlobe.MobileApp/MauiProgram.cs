using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Storage;
using System.IO;
using TravelGlobe.Application;
using TravelGlobe.Infrastructure.Persistance;

namespace TravelGlobe.MobileApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "travelglobe.db");
            builder.Configuration["ConnectionStrings:TravelGlobeSqlite"] = $"Data Source={dbPath}";

            builder.Services.AddApplication(builder.Configuration);
            builder.Services.AddMobile();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<TravelGlobeDbContext>();
                db.Database.EnsureCreated();
            }

            return app;
        }
    }
}