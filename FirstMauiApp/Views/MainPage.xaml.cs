namespace FirstMauiApp.Views;

public partial class MainPage : ContentPage
{
	const int max = 30;
    const int min = 0;
    const int remainingChances = 5;
    
    public MainPage()
	{
		InitializeComponent();
        LevelTwoLbl.Text = $"Te pediremos que adivines el número entre {min} y {max}\nTienes {remainingChances} oportunidades\n¡Comencemos!";
    }

    private async void IniciarJuegoClicked(object sender, EventArgs e)
    {    
        await Navigation.PushAsync(new GuessTheNumberPage());
    }

}

