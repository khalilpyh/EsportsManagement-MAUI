/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using Microsoft.Extensions.Logging;
using ypeng_EsportsManagementMAUI.ViewModels;
using ypeng_EsportsManagementMAUI.Views;

namespace ypeng_EsportsManagementMAUI;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		//configure DI
		builder.Services.AddSingleton<GameViewModel>();
		builder.Services.AddTransient<TeamViewModel>();
		builder.Services.AddTransient<PlayerViewModel>();
		builder.Services.AddTransient<PlayerDetailViewModel>();

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<TeamPage>();
		builder.Services.AddTransient<PlayerPage>();
		builder.Services.AddTransient<PlayerDetailPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
