using System.Collections.ObjectModel;
using FirstMauiApp.Data;
using FirstMauiApp.Models;
using FirstMauiApp.ViewModels;
using Syncfusion.Maui.Popup;

namespace FirstMauiApp.Views;

public partial class SimsFoodPage : ContentPage
{
	public SimsFoodPage()
	{
		InitializeComponent();
		SfPopup popup = new ();
    }

    private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        SimsFoodViewModel viewModel = (SimsFoodViewModel)this.BindingContext;

        if (viewModel.FoodBySearchCommand.CanExecute(e.NewTextValue) && e.NewTextValue != null)
            viewModel.FoodBySearchCommand.Execute(e.NewTextValue);
    }

    private void ClickToShowPopup_Clicked(object sender, EventArgs e)
    {
        popup.Show();
    }
}