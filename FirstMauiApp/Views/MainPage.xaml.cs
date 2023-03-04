namespace FirstMauiApp.Views;

public partial class MainPage : ContentPage
{
	
    public MainPage()
	{
		InitializeComponent();
    }

    private async void IniciarJuegoClicked(object sender, EventArgs e)
    {    
        await Navigation.PushAsync(new GuessTheNumberPage());
    }

}

