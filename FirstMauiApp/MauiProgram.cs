using CommunityToolkit.Maui;
using FirstMauiApp.Data;
using FirstMauiApp.ViewModels;
using Syncfusion.Maui.Core.Hosting;

namespace FirstMauiApp;

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
		builder.UseMauiCommunityToolkit();


        builder.Services.AddSingleton<SimsFoodViewModel>();
		builder.Services.AddSingleton<RecipesDatabase>();

        builder.ConfigureSyncfusionCore();

        return builder.Build();
	}
}
