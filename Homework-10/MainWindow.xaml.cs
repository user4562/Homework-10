using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using Homework_10.logic;
using Homework_10.pages;

namespace Homework_10
{
    public partial class MainWindow : Window
    {
        private App app = App.Current as App;

        public MainWindow()
        {
            Title = app.CurrentUser.Title;

            InitializeComponent();

            ButtonsLoad();
            PagesLoad();

            app.MainFrame = FrameContent;
            app.MainFrame.Content = app.MainPage;
            app.CurrentPage = app.MainPage;
            app.ButtonsForPage();
        }

        private void ButtonsLoad()
        { 
            app.ButtonDone = ButtonDone;
            app.ButtonBack = ButtonBack;
            app.ButtonCreate = ButtonCreate;
            app.ButtonRemove = ButtonRemove;
            app.ButtonChange = ButtonChange;
        }

        private void PagesLoad()
        {
            if (app.CurrentUser.Accesses.Contains(Access.ChangeName) ||
                app.CurrentUser.Accesses.Contains(Access.ChangePhone) ||
                app.CurrentUser.Accesses.Contains(Access.ChangePassport))
            {
                app.ClientChangePage = new ClientChangePage();
            }

            app.ClientInfoPage = new ClientInfoPage();
            app.MainPage = new ClientsListPage();
        }

        private void OnCreateNewClient(object sender, RoutedEventArgs e)
        {
            Page createPage = new ClientCreatePage();
            app.MainFrame.Content = createPage;
            app.CurrentPage = createPage;
            app.ButtonsForPage();
        }

        private void OnAccountExit(object sender, RoutedEventArgs e)
        {
            new StartWindow().Show();
            Close();
        }

        private void OnRemoveClient(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены что хотите удалить информацию о клиенте?",
                                         "Удаление",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Warning,
                                         MessageBoxResult.No);

            if(result == MessageBoxResult.Yes)
            {
                app.Clients.Remove(app.CurrentClient);
                DBClients.Save();

                app.MainFrame.Content = app.MainPage;
                app.CurrentPage = app.MainPage;
                app.ButtonsForPage();
            }

        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            if(app.MainFrame.Content is ClientChangePage)
            {
                app.MainFrame.Content = app.ClientInfoPage;
                app.CurrentPage = app.ClientInfoPage;
            }
            else
            {
                app.MainFrame.Content = app.MainPage;
                app.CurrentPage = app.MainPage;
            }
            
            app.ButtonsForPage();
        }

        private void OnChangeClient(object sender, RoutedEventArgs e)
        {
            app.MainFrame.Content = app.ClientChangePage;
            app.CurrentPage = app.ClientChangePage;

            (app.ClientChangePage as ClientChangePage).DataLoad();
            app.ButtonsForPage();
        }

        private void OnDoneClick(object sender, RoutedEventArgs e)
        {
            if(app.CurrentPage is ClientChangePage changePage)
            {
                changePage.DoneInput();
            }

            if (app.CurrentPage is ClientCreatePage createPage)
            {
                createPage.DoneInput();
            }
        }
    }
}
