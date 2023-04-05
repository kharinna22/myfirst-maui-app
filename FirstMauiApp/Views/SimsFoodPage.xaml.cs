using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
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
    private int foodId = 0;
	public SimsFoodPage()
	{
		InitializeComponent();
        SimsFoodViewModel viewModel = (SimsFoodViewModel)this.BindingContext;
        if (!viewModel.VerifyLoading())
        {
            LoadingPopup.Show();
            return;
        }
    }

    private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        SimsFoodViewModel viewModel = (SimsFoodViewModel)this.BindingContext;

        if (viewModel.FoodBySearchCommand.CanExecute(e.NewTextValue) && e.NewTextValue != null)
            viewModel.FoodBySearchCommand.Execute(e.NewTextValue);
    }

    private void ClickToShowPopup_Clicked(object sender, EventArgs e)
    {
        SimsFoodViewModel viewModel = (SimsFoodViewModel)this.BindingContext;
        viewModel.LoadFilters();
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

    async public static Task Wait(int intervalo = 500)
    {
        await Task.Delay(intervalo);
    }

    private void OnDataGridCellTapped(object sender, DataGridCellTappedEventArgs e)
    {
        SimsFoodViewModel viewModel = (SimsFoodViewModel)this.BindingContext;

        Food food = (Food)e.RowData;

        if (!viewModel.VerifyLoading())
        {
            foodId = food.Id;
            LoadingPopup.Show();
            return;
        }

        if (food != null && viewModel.GetFoodDetailsCommand.CanExecute(food.Id))
        {
            viewModel.GetFoodDetailsCommand.Execute(food.Id);
            DetailsPopup.Show();
        }
    }

    private void FiltrosPopup_Closed(object sender, EventArgs e)
    {
        SimsFoodViewModel viewModel = (SimsFoodViewModel)this.BindingContext;
        if (viewModel.KeepFiltersCommand.CanExecute(null))
            viewModel.KeepFiltersCommand.Execute(null);
    }

    private void RandomFood_Clicked(object sender, EventArgs e)
    {
        SimsFoodViewModel viewModel = (SimsFoodViewModel)this.BindingContext;

        if (viewModel.VerifyLoading() && viewModel.RandomFoodDetailsCommand.CanExecute(null))
        {
            viewModel.RandomFoodDetailsCommand.Execute(null);
            DetailsPopup.Show();
        }
    }

    private async void LoadingPopup_Opened(object sender, EventArgs e)
    {
        await Wait(2500);

        SimsFoodViewModel viewModel = (SimsFoodViewModel)this.BindingContext;
        while (true) {

            if (viewModel.VerifyLoading())
            {
                LoadingPopup.IsOpen = false;
                return;
            }

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            IToast toast = Toast.Make($"CARGANDO {App.Database.IsLoading()}", ToastDuration.Long, 14);
            await toast.Show(cancellationTokenSource.Token);

            await Wait(10000);
        }

    }

    private void LoadingPopup_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        SimsFoodViewModel viewModel = (SimsFoodViewModel)this.BindingContext;
        if (foodId != 0 && viewModel.GetFoodDetailsCommand.CanExecute(foodId))
        {
            viewModel.GetFoodDetailsCommand.Execute(foodId);
            DetailsPopup.Show();
        }
    }
}