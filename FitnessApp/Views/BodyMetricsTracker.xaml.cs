using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using FitnessApp.Models;
using FitnessApp.Services;

namespace FitnessApp.Views;

[QueryProperty("_username","_username")]
public partial class BodyMetricsTracker : ContentPage, IQueryAttributable
{
    private LocalDBService _dbService = new LocalDBService();
    string _username;

    // Уровень активности (коэффициенты)
    double[] activityFactors = { 1.2, 1.375, 1.55, 1.725, 1.9 };
    double selectedFactor;
    BodyMetrics bm = null;
    public BodyMetricsTracker()
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        frameResult.IsVisible = false;
        GetBodyMetrics(_username);
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query) {
        _username = query["_username"] as string;
    }
    private async void CalculateClicked(object sender, EventArgs e)
    {
        if (double.TryParse(weightEntry.Text, out double weight) && double.TryParse(heightEntry.Text, out double height) && int.TryParse(ageEntry.Text, out int age) 
            && double.TryParse(weightGoal.Text, out double goalWeight))
        {
            double bmr = CalculateBMR(weight, height, genderPicker.SelectedIndex, age);
            double tdee = CalculateTDEE(bmr);

            BodyMetrics metrics = new BodyMetrics()
            {
                Age = age,
                Gender = genderPicker.SelectedIndex,
                BMR = bmr,
                TDEE = tdee,
                Weight = weight,
                HeightCm = height,
                ActivityLevel = selectedFactor,
                GoalWeight = goalWeight,
                Username = _username
            };

            int caloriesGoal = goalWeight > weight ? (int)tdee + 750 : (int)tdee - 750;

            User user = await _dbService.GetByUsername(_username);
            user.CaloriesGoal = caloriesGoal;
            await _dbService.UpdateUser(user);

            if (bm != null)
            {
                metrics.Id = bm.Id;
                await _dbService.UpdateMetrics(metrics);
                Toast.Make("Данные обновлены").Show();
            }
            else {
                await _dbService.CreateMetrics(metrics);
            }
            frameResult.IsVisible = true;
            BMRLabel.Text = $"Ваша базовая метаболическая скорость (BMR): {bmr:0} ккал/сут";
            TDEELabel.Text = $"Ваша общая дневная энергозатрата (TDEE): {tdee:0} ккал/сут";
        }
        else
        {
            Toast.Make("Введите корректные данные.").Show();
        }
    }

    private double CalculateBMR(double weight, double height, int gender, int age)
    {
        // Формула Харриса-Бенедикта для расчета BMR (базовой метаболической скорости)
        // Мужчины: BMR = (10 * weight) + (6.25 * height) - (5 * (double)age) + 5;
        // Женщины: BMR = (10 * weight) + (6.25 * height) - (5 * (double)age) - 161;

        double bmr = 88.362 + (13.397 * weight) - (5.677 * 30);
        switch (gender) {
            case 0:
                bmr = (10 * weight) + (6.25 * height) - (5 * (double)age) + 5;
                break;
            case 1:
                bmr = (10 * weight) + (6.25 * height) - (5 * (double)age) - 161;
                break;
        }
        
        return bmr;
    }

    private double CalculateTDEE(double bmr)
    {
        selectedFactor = activityFactors[activityLevelPicker.SelectedIndex];
        double tdee = bmr * selectedFactor;
        return tdee;
    }

    private async void GetBodyMetrics(string username) {
        bm = await _dbService.GetBodyMetrics(username);
        if (bm == null) return;
        weightEntry.Text = bm.Weight.ToString();
        weightGoal.Text = bm.GoalWeight.ToString();
        heightEntry.Text = bm.HeightCm.ToString();
        ageEntry.Text = bm.Age.ToString();
        genderPicker.SelectedIndex = bm.Gender;
        activityLevelPicker.SelectedIndex = Array.IndexOf(activityFactors, bm.ActivityLevel);
        BMRLabel.Text = $"Ваша базовая метаболическая скорость (BMR): {bm.BMR.ToString():0} ккал/сут";
        TDEELabel.Text = $"Ваша общая дневная энергозатрата (TDEE): {bm.TDEE.ToString():0} ккал/сут";
        frameResult.IsVisible = true;
    }
}