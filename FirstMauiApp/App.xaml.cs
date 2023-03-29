using FirstMauiApp.Data;

namespace FirstMauiApp;

public partial class App : Application
{
	public static RecipesDatabase Database { get; } = new ();

	public App()
	{
		InitializeComponent();

		Database.Init().Wait(5000);
		MainPage = new AppShell();
	}
}
