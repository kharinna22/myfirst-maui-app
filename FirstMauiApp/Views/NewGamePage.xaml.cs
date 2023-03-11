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

		if (viewModel.VerifyNumbersRangeCommand.CanExecute(null))
			viewModel.VerifyNumbersRangeCommand.Execute(null);
	}

	private void NumberChances_TextChanged(object sender, TextChangedEventArgs e)
	{
        NewGameViewModel viewModel = (NewGameViewModel)this.BindingContext;

		if(viewModel.IsMaxChancesEditable)
            if (viewModel.VerifyNumberChancesCommand.CanExecute(null))
                viewModel.VerifyNumberChancesCommand.Execute(null);

    }
}