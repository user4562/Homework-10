using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

using Homework_10.logic;

namespace Homework_10.pages
{
    public partial class ClientsListPage : Page
    {
        public App app = App.Current as App;
        public ObservableCollection<Client> ClientsList { get; private set; }

        public ClientsListPage()
        {
            TextVisible();

            InitializeComponent();
        }

        private void TextVisible()
        {
            ClientsList = app.Clients;

            if (!(app.CurrentUser.Accesses.Contains(Access.ReadName) ||
                  app.CurrentUser.Accesses.Contains(Access.ChangeName)))
            {
                for (int i = 0; i < ClientsList.Count; i++)
                {
                    ClientsList[i].FirstName = "*****";
                    ClientsList[i].LastName = "*****";
                    ClientsList[i].Patronymic = "*****";
                }
            }
        }

        private void ViewClientInfo(object sender, MouseButtonEventArgs e)
        {
            ListView list = sender as ListView;

            if(list.SelectedItem != null)
            {
                app.CurrentClient = list.SelectedItem as Client;
                app.MainFrame.Content = app.ClientInfoPage;
                app.CurrentPage = app.ClientInfoPage;

                (app.ClientInfoPage as ClientInfoPage).DataLoad();
                app.ButtonsForPage();
            }
        }
    }
}
