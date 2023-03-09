using FirstMauiApp.ViewModels;
using Microsoft.Maui.ApplicationModel;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace FirstMauiApp.Views;

public partial class GuessTheNumberPage : ContentPage
{

    public GuessTheNumberPage()
    {
        InitializeComponent();
        AdivinarBtn.IsEnabled = false;
    }

    private async void ReiniciarBtnClicked(object sender, EventArgs e)
    {
        var page = Navigation.NavigationStack.LastOrDefault();

        GuessTheNumberViewModel viewModel = this.BindingContext as GuessTheNumberViewModel;

        await Shell.Current.GoToAsync($"{nameof(GuessTheNumberPage)}?minguess={viewModel.MinGuess}&maxguess={viewModel.MaxGuess}&maxchances={viewModel.MaxChances}", false);

        Navigation.RemovePage(page);
    }

    private void NumberInputChanged(object sender, TextChangedEventArgs e)
    {
        if (!NumberEntry.IsEnabled && NumberEntry.Text == null)
        {
            NumberEntry.Text = e.OldTextValue;
            AdivinarBtn.IsEnabled = false;
            return;
        }
        else if (!NumberEntry.IsEnabled && NumberEntry.Text != null)
            return;

        AdivinarBtn.IsEnabled = e.NewTextValue != "" && e.NewTextValue != null;
        if (e.NewTextValue != null && NumberEntry.IsEnabled) NumberEntry.Text = Regex.Replace(e.NewTextValue, "[^0-9]", "");
    }

}