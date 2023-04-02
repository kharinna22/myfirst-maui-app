﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FirstMauiApp.Data;
using FirstMauiApp.Models;
using FirstMauiApp.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FirstMauiApp.ViewModels;
internal class SimsFoodViewModel : ObservableObject
{
    #region Tables & Lists
    public ObservableCollection<Food> Items { get; set; } = new();
    public List<Food> FoodsFiltered { get; set; } = new();
    public List<Filter> Filters { get; set; }
    public ICommand FoodBySearchCommand { get; set; }
    public ICommand FoodByFilterCommand { get; set; }
    #endregion

    #region Food Details
    public string DetailsName { get; set; }
    public string DetailsSkill { get; set; }
    public List<Other> DetailsOthers { get; set; } = new();
    public bool IsDetailsOthersVisible { get; set; }
    public List<Pack> DetailsPacks { get; set; } = new();
    public ICommand GetFoodDetailsCommand { get; set; }
    #endregion

    public SimsFoodViewModel()
    {
        FoodBySearchCommand = new RelayCommand<string>(FoodBySearch);
        FoodByFilterCommand = new RelayCommand(FoodByFilter);
        GetFoodDetailsCommand = new RelayCommand<int>(GetFoodDetails);
        LoadData();
    }

    private void ReloadFoodList(List<Food> foods)
    {
        Items.Clear();

        foreach (Food food in foods)
            Items.Add(food);
    }

    private void LoadTable()
    {
        List<Food> items = App.Database.GetFoods();
        ReloadFoodList(items);
        FoodsFiltered = items.ToList();
    }

    private void LoadData()
    {
        Filters = App.Database.GetFilters();
        LoadTable();
    }

    private void FoodBySearch(string search)
    {
        List<Food> items = App.Database.GetItemsFiltered(search,FoodsFiltered);
        ReloadFoodList(items);
    }

    private void FoodByFilter()
    {
        List<Food> items = App.Database.GetFoods();
        if (Filters.FirstOrDefault(f => f.IsSelected)!=null)
            items = App.Database.GetItemsFilteredByMultiple(Filters.Where(f => f.IsSelected).ToList());
        
        FoodsFiltered = items.ToList();
        ReloadFoodList(items);

    }

    private void GetFoodDetails(int foodId)
    {
        FoodDetails foodDetails = App.Database.GetFoodDetails(foodId);
        DetailsName = foodDetails.Name;
        DetailsSkill = foodDetails.Skill.ToString();
        DetailsOthers = foodDetails.Others.ToList();

        IsDetailsOthersVisible = DetailsOthers.Count > 0;

        DetailsPacks = foodDetails.Packs.ToList();
        
        OnPropertyChanged(nameof(DetailsName));
        OnPropertyChanged(nameof(DetailsSkill));
        OnPropertyChanged(nameof(DetailsOthers));
        OnPropertyChanged(nameof(IsDetailsOthersVisible));
        OnPropertyChanged(nameof(DetailsPacks));
    }

    
}

