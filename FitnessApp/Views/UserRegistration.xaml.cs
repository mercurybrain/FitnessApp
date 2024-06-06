using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using FitnessApp.Models;
using FitnessApp.Services;
using System.Diagnostics;

namespace FitnessApp.Views;

public partial class UserRegistration : ContentPage
{
    private readonly LocalDBService _dbService;
    public UserRegistration(LocalDBService dbService)
	{
		InitializeComponent();
        _dbService = dbService;
	}

    private async void Registration_Clicked(object sender, EventArgs e)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        string text = "Ошибка! Неверный формат данных.";
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 18;
        var toast = Toast.Make(text, duration, fontSize);

        if (!String.IsNullOrEmpty(login.Text) && !String.IsNullOrEmpty(pass.Text) && !String.IsNullOrEmpty(email.Text))
        {
            User user = new User();
            user.Username = login.Text;
            user.PasswordHash = pass.Text;
            user.Email = email.Text;

            try {
                await _dbService.CreateUser(user);
                await Toast.Make("Регистрация успешна! Теперь вы можете войти.", ToastDuration.Short, 18).Show();
                await Navigation.PopAsync(true);
            } catch (Exception ex) {
                Debug.Print(ex.Source + " : " + ex.Message);
                await toast.Show(cancellationTokenSource.Token);
            }
        }
        else
        {
            await toast.Show(cancellationTokenSource.Token);
        }
    }
}