namespace FirstMauiApp.Views;

public partial class MainPage : ContentPage
{
	const int max = 30;
    const int min = 0;
    int numberToBeGuessed = new Random().Next(min, max);
    const int remainingChances = 5;
    bool numberFound = false;
    List<String> temperaturas = new()
    {
        "ARDIENDO EN LLAMAS",
        "TIBIO",
        "NI FRIO NI CALIENTE",
        "FRIO",
        "CONGELADISIMOOO"
    };

    public MainPage()
	{
		InitializeComponent();
        LevelTwoLbl.Text = $"Te pediremos que adivines el número entre {min} y {max}\nTienes {remainingChances} oportunidades\n¡Comencemos!";
    }

    private async void IniciarJuegoClicked(object sender, EventArgs e)
    {

        
        //await Navigation.PushModalAsync(new GuessTheNumberPage());
        await Navigation.PushAsync(new GuessTheNumberPage());
        //await Shell.Current.GoToAsync("GuessTheNumberPage");
    }

    //   private void OnCounterClicked(object sender, EventArgs e)
    //{
    //	count++;

    //	if (count == 1)
    //		CounterBtn.Text = $"Apretaste {count} vez, maldito";
    //	else
    //		CounterBtn.Text = $"Apretaste {count} veces, lindo";

    //	SemanticScreenReader.Announce(CounterBtn.Text);
    //}

    //  private void OnEntryCompleted(object sender, EventArgs e)
    //  {
    //LevelOneLbl.Text = $"Valor ingresado: {entry.Text}";


    //      //string oldText = e.OldTextValue;
    //      //string newText = e.NewTextValue;
    //      //string myText = entry.Text;

    //  }
}

