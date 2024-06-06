using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using FitnessApp.Data;
using FitnessApp.Views;
using FitnessApp.Services;
using FitnessApp.ViewModel;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace FitnessApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseSkiaSharp(true)
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<LocalDBService>();
            builder.Services.AddSingleton<JsonCalories>();
            builder.Services.AddSingleton<FoodViewModel>();
            builder.Services.AddTransient<MainPage>();
            return builder.Build();
        }
    }
}
