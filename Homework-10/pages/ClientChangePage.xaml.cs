using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Homework_10.logic;

namespace Homework_10.pages
{
    public partial class ClientChangePage : Page
    {
        private App app = App.Current as App;

        public ClientChangePage()
        {
            InitializeComponent();
        }

        public void DoneInput()
        {
            Access[] accesses = app.CurrentUser.Accesses;

            if (app.CurrentClient == null) throw new NullReferenceException("Не выбран клиент");

            bool done = true;

            if (accesses.Contains(Access.ChangeName))
            {
                if (app.CurrentClient.FirstName != FirstNameInput.Text)
                {
                    if (!Client.CheckName(FirstNameInput.Text))
                    {
                        FirstNameInput.BorderBrush = Brushes.Red;
                        done = false;
                    }
                    else
                    {
                        app.CurrentClient.FirstName = FirstNameInput.Text;
                    }
                }

                if (app.CurrentClient.LastName != LastNameInput.Text)
                {
                    if (!Client.CheckName(LastNameInput.Text))
                    {
                        LastNameInput.BorderBrush = Brushes.Red;
                        done = false;
                    }
                    else
                    {
                        app.CurrentClient.LastName = LastNameInput.Text;
                    }
                }

                if (app.CurrentClient.Patronymic != PatronymicInput.Text)
                {
                    if (!Client.CheckName(PatronymicInput.Text))
                    {
                        PatronymicInput.BorderBrush = Brushes.Red;
                        done = false;
                    }
                    else
                    {
                        app.CurrentClient.Patronymic = PatronymicInput.Text;
                    }
                }
            }     

            if(accesses.Contains(Access.ChangePassport) &&
               app.CurrentClient.PassportNumber.Number != PassportNumberInput.Text)
            {
                if(PassportNumber.IsPassportNumber(PassportNumberInput.Text))
                {
                    app.CurrentClient.PassportNumber = new PassportNumber(PassportNumberInput.Text);
                }
                else
                {
                    PassportNumberInput.BorderBrush = Brushes.Red;
                    done = false;
                }
            }

            if (accesses.Contains(Access.ChangePhone) &&
                app.CurrentClient.PhoneNumber.Number != PhoneNumberInput.Text)
            {
                if (PhoneNumber.IsPhoneNumber(PhoneNumberInput.Text))
                {
                    app.CurrentClient.PhoneNumber = new PhoneNumber(PhoneNumberInput.Text);
                }
                else
                {
                    PhoneNumberInput.BorderBrush = Brushes.Red;
                    done = false;
                }
            }

            if(done)
            {
                app.CurrentClient.DateChange = DateTime.Now;
                app.CurrentClient.WhoChange = app.CurrentUser.Title;

                DBClients.Save();


                app.MainPage = new ClientsListPage();

                app.MainFrame.Content = app.ClientInfoPage;
                app.CurrentPage = app.ClientInfoPage;

                (app.ClientInfoPage as ClientInfoPage).DataLoad();
                app.ButtonsForPage();
            }
        }

        public void DataLoad()
        {
            Client client = app.CurrentClient;

            bool empty = client == null;

            string friastName = empty ? "Null" : client.FirstName;
            string lastName = empty ? "Null" : client.LastName;
            string patronymic = empty ? "Null" : client.Patronymic;

            string phoneNumber = empty ? "0 (000) 000-00-00" : client.PhoneNumber.Number;
            string passportNumber = empty ? "00 00 000000" : client.PassportNumber.Number;

            string dateCreate = empty ? "Null" : client.DateCreate.ToString();
            string dateChange = empty ? "Null" : client.DateChange.ToString();

            if (app.CurrentUser.Accesses.Contains(Access.ReadName))
            {
                FirstNameText.Text = friastName;
                LastNameText.Text = lastName;
                PatronymicText.Text = patronymic;

                if (app.CurrentUser.Accesses.Contains(Access.ChangeName))
                {
                    FirstNameInput.Text = friastName;
                    LastNameInput.Text = lastName;
                    PatronymicInput.Text = patronymic;

                    FirstNameText.Visibility = Visibility.Collapsed;
                    LastNameText.Visibility = Visibility.Collapsed;
                    PatronymicText.Visibility = Visibility.Collapsed;

                    FirstNameInput.Visibility = Visibility.Visible;
                    LastNameInput.Visibility = Visibility.Visible;
                    PatronymicInput.Visibility = Visibility.Visible;
                }
            }
            else
            {
                FirstNameText.Text = "*****";
                LastNameText.Text = "*****";
                PatronymicText.Text = "*****";
            }

            PhoneNumberText.Text = phoneNumber;
            PassportNumberText.Text = passportNumber;

            if (app.CurrentUser.Accesses.Contains(Access.ChangePhone))
            {
                PhoneNumberInput.Text = phoneNumber;

                PhoneNumberText.Visibility = Visibility.Collapsed;
                PhoneNumberInput.Visibility = Visibility.Visible;
            }

            if (app.CurrentUser.Accesses.Contains(Access.ChangePassport))
            {
                PassportNumberInput.Text = passportNumber;

                PassportNumberText.Visibility = Visibility.Collapsed;
                PassportNumberInput.Visibility = Visibility.Visible;
            }

            DateCreate.Text = dateCreate;
            DateChange.Text = dateChange;

            if (!empty && client.WhoChange != null)
            {
                WhoChangeLabel.Visibility = System.Windows.Visibility.Visible;
                WhoChange.Text = client.WhoChange;
            }
        }

        private void OnWriteInput(object sender, RoutedEventArgs e)
        {
            TextBox input = (sender as TextBox);
            input.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }
    }
}
