using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Homework_10.logic;

namespace Homework_10.pages
{
    public partial class ClientCreatePage : Page
    {
        private App app = App.Current as App;

        public ClientCreatePage()
        {
            if (!app.CurrentUser.Accesses.Contains(Access.CreateClient))
                throw new UnauthorizedAccessException("Неправомерный доступ");

            InitializeComponent();
        }

        public void DoneInput()
        {
            bool done = true;

            string lastName = LastNameInput.Text;
            string firstName = FirstNameInput.Text;
            string patronymic = PatronymicInput.Text;

            PhoneNumber phone;
            PassportNumber passport;

            if (!Client.CheckName(lastName))
            {
                LastNameInput.BorderBrush = Brushes.Red;
                done = false;
            }

            if (!Client.CheckName(firstName))
            {
                FirstNameInput.BorderBrush = Brushes.Red;
                done = false;
            }

            if (!Client.CheckName(patronymic))
            {
                PatronymicInput.BorderBrush = Brushes.Red;
                done = false;
            }

            if (PhoneNumber.IsPhoneNumber(PhoneNumberInput.Text))
            {
                phone = new PhoneNumber(PhoneNumberInput.Text);
            }
            else
            {
                PhoneNumberInput.BorderBrush = Brushes.Red;
                phone = PhoneNumber.Empty;
                done = false;
            } 

            if (PassportNumber.IsPassportNumber(PassportNumberInput.Text))
            {
                passport = new PassportNumber(PassportNumberInput.Text);
            }
            else
            {
                PassportNumberInput.BorderBrush = Brushes.Red;
                passport = PassportNumber.Empty;
                done = false;
            }

            if (done)
            {
                Client newClient = new Client(firstName, lastName, 
                                              patronymic, phone, passport, 
                                              DateTime.Now, DateTime.Now);

                app.Clients.Insert(0, newClient);

                DBClients.Save();

                app.MainFrame.Content = app.MainPage;
                app.CurrentPage = app.MainPage;

                app.ButtonsForPage();
            }
        }

        private string _focusString = "";

        private void FocusInput(object sender, RoutedEventArgs e)
        {
            TextBox input = (sender as TextBox);

            if(input.Text == "First Name" || 
               input.Text == "Last Name" || 
               input.Text == "Patronymic" ||
               input.Text == "Phone Number" ||
               input.Text == "Passport Number")
            {
                _focusString = input.Text;
                input.Text = "";
            }      
        }

        private void UnFocusInput(object sender, RoutedEventArgs e)
        {
            TextBox input = (sender as TextBox);

            if(input.Text.Length == 0) input.Text = _focusString;
        }

        private void OnWriteInput(object sender, RoutedEventArgs e)
        {
            TextBox input = (sender as TextBox);
            input.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }
    }
}
