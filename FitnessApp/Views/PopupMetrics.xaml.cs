using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace FitnessApp.Views;

public partial class PopupMetrics : Popup
{
    string _username;
	public PopupMetrics(string username)
	{
        InitializeComponent();
        _username = username;
	}

    private void Button_Close(object sender, EventArgs e)
    {
		Close();
    }

    private void Button_Redirect(object sender, EventArgs e)
    {
        Close();
        AppShell.Current.GoToAsync($"{nameof(BodyMetricsTracker)}?_username={_username}");
    }
}