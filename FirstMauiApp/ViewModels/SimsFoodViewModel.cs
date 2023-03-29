using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FirstMauiApp.Data;
using FirstMauiApp.Models;
using FirstMauiApp.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FirstMauiApp.ViewModels;
internal class SimsFoodViewModel : ObservableObject
{
    public ObservableCollection<Food> Items { get; set; } = new();
    public ICommand FoodBySearchCommand { get; set; }

    public SimsFoodViewModel()
    {
        FoodBySearchCommand = new AsyncRelayCommand<string>(FoodBySearch);
        LoadTableAsync();
    }

    private async void LoadTableAsync()
    {
        var items = await App.Database.GetItemsAsync();
        Items.Clear();
        foreach (var item in items)
            Items.Add(item);

        OnPropertyChanged(nameof(Items));
    }

    private async Task FoodBySearch(string search)
    {
        var items = await App.Database.GetItemsFilteredAsync(search);
        Items.Clear();
        foreach (var item in items)
            Items.Add(item);

        OnPropertyChanged(nameof(Items));
    }

}

