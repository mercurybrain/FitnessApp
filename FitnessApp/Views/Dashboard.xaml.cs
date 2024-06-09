using __XamlGeneratedCode__;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using FitnessApp.Models;
using FitnessApp.Services;
using FitnessApp.ViewModel;

namespace FitnessApp.Views;

public partial class Dashboard : ContentPage
{
    private string _username;
    public Dashboard(DashboardViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        this._username = viewModel.Username;
        if (viewModel._bodyMetrics == null) this.ShowPopup(new PopupMetrics(_username));
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
    }
    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        var stack = Shell.Current.Navigation.NavigationStack.ToArray();
        for (int i = stack.Length - 1; i > 0; i--)

        {

            Shell.Current.Navigation.RemovePage(stack[i]);

        }
        await Navigation.PushAsync(new MainPage(), true);
    }

    private void bms_Clicked(object sender, EventArgs e)
    {
        AppShell.Current.GoToAsync($"{nameof(BodyMetricsTracker)}?_username={_username}");
    }
    private async void cals_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CaloriesPage(new ViewModel.FoodViewModel(new JsonCalories(), _username)));
    }

    private async void calsHistory_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CalsHistoryPage(new ViewModel.CalsHistoryVM(_username)));
    }

    private void exerciseClicked(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new CalsHistroyPage);
    }
}