using FirstMauiApp.ViewModels;
using Microsoft.Maui.ApplicationModel;
using System.Text.RegularExpressions;

namespace FirstMauiApp.Views;

public partial class RandomPage : ContentPage
{

    public RandomPage()
    {
        InitializeComponent();

        RandomViewModel viewModel = (RandomViewModel)this.BindingContext;

        if (viewModel.ActualizarCommand.CanExecute(null))
            viewModel.ActualizarCommand.Execute(null);
    }

}