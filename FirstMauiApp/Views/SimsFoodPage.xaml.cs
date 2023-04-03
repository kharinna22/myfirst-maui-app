using System.Collections.ObjectModel;
using FirstMauiApp.Data;
using FirstMauiApp.Models;
using FirstMauiApp.ViewModels;
using Microsoft.Maui.Controls.Shapes;
using Syncfusion.Maui.DataGrid;
using Syncfusion.Maui.DataSource;
using Syncfusion.Maui.Popup;

namespace FirstMauiApp.Views;

public partial class SimsFoodPage : ContentPage
{
	public SimsFoodPage()
	{
		InitializeComponent();
    }

    private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        SimsFoodViewModel viewModel = (SimsFoodViewModel)this.BindingContext;

        if (viewModel.FoodBySearchCommand.CanExecute(e.NewTextValue) && e.NewTextValue != null)
            viewModel.FoodBySearchCommand.Execute(e.NewTextValue);
    }

    private void ClickToShowPopup_Clicked(object sender, EventArgs e)
    {
        FiltrosPopup.Show();
    }

    private void dataGrid_QueryRowHeight(object sender, DataGridQueryRowHeightEventArgs e)
    {
        if (e.RowIndex != 0)
        {
            //Calculates and sets the height of the row based on its content.
            e.Height = e.GetIntrinsicRowHeight(e.RowIndex);
            e.Handled = true;
        }
    }

    private void OnDataGridCellTapped(object sender, DataGridCellTappedEventArgs e)
    {
        SimsFoodViewModel viewModel = (SimsFoodViewModel)this.BindingContext;

        Food food = (Food)e.RowData;

        if (food != null && viewModel.GetFoodDetailsCommand.CanExecute(food.Id))
        {
            viewModel.GetFoodDetailsCommand.Execute(food.Id);
            DetailsPopup.Show();
        }
    }

        DetailsPopup.Show();
    }
}