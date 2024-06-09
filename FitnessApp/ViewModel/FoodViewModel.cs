using FitnessApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FitnessApp.Services;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Alerts;
using System.Globalization;

namespace FitnessApp.ViewModel
{
    public partial class FoodViewModel : BaseViewModel
    {
        JsonCalories foodService;
        LocalDBService _dbService;
        string _username;
        public ObservableCollection<Calories> foodList { get; } = new();
        public FoodViewModel(JsonCalories foodService, string username)
        {
            Title = "Трекер калорий";
            this.foodService = foodService;
            _dbService = new LocalDBService();
            _username = username;
        }
        [RelayCommand]
        async Task GetFoodAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var foods = await foodService.GetFoods();

                if (foodList.Count != 0)
                    foodList.Clear();

                foreach (var food in foods)
                    foodList.Add(food);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get foods: {ex.Message}");
                await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        [RelayCommand]
        async Task PerformSearch(string query) {
            if (IsBusy)
                return;

            var foods = await foodService.GetSearchResults(query);

            if (foods.Any())
            {
                foodList.Clear();
                foreach (var food in foods)
                    foodList.Add(food);
            }
            else {
                await Toast.Make("Ничего не найдено").Show();
            }
        }

        [RelayCommand]
        async Task AddFood(Calories food) {
            if (food == null) return;

            float gr = float.Parse(await Shell.Current.DisplayPromptAsync(food.name, "Грамм: ", initialValue: "100", keyboard: Keyboard.Numeric), CultureInfo.InvariantCulture);

            try
            {
                IsBusy = true;

                float proteins = float.Parse(food.b, CultureInfo.InvariantCulture) * gr / 100;
                float fats = float.Parse(food.g, CultureInfo.InvariantCulture) * gr / 100;
                float carbohydrates = float.Parse(food.u, CultureInfo.InvariantCulture) * gr / 100;
                float kcal = float.Parse(food.kcal, CultureInfo.InvariantCulture) * gr / 100;

                var maybe = await _dbService.GetTracker(_username);
                if (maybe is not null && maybe.DateTrack == DateTime.Now.Date)
                {
                    proteins += maybe.b;
                    fats += maybe.g;
                    carbohydrates += maybe.u;
                    kcal += maybe.CaloriesInTake;


                    await _dbService.UpdateTracker(new Tracker()
                    {
                        Id = maybe.Id,
                        b = (float)Math.Round(proteins, 2),
                        g = (float)Math.Round(fats, 2),
                        u = (float)Math.Round(carbohydrates, 2),
                        CaloriesInTake = (float)Math.Round(kcal, 2),
                        DateTrack = DateTime.Now.Date,
                        Username = _username
                    });
                }
                else {
                    await _dbService.InsertTrack(new Tracker()
                    {
                        b = (float)Math.Round(proteins, 2),
                        g = (float)Math.Round(fats, 2),
                        u = (float)Math.Round(carbohydrates, 2),
                        CaloriesInTake = (float)Math.Round(kcal, 2),
                        DateTrack = DateTime.Now.Date,
                        Username = _username
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get foods: {ex.Message}");
                await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "OK");
            }
            finally {
                IsBusy = false;
            }
        }
    }
}
