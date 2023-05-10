using System;
using System.Linq;
using System.Windows.Controls;

using Homework_10.logic;

namespace Homework_10.pages
{
    public partial class ClientInfoPage : Page
    {
        private App app = App.Current as App;

        public ClientInfoPage()
        {
            InitializeComponent();
        }

        public void DataLoad()
        {
            Client client = app.CurrentClient;

            bool empty = client == null;

            if (app.CurrentUser.Accesses.Contains(Access.ReadName) ||
                app.CurrentUser.Accesses.Contains(Access.ChangeName))
            {
                FirstNameText.Text = empty ? "Null" : client.FirstName;
                LastNameText.Text = empty ? "Null" : client.LastName;
                PatronymicText.Text = empty ? "Null" : client.Patronymic;
            }
            else
            {
                FirstNameText.Text = "*****";
                LastNameText.Text = "*****";
                PatronymicText.Text = "*****";
            }

            PhoneNumberText.Text = empty ? "0 (000) 000-00-00" : client.PhoneNumber.Number;
            PassportNumberText.Text = empty ? "00 00 000000" : client.PassportNumber.Number;

            DateCreate.Text = empty ? "Null" : client.DateCreate.ToString();
            DateChange.Text = empty ? "Null" : client.DateChange.ToString();
        }
    }
}
