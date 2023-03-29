using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using FirstMauiApp.Models;
using Size = FirstMauiApp.Models.Size;
using System.Net.WebSockets;

namespace FirstMauiApp.Data
{
    public class RecipesDatabase
    {
        SQLiteAsyncConnection Database;
        /* Listas estáticas ya que se espera sólo lectura */
        List<Food> Foods;
        List<Size> Sizes;
        List<Ingredient> Ingredients;
        List<ServingTime> ServingTimes;
        List<Other> Others;
        List<Pack> Packs;
        List<Recipe> Recipes;
        List<Component> Components;
        List<ComponentIngredient> ComponentsIngredients;
        List<FoodServingTime> FoodsServingTimes;
        List<FoodOther> FoodsOthers;
        List<FoodPack> FoodsPacks;

        public RecipesDatabase() { }

        public async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            
            await Database.CreateTableAsync<Food>();
            await Database.CreateTableAsync<Size>();
            await Database.CreateTableAsync<Ingredient>();
            await Database.CreateTableAsync<ServingTime>();
            await Database.CreateTableAsync<Other>();
            await Database.CreateTableAsync<Pack>();

            await Database.CreateTableAsync<Recipe>();
            await Database.CreateTableAsync<Component>();
            await Database.CreateTableAsync<ComponentIngredient>();
            await Database.CreateTableAsync<FoodServingTime>();
            await Database.CreateTableAsync<FoodOther>();
            await Database.CreateTableAsync<FoodPack>();

            await LoadDatabaseAsync();
        }

        private async Task LoadDatabaseAsync()
        {
            // CARGAR FOODS
            if(await Database.Table<Food>().CountAsync() == 0) 
            {
                using var fileStream = await FileSystem.OpenAppPackageFileAsync("Foods.csv");
                using var reader = new StreamReader(fileStream, Encoding.Latin1);
                var contents = reader.ReadToEnd();
                var lines = contents.Split("\r\n");

                foreach (var line in lines)
                {
                    string[] data = line.Split(",");

                    bool isNumeric = int.TryParse(data[0], out _);
                    if (!isNumeric)
                        continue;


                    await Database.InsertAsync(
                        new Food
                        {
                            Id = int.Parse(data[0]),
                            NameEN = data[1],
                            NameES = data[2],
                            Skill = int.Parse(data[3])
                        }
                    );
                }
            }
            Foods = await Database.Table<Food>().ToListAsync();

            // CARGAR SIZES
            if (await Database.Table<Size>().CountAsync() == 0)
            {
                using var fileStream = await FileSystem.OpenAppPackageFileAsync("Sizes.csv");
                using var reader = new StreamReader(fileStream, Encoding.Latin1);
                var contents = reader.ReadToEnd();
                var lines = contents.Split("\r\n");

                foreach (var line in lines)
                {
                    string[] data = line.Split(",");

                    bool isNumeric = int.TryParse(data[0], out _);
                    if (!isNumeric)
                        continue;

                    await Database.InsertAsync(
                        new Size
                        {
                            Id = int.Parse(data[0]),
                            NameEN = data[1],
                            NameES = data[2]
                        }
                    );
                }
            }
            Sizes = await Database.Table<Size>().ToListAsync();

            // CARGAR INGREDIENTS
            if (await Database.Table<Ingredient>().CountAsync() == 0)
            {
                using var fileStream = await FileSystem.OpenAppPackageFileAsync("Ingredients.csv");
                using var reader = new StreamReader(fileStream, Encoding.Latin1);
                var contents = reader.ReadToEnd();
                var lines = contents.Split("\r\n");

                foreach (var line in lines)
                {
                    string[] data = line.Split(",");

                    bool isNumeric = int.TryParse(data[0], out _);
                    if (!isNumeric)
                        continue;

                    await Database.InsertAsync(
                        new Ingredient
                        {
                            Id = int.Parse(data[0]),
                            NameEN = data[1],
                            NameES = data[2]
                        }
                    );
                }
            }
            Ingredients = await Database.Table<Ingredient>().ToListAsync();

            // CARGAR SERVING TIMES
            if (await Database.Table<ServingTime>().CountAsync() == 0)
            {
                using var fileStream = await FileSystem.OpenAppPackageFileAsync("ServingTimes.csv");
                using var reader = new StreamReader(fileStream, Encoding.Latin1);
                var contents = reader.ReadToEnd();
                var lines = contents.Split("\r\n");

                foreach (var line in lines)
                {
                    string[] data = line.Split(",");

                    bool isNumeric = int.TryParse(data[0], out _);
                    if (!isNumeric)
                        continue;

                    await Database.InsertAsync(
                        new ServingTime
                        {
                            Id = int.Parse(data[0]),
                            NameEN = data[1],
                            NameES = data[2]
                        }
                    );
                }
            }
            ServingTimes = await Database.Table<ServingTime>().ToListAsync();

            // CARGAR OTHERS
            if (await Database.Table<Other>().CountAsync() == 0)
            {
                using var fileStream = await FileSystem.OpenAppPackageFileAsync("Others.csv");
                using var reader = new StreamReader(fileStream, Encoding.Latin1);
                var contents = reader.ReadToEnd();
                var lines = contents.Split("\r\n");

                foreach (var line in lines)
                {
                    string[] data = line.Split(",");

                    bool isNumeric = int.TryParse(data[0], out _);
                    if (!isNumeric)
                        continue;

                    await Database.InsertAsync(
                        new Other
                        {
                            Id = int.Parse(data[0]),
                            NameEN = data[1],
                            NameES = data[2]
                        }
                    );
                }
            }
            Others = await Database.Table<Other>().ToListAsync();

            // CARGAR PACKS
            if (await Database.Table<Pack>().CountAsync() == 0)
            {
                using var fileStream = await FileSystem.OpenAppPackageFileAsync("Packs.csv");
                using var reader = new StreamReader(fileStream, Encoding.Latin1);
                var contents = reader.ReadToEnd();
                var lines = contents.Split("\r\n");

                foreach (var line in lines)
                {
                    string[] data = line.Split(",");

                    bool isNumeric = int.TryParse(data[0], out _);
                    if (!isNumeric)
                        continue;

                    await Database.InsertAsync(new Pack { Id = int.Parse(data[0]), NameEN = data[1], NameES = data[2] });
                }
            }
            Packs = await Database.Table<Pack>().ToListAsync();

            // CARGAR RECIPES
            if (await Database.Table<Recipe>().CountAsync() == 0)
            {
                using var fileStream = await FileSystem.OpenAppPackageFileAsync("Recipes.csv");
                using var reader = new StreamReader(fileStream, Encoding.Latin1);
                var contents = reader.ReadToEnd();
                var lines = contents.Split("\r\n");

                foreach (var line in lines)
                {
                    string[] data = line.Split(",");

                    bool isNumeric = int.TryParse(data[0], out _);
                    if (!isNumeric)
                        continue;

                    await Database.InsertAsync(
                        new Recipe
                        {
                            Id = int.Parse(data[0]),
                            FoodId = int.Parse(data[1]),
                            SizeId = int.Parse(data[2]),
                            Price = int.Parse(data[3])
                        }
                    );
                }
            }
            Recipes = await Database.Table<Recipe>().ToListAsync();

            // CARGAR COMPONENTS
            if (await Database.Table<Component>().CountAsync() == 0)
            {
                using var fileStream = await FileSystem.OpenAppPackageFileAsync("Components.csv");
                using var reader = new StreamReader(fileStream, Encoding.Latin1);
                var contents = reader.ReadToEnd();
                var lines = contents.Split("\r\n");

                foreach (var line in lines)
                {
                    string[] data = line.Split(",");

                    bool isNumeric = int.TryParse(data[0], out _);
                    if (!isNumeric)
                        continue;

                    await Database.InsertAsync(
                        new Component
                        {
                            Id = int.Parse(data[0]),
                            RecipeId = int.Parse(data[1])
                        }
                    );
                }
            }
            Components = await Database.Table<Component>().ToListAsync();

            // CARGAR COMPONENTINGREDIENT
            if (await Database.Table<ComponentIngredient>().CountAsync() == 0)
            {
                using var fileStream = await FileSystem.OpenAppPackageFileAsync("Components_Ingredients.csv");
                using var reader = new StreamReader(fileStream, Encoding.Latin1);
                var contents = reader.ReadToEnd();
                var lines = contents.Split("\r\n");

                foreach (var line in lines)
                {
                    string[] data = line.Split(",");

                    bool isNumeric = int.TryParse(data[0], out _);
                    if (!isNumeric)
                        continue;

                    await Database.InsertAsync(
                        new ComponentIngredient
                        {
                            ComponentId = int.Parse(data[0]),
                            IngredientId = int.Parse(data[1]),
                            Quantity = int.Parse(data[2])
                        }
                    );
                }
            }
            ComponentsIngredients = await Database.Table<ComponentIngredient>().ToListAsync();

            // CARGAR FOODSERVINGTIME
            if (await Database.Table<FoodServingTime>().CountAsync() == 0)
            {
                using var fileStream = await FileSystem.OpenAppPackageFileAsync("Foods_ServingTimes.csv");
                using var reader = new StreamReader(fileStream, Encoding.Latin1);
                var contents = reader.ReadToEnd();
                var lines = contents.Split("\n");

                foreach (var line in lines)
                {
                    string[] data = line.Split(",");

                    bool isNumeric = int.TryParse(data[0], out _);
                    if (!isNumeric)
                        continue;

                    await Database.InsertAsync(new FoodServingTime { FoodId = int.Parse(data[0]), ServingTimeId = int.Parse(data[1]) });
                }
            }
            FoodsServingTimes = await Database.Table<FoodServingTime>().ToListAsync();

            // CARGAR FOODOTHER
            if (await Database.Table<FoodOther>().CountAsync() == 0)
            {
                using var fileStream = await FileSystem.OpenAppPackageFileAsync("Foods_Others.csv");
                using var reader = new StreamReader(fileStream, Encoding.Latin1);
                var contents = reader.ReadToEnd();
                var lines = contents.Split("\n");

                foreach (var line in lines)
                {
                    string[] data = line.Split(",");

                    bool isNumeric = int.TryParse(data[0], out _);
                    if (!isNumeric)
                        continue;

                    await Database.InsertAsync(new FoodOther { FoodId = int.Parse(data[0]), OtherId = int.Parse(data[1]) });
                }
            }
            FoodsOthers = await Database.Table<FoodOther>().ToListAsync();

            // CARGAR FOODPACK
            if (await Database.Table<FoodPack>().CountAsync() == 0)
            {
                using var fileStream = await FileSystem.OpenAppPackageFileAsync("Foods_Packs.csv");
                using var reader = new StreamReader(fileStream, Encoding.Latin1);
                var contents = reader.ReadToEnd();
                var lines = contents.Split("\n");

                foreach (var line in lines)
                {
                    string[] data = line.Split(",");

                    bool isNumeric = int.TryParse(data[0], out _);
                    if (!isNumeric)
                        continue;

                    await Database.InsertAsync(new FoodPack { FoodId = int.Parse(data[0]), PackId = int.Parse(data[1]) });
                }

            }
            FoodsPacks = await Database.Table<FoodPack>().ToListAsync();
        }

        public async Task<List<Food>> GetItemsAsync()
        {
            await Init();

            return Foods.OrderBy(f => f.NameES).ToList();
        }

        public async Task<List<Food>> GetItemsFilteredAsync(string search)
        {
            await Init();
            return 
                Foods
                    .Where(f => f.NameES.ToUpper().Contains(search.ToUpper()))
                    .OrderBy(f => f.NameES).ToList();
        }
    }
}
