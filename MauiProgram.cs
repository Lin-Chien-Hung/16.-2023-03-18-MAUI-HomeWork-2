using MauiApp1.ViewModel;
// 相機使用套件
using CommunityToolkit.Maui; 
// CommunityToolkit.Maui.Camera
using Microsoft.Extensions.Logging;

namespace MauiApp1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            // 1. 定義好需要那些功能
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit()        // For using CommunityToolkit features
            .UseMauiCommunityToolkitCamera();   // For camera usage

            // ===========================================================================
            // 2. 讓這些畫面分別繼承上述註冊好的功能

            // AddSingleton：全域唯一實例
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();
            
            // AddTransient：每次都新建實例
            builder.Services.AddTransient<DetailPage>();
            builder.Services.AddTransient<DetailViewModel>();

            // ===========================================================================
#if DEBUG
            //builder.Logging.AddDebug();
#endif
            // ===========================================================================
            return builder.Build();
        }
    }
}