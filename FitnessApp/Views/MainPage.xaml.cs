using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using FitnessApp.Services;
using FitnessApp.Views;
using Microsoft.Maui.Controls;
using System.Threading;

namespace FitnessApp
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDBService _dbService = new LocalDBService();
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
#if DEBUG
            dropDBbtn.IsVisible = true;
#endif
            base.OnAppearing();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            pass.IsEnabled = false;
            pass.IsEnabled = true;
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            string text = "Ошибка! Неверные данные для входа.";
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 18;
            var toast = Toast.Make(text, duration, fontSize);

            if (!String.IsNullOrEmpty(login.Text) && !String.IsNullOrEmpty(pass.Text))
            {
                bool success = await _dbService.VerifyPass(login.Text, pass.Text);
                if (success)
                {
                    await Navigation.PushAsync(new Dashboard(new ViewModel.DashboardViewModel(_dbService, login.Text, await _dbService.GetBodyMetrics(login.Text))));
                }
                else await toast.Show(cancellationTokenSource.Token);
            }
            else {
                await toast.Show(cancellationTokenSource.Token);
            }
        }

        private async void Registration_Clicked(object sender, EventArgs e)
        {
            pass.IsEnabled = false;
            pass.IsEnabled = true;
            await Navigation.PushAsync(new UserRegistration(_dbService), true);
        }

        private async void Drop_DB(object sender, EventArgs e) {
            await _dbService.DropAll();
            await Shell.Current.DisplayAlert("Сброс БД", "База сброшена, перезапустите приложение", "OK");
            Application.Current.Quit();
        }
    }

}
