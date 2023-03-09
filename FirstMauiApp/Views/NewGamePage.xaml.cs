using FirstMauiApp.ViewModels;

namespace FirstMauiApp.Views;

public partial class NewGamePage : ContentPage
{
	public NewGamePage()
	{
		InitializeComponent();
	}

    private void NumberGuess_TextChanged(object sender, TextChangedEventArgs e)
    {
		NewGameViewModel viewModel = (NewGameViewModel)this.BindingContext;

		if (viewModel.CalculateMaxChancesCommand.CanExecute(null))
			viewModel.CalculateMaxChancesCommand.Execute(null);
    }
}