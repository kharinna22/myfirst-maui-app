using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstMauiApp.Models
{
    public class FoodDetails
    {
        // PROXIMAMENTE 
        // public string Photo { get; set; }
        public string Name { get; set; }
        public int Skill { get; set; }
        public List<RecipesDetails> Recipes { get; set; }
        public List<ServingTime> ServingTimes { get; set; }
        public List<Other> Others { get; set; }
        public List<Pack> Packs { get; set; }
    }

    public class RecipesDetails
    {
        public string Size { get; set; }
        public string Price { get; set; }
        public string Ingredients { get; set; }
    }

}