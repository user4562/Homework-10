using System.Collections.ObjectModel;
using System.Text.Json;
using System.IO;

namespace Homework_10.logic
{
    internal static class DBClients
    {
        private static App app = App.Current as App;
        private static string DBClientsFile = "dbclients.json";

        public static void Load()
        {
            if (File.Exists(DBClientsFile))
            {
                string jsonString = File.ReadAllText(DBClientsFile);
                app.Clients = JsonSerializer.Deserialize<ObservableCollection<Client>>(jsonString);
            }
            else
            {
                app.Clients = new ObservableCollection<Client>();
            }
        }

        public static void Save()
        {
            User currentUser = app.CurrentUser;
            app.CurrentUser = new Manager();

            string jsonString = JsonSerializer.Serialize(app.Clients);
            app.CurrentUser = currentUser;

            File.WriteAllText(DBClientsFile, jsonString);
        }
    }
}
