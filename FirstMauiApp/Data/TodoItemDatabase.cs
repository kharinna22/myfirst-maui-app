﻿using System;
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
        private SQLiteAsyncConnection Database;
        /* Listas estáticas ya que se espera sólo lectura */
        private List<Food> Foods;
        private List<Size> Sizes;
        private List<Ingredient> Ingredients;
        private List<ServingTime> ServingTimes;
        private List<Other> Others;
        private List<Pack> Packs;
        private List<Recipe> Recipes;
        private List<Component> Components;
        private List<ComponentIngredient> ComponentsIngredients;
        private List<FoodServingTime> FoodsServingTimes;
        private List<FoodOther> FoodsOthers;
        private List<FoodPack> FoodsPacks;
        List<Filter> Filters;

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
            Foods = await Database.Table<Food>().OrderBy(f => f.NameES).ToListAsync();

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
            Ingredients = Ingredients.OrderBy(i => i.NameES).ToList();

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

        public List<Filter> GetFilters()
        {
            List<Filter> filters = new();

            foreach (Size size in Sizes)
                filters.Add(new Filter() { Group = "Tamaño", Id = size.Id, NameEN = size.NameEN, NameES = size.NameES });

            //foreach (Ingredient ingredient in Ingredients)
            //    filters.Add(new Filter() { Group = "Ingredientes", Id = ingredient.Id, NameEN = ingredient.NameEN, NameES = ingredient.NameES });

            foreach (ServingTime servingTime in ServingTimes)
                filters.Add(new Filter() { Group = "Comidas del Día", Id = servingTime.Id, NameEN = servingTime.NameEN, NameES = servingTime.NameES });

            foreach (Other other in Others)
                filters.Add(new Filter() { Group = "Otros", Id = other.Id, NameEN = other.NameEN, NameES = other.NameES });

            foreach (Pack pack in Packs)
                filters.Add(new Filter() { Group = "Packs", Id = pack.Id, NameEN = pack.NameEN, NameES = pack.NameES });

            return filters;
        }

        public List<Food> GetFoods()
        {
            return Foods;
        }

        public List<Food> GetItemsFiltered(string search,List<Food> foods)
        {
            string searchNormalized = new String(search.Normalize(NormalizationForm.FormD).Where(c => c<128).ToArray());

            return
                foods
                    .Where(f => new String(f.NameES.Normalize(NormalizationForm.FormD).Where(c => c < 128).ToArray()).ToUpper().Contains(searchNormalized.ToUpper()))
                    .OrderBy(f => f.NameES).ToList();
        }

        public List<Food> GetItemsFilteredByMultiple(List<Filter> filters)
        {
            List<Food> foodsFiltered = Foods.ToList();

            if (filters.FirstOrDefault(f => f.Group == "Tamaño") != null)
            { 
                // Obtiene las comidas y sus tamaños
                foodsFiltered = foodsFiltered
                        .Join(Recipes,
                            f => f.Id,
                            r => r.FoodId,
                            (f, r) => new { Food = f, Recipe = r })
                        .Join(filters.Where(s => s.Group == "Tamaño").ToList(),
                            fr => fr.Recipe.SizeId,
                            filter => filter.Id,
                            (fr, filter) => fr.Food)
                        .ToList();

                foodsFiltered = ObtainFoodsDuplicated(foodsFiltered, filters.Where(s => s.Group == "Tamaño").Count());
            }

            // Filtro por ingrediente, debe mostrar las comidas que se puedan hacer con esos ingredientes

            if (filters.FirstOrDefault(f => f.Group == "Comidas del Día") != null)
            {
                foodsFiltered = foodsFiltered
                    .Join(FoodsServingTimes,
                        f => f.Id,
                        fst => fst.FoodId,
                        (f, fst) => new { Food = f, FoodServingTime = fst })
                    .Join(filters.Where(s => s.Group == "Comidas del Día").ToList(),
                        ffst => ffst.FoodServingTime.ServingTimeId,
                        filter => filter.Id,
                        (fr, filter) => fr.Food)
                    .ToList();

                foodsFiltered = ObtainFoodsDuplicated(foodsFiltered, filters.Where(s => s.Group == "Comidas del Día").Count());
            }

            if (filters.FirstOrDefault(f => f.Group == "Otros") != null)
            { 
                foodsFiltered = foodsFiltered
                    .Join(FoodsOthers,
                        f => f.Id,
                        fo => fo.FoodId,
                        (f, fo) => new { Food = f, FoodOther = fo })
                    .Join(filters.Where(s => s.Group == "Otros").ToList(),
                        ffo => ffo.FoodOther.OtherId,
                        filter => filter.Id,
                        (fr, filter) => fr.Food)
                    .ToList();

                foodsFiltered = ObtainFoodsDuplicated(foodsFiltered, filters.Where(s => s.Group == "Otros").Count());
            }

            if (filters.FirstOrDefault(f => f.Group == "Packs") != null)
                foodsFiltered = foodsFiltered
                    .Join(FoodsPacks,
                        f => f.Id,
                        fp => fp.FoodId,
                        (f, fp) => new { Food = f, FoodPack = fp })
                    .Join(filters.Where(s => s.Group == "Packs").ToList(),
                        ffp => ffp.FoodPack.PackId,
                        filter => filter.Id,
                        (fr, filter) => fr.Food)
                    .ToList();

            return foodsFiltered
                    .GroupBy(f => f.Id)
                    .Select(f => f.First())
                    .ToList();
        }

        /* Mantiene los objetos que están duplicados una cierta cantidad de veces */
        private List<Food> ObtainFoodsDuplicated(List<Food> foodsFiltered,int timesDuplicated)
        {
            return foodsFiltered
                    .GroupBy(f => f.Id)
                    .Where(g => g.Count() == timesDuplicated)
                    .Select(f => f.First())
                    .ToList();
        }

        public FoodDetails GetFoodDetails(int foodId)
        {
            Food food = Foods.Single(f => f.Id == foodId);

            FoodDetails foodDetails = new FoodDetails()
            {
                Name = food.NameES,
                Skill = food.Skill
            };

            foodDetails.ServingTimes = Foods
                .Join(FoodsServingTimes,
                    f => f.Id,
                    fst => fst.FoodId,
                    (f, fst) => new { Food = f, FoodServingTime = fst })
                .Where(ffst => ffst.Food.Id == foodId)
                .Join(ServingTimes,
                    ffst => ffst.FoodServingTime.ServingTimeId,
                    st => st.Id,
                    (ffst, st) => st)
                .ToList();


            foodDetails.Others = Foods
                .Join(FoodsOthers,
                    f => f.Id,
                    fo => fo.FoodId,
                    (f, fo) => new { Food = f, FoodOther = fo })
                .Where(ffo => ffo.Food.Id == foodId)
                .Join(Others,
                    ffo => ffo.FoodOther.OtherId,
                    o => o.Id,
                    (ffo, o) => o)
                .ToList();

            foodDetails.Packs = Foods
                .Join(FoodsPacks,
                    f => f.Id,
                    fp => fp.FoodId,
                    (f, fp) => new { Food = f, FoodPack = fp })
                .Where(ffp => ffp.Food.Id == foodId)
                .Join(Packs,
                    ffp => ffp.FoodPack.PackId,
                    p => p.Id,
                    (ffp, p) => p)
                .ToList();

            // REGLA = Las comidas que requieran el juego base o la versión deluxe, sólo mostrarán que requieren el juego base
            if (foodDetails.Packs.Find(p => p.Id == 1) != null && foodDetails.Packs.Find(p => p.Id == 2) != null)
                foodDetails.Packs.Remove(foodDetails.Packs.Find(p => p.Id == 2));

            return foodDetails;
        }


        
    }
}
