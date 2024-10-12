using CommunityToolkit.Maui;
using IoTControlCenter.ViewModels;
using Microsoft.Extensions.Logging;
using Shared.Handlers;

namespace IoTControlCenter
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				});

			builder.Services.AddMauiBlazorWebView();

#if DEBUG
			builder.Services.AddBlazorWebViewDeveloperTools();
			builder.Logging.AddDebug();
#endif

			builder.Services.AddTransient<HomeViewModel>();
			builder.Services.AddTransient<SettingsViewModel>();
			builder.Services.AddTransient<IotHubHandler>();



			return builder.Build();
		}
	}
}
