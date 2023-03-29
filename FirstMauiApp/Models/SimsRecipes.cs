using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstMauiApp.Models
{
    public class Food
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameES { get; set; }
        public int Skill { get; set; }
    }
    public class Size
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameES { get; set; }
    }
    public class Ingredient
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameES { get; set; }
    }
    public class ServingTime
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameES { get; set; }
    }
    public class Other
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameES { get; set; }
    }
    public class Pack
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameES { get; set; }
    }

    public class Recipe
    {
        [PrimaryKey]
        public int Id { get; set; }
        [Indexed]
        public int FoodId { get; set; }
        [Indexed]
        public int SizeId { get; set; }
        public int Price { get; set; }
    }
    public class Component
    {
        [PrimaryKey]
        public int Id { get; set; }
        [Indexed]
        public int RecipeId { get; set; }
    }
    public class ComponentIngredient
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int ComponentId { get; set; }
        [Indexed]
        public int IngredientId { get; set; }
        public int Quantity { get; set; }
    }
    public class FoodServingTime
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int FoodId { get; set; }
        [Indexed]
        public int ServingTimeId { get; set; }
    }
    public class FoodOther    
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int FoodId { get; set; }
        [Indexed]
        public int OtherId { get; set; }
    }
    public class FoodPack
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int FoodId { get; set; }
        [Indexed]
        public int PackId { get; set; }
    }

    
}
