using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace ChatAi
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
                    // Подключить шрифты
                });

            return builder.Build();
        }
    }
}
