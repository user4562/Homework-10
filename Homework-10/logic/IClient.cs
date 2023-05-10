using System;

namespace Homework_10.logic
{
    internal interface IClient
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Patronymic { get; set; }
        PhoneNumber PhoneNumber { get; set; }
        PassportNumber PassportNumber { get; set; }
        DateTime DateCreate { get; set; }
    }
}
