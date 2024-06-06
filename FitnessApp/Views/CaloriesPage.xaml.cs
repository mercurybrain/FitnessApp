using FitnessApp.Models;
using FitnessApp.Services;
using FitnessApp.ViewModel;

namespace FitnessApp.Views;

public partial class CaloriesPage : ContentPage
{
    public CaloriesPage(FoodViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}