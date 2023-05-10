using System;
using System.Collections.Generic;

namespace Homework_10.logic
{
    public class Client : IClient
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public PassportNumber PassportNumber { get; set; }

        public DateTime DateCreate { get; set; }
        public DateTime DateChange { get; set; }

        public string WhoChange { get; set; }

        public Client(string firstName, string lastName, string patronymic, 
                      PhoneNumber phoneNumber, PassportNumber passportNumber,
                      DateTime dateCreate, DateTime dateChange)
        {            
            if (!CheckName(firstName))
                throw new ArgumentException("First name have a wrong symbol/s");
            if (!CheckName(lastName))
                throw new ArgumentException("Last name have a wrong symbol/s");
            if (!CheckName(patronymic))
                throw new ArgumentException("Patronymic have a wrong symbol/s");

            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            PhoneNumber = phoneNumber;
            PassportNumber = passportNumber;

            DateCreate = dateCreate;
            DateChange = dateChange;
        }

        public static bool CheckName(string name)
        {
            if (name == "First Name" || name == "Last Name" || name == "Patronymic" ||
                name.Length == 0) return false;
                
            foreach (char symbol in name)
            {
                if (!char.IsLetter(symbol)) return false;
            }
            return true;
        }
    }

    // Temp
    internal class ClientRandom
    {
        private static readonly string[] FirstNames = new string[]
{
        "Николай", "Руслан", "Алексей", "Юрий", "Ярослав", "Семен", "Евгений", "Олег",
        "Артур", "Петр", "Степан", "Илья", "Вячеслав", "Сергей", "Василий", "Степан",
        "Федор", "Стас", "Вячеслав", "Георгий", "Антон", "Борис", "Захар", "Арсений"
};

        private static readonly string[] LastNames = new string[]
        {
        "Смирнов", "Иванов", "Кузнецов", "Соколов", "Попов", "Лебедев", "Козлов", "Новиков",
        "Морозов", "Петров", "Волков", "Соловьёв", "Васильев", "Зайцев", "Павлов", "Семёнов",
        "Голубев", "Виноградов", "Богданов", "Воробьёв", "Фёдоров", "Михайлов", "Беляев", "Тарасов"
        };

        private static readonly string[] Patronymics = new string[]
        {
        "Александрович", "Алексеевич", "Анатольевич", "Андреевич", "Антонович", "Аркадьевич", "Артемович",
        "Бедросович", "Богданович", "Борисович", "Валентинович", "Валерьевич", "Васильевич", "Викторович",
        "Витальевич", "Владимирович", "Владиславович", "Вольфович", "Вячеславович", "Геннадиевич",
        "Георгиевич", "Григорьевич", "Данилович", "Денисович", "Дмитриевич", "Евгеньевич", "Егорович"
        };

        private static readonly Random _rand = new Random();

        public static Client Create()
        {
            string firstName = FirstNames[_rand.Next(FirstNames.Length - 1)];
            string lastName = LastNames[_rand.Next(LastNames.Length - 1)];
            string patronymic = Patronymics[_rand.Next(Patronymics.Length - 1)];

            PhoneNumber phone = new PhoneNumber($"{(_rand.Next(10) < 8 ? "+" : "")}" +
                                                $"{_rand.Next(1_111, 9_999)}" +
                                                $"{_rand.Next(1111111, 9999999)}");

            PassportNumber passport = new PassportNumber((uint)_rand.Next(99),
                                                         (uint)_rand.Next(99),
                                                         (uint)_rand.Next(999999));

            return new Client(firstName, lastName, patronymic, phone, passport, DateTime.Now, DateTime.Now);
        }

        public static List<Client> Generate(int count)
        {
            List<Client> clients = new List<Client>();

            for (int i = 0; i < count; i++)
            {
                clients.Add(ClientRandom.Create());
            }

            return clients;
        }
    }
}
