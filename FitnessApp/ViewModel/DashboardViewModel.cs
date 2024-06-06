using CommunityToolkit.Mvvm.ComponentModel;
using FitnessApp.Models;
using FitnessApp.Services;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore;
using System.Diagnostics;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;

namespace FitnessApp.ViewModel
{
    public partial class DashboardViewModel : BaseViewModel
    {
        LocalDBService _dbService;

        [ObservableProperty]
        public string _username;
        public BodyMetrics _bodyMetrics;
        [ObservableProperty]
        public int inTake;
        [ObservableProperty]
        public int caloriesGoal;
        [ObservableProperty]
        public double proteins;
        [ObservableProperty]
        public double fats;
        [ObservableProperty]
        public double carbohydrates;
        [ObservableProperty]
        public double caloriesToTake;
        [ObservableProperty]
        public double caloriesProgress;
        public ObservableValue ObservableValue1 { get; set; }
        public ObservableValue ObservableValue2 { get; set; }
        public IEnumerable<ISeries> Series { get; set; }
        public DashboardViewModel(LocalDBService dBService, string username, BodyMetrics bodyMetrics) {
            Title = "Главная";
            _dbService = dBService;
            _username = username;
            _bodyMetrics = bodyMetrics;

            var result = GetFoodAsync();
        }
        async Task GetFoodAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                User user = await _dbService.GetByUsername(Username);
                if (user.CaloriesGoal == 0)
                    CaloriesGoal = 2400;
                else
                    CaloriesGoal = user.CaloriesGoal;
                Tracker tracker = await _dbService.GetTracker(Username);
                InTake = 0;
                
                if (tracker is not null && tracker.DateTrack == DateTime.Now.Date) {
                    InTake = tracker.CaloriesInTake;
                    Proteins = tracker.b; Fats = tracker.g; Carbohydrates = tracker.u;
                }

                CaloriesToTake = CaloriesGoal - InTake;
                CaloriesProgress = 100 / (CaloriesGoal / InTake);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get data: {ex.Message}");
                await Shell.Current.DisplayAlert("Ошибка!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
