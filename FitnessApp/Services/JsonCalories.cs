using FitnessApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FitnessApp.Services
{
    public class JsonCalories
    {
        List<Calories> foodList = new();
        public async Task<List<Calories>> GetFoods()
        {
            if (foodList?.Count > 0)
                return foodList;
            using var stream = await FileSystem.OpenAppPackageFileAsync("products.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            foodList = JsonSerializer.Deserialize(contents, CaloriesContext.Default.ListCalories);
            return foodList;
        }

        public async Task<List<Calories>> GetSearchResults(string query) {
            if (foodList?.Count > 0)
                return foodList.Where(x => x.name.ToLower().Contains(query.ToLower())).ToList();
            return foodList;
        }
    }
}
