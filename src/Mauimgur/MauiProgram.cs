using Drastic.Services;
using Mauimgur.Services;
using Microsoft.Extensions.Logging;

namespace Mauimgur;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		Microsoft.Maui.Handlers.ButtonHandler.Mapper.AppendToMapping("CatalystButton", (handler, view) =>
		{
#if MACCATALYST
#pragma warning disable CA1416 // プラットフォームの互換性を検証
            handler.PlatformView.PreferredBehavioralStyle = UIKit.UIBehavioralStyle.Pad;
#pragma warning restore CA1416 // プラットフォームの互換性を検証
#endif
        });
		var builder = MauiApp.CreateBuilder();

		builder.Services!
	   .AddSingleton<IErrorHandlerService, ErrorHandlerService>()
	   .AddSingleton<IAppDispatcher, AppDispatcherService>();

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

        return builder.Build();
	}
}
