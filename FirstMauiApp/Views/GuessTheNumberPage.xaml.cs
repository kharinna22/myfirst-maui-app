using Microsoft.Maui.ApplicationModel;
using System.Text.RegularExpressions;

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

        await Shell.Current.GoToAsync(nameof(GuessTheNumberPage), false);

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