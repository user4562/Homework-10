namespace Homework_10.logic
{
    internal class Consultant : User
    {
        public override string Title { get; } = "Консультант";
        public override Access[] Accesses { get; } = new Access[] 
        { 
            Access.ReadName,
            Access.ReadPhone,
            Access.ChangePhone
        };
    }
}
