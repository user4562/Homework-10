using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using Homework_10.logic;
using Homework_10.pages;

namespace Homework_10
{
    public partial class App : Application
    {
        public Frame MainFrame { get; set; }
        public Page MainPage { get; set; }
        public Page ClientInfoPage { get; set; }
        public Page ClientChangePage { get; set; }
        public Page CurrentPage { get; set; }

        public ObservableCollection<Client> Clients { get; set; }
        public User CurrentUser { get; set; }
        public Client CurrentClient { get; set; }
        public Button ButtonDone { get; set; }
        public Button ButtonBack { get; set; }
        public Button ButtonCreate { get; set; }
        public Button ButtonRemove { get; set; }
        public Button ButtonChange { get; set; }

        public App()
        {
            DBClients.Load();
            MainFrame = new Frame();
        }

        public void ButtonsForPage()
        {
            Access[] access = CurrentUser.Accesses;

            if (CurrentPage is ClientInfoPage)
            {
                ButtonCreate.Visibility = Visibility.Collapsed;
                ButtonDone.Visibility = Visibility.Collapsed;

                ButtonBack.Visibility = Visibility.Visible;

                if (access.Contains(Access.ChangeName) ||
                    access.Contains(Access.ChangePhone) ||
                    access.Contains(Access.ChangePassport))
                {
                    ButtonChange.Visibility = Visibility.Visible;
                }

                if (CurrentUser.Accesses.Contains(Access.RemoveClient))
                {
                    ButtonRemove.Visibility = Visibility.Visible;
                }
            }

            if (CurrentPage is ClientCreatePage ||
                CurrentPage is ClientChangePage)
            {
                ButtonCreate.Visibility = Visibility.Collapsed;
                ButtonRemove.Visibility = Visibility.Collapsed;
                ButtonChange.Visibility = Visibility.Collapsed;

                ButtonDone.Visibility = Visibility.Visible;
                ButtonBack.Visibility = Visibility.Visible;
            }


            if (CurrentPage is ClientsListPage)
            {
                ButtonRemove.Visibility = Visibility.Collapsed;
                ButtonChange.Visibility = Visibility.Collapsed;
                ButtonDone.Visibility = Visibility.Collapsed;
                ButtonBack.Visibility = Visibility.Collapsed;

                if (CurrentUser.Accesses.Contains(Access.CreateClient))
                {
                    ButtonCreate.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
