
namespace FitnessApp.Views;

public partial class UserLogin : ContentPage
{
    public double inTake { get; set; } = 1200;
    public double caloriesGoal { get; set; } = 2400;
    public double caloriesToTake => caloriesGoal - inTake;
    public UserLogin()
	{
		InitializeComponent();
        BindingContext = this; // Установка контекста данных для привязки
    }
    protected override void OnAppearing()
    {
        caloriesProgress.Progress = 100 / ((int)caloriesGoal / (int)inTake);
        base.OnAppearing();
    }
}