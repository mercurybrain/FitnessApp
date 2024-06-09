using FitnessApp.Views;

namespace FitnessApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(BodyMetricsTracker), typeof(BodyMetricsTracker));
            Routing.RegisterRoute(nameof(CaloriesPage), typeof(CaloriesPage));
            Routing.RegisterRoute(nameof(CalsHistoryPage), typeof(CalsHistoryPage));
        }
    }
}
