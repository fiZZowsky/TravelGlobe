using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Handlers;
using TravelGlobe.Application;
using TravelGlobe.Infrastructure.Persistance;
using TravelGlobe.Infrastructure.Seeders;

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
                })
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddHandler<SearchBar, NoIconSearchBarHandler>();
#if ANDROID || IOS
                    handlers.AddHandler<Shell, RoundedFloatingTabbarHandler>();
#endif
#if ANDROID
                    handlers.AddHandler<WebView, WebViewHandler>();
                    WebViewHandler.Mapper.AppendToMapping(nameof(LocalAssetWebViewClient), (handler, view) =>
                    {
                        if (handler.PlatformView is Android.Webkit.WebView nativeWebView)
                        {
                            nativeWebView.SetWebViewClient(new LocalAssetWebViewClient(nativeWebView.Context));
                        }
                    });
#endif
                });

#if DEBUG
                    builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<TravelGlobeDbContext>();
                db.Database.EnsureCreated();
                DatabaseSeeder.SeedAsync(db).GetAwaiter().GetResult();
            }

            return app;
        }
    }
}