namespace Homework_10.logic
{
    internal class Manager : User
    {
        public override string Title { get; } = "Менеджер";
        public override Access[] Accesses { get; } = new Access[]
        {
            Access.ReadName,
            Access.ReadPhone,
            Access.ReadPassport,
            Access.ChangeName,
            Access.ChangePhone,
            Access.ChangePassport,
            Access.CreateClient,
            Access.RemoveClient
        };
    }
}
