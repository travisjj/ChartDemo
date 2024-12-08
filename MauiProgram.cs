using ChartDemo.Services;
using ChartDemo.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace ChartDemo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMudServices();
            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddScoped(typeof(IDataLoader<>), typeof(DataLoader<>));
            builder.Services.AddScoped(typeof(IDataSaver<>), typeof(DataSaver<>));
            builder.Services.AddSingleton<IBookmark,Bookmark>();
            builder.Services.AddSingleton<ChartDataVM>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
