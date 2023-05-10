using System.Windows;

using Homework_10.logic;

namespace Homework_10
{
    public partial class StartWindow : Window
    {
        public App app = App.Current as App;

        public StartWindow()
        {
            InitializeComponent();
        }

        private void OnButton_Сonsultant(object sender, RoutedEventArgs e)
        {
            app.CurrentUser = new Consultant();
            new MainWindow().Show();
            Close();
        }

        private void OnButton_Manager(object sender, RoutedEventArgs e)
        {
            app.CurrentUser = new Manager();
            new MainWindow().Show();
            Close();
        }
    }
}
