namespace Homework_10.logic
{
    public abstract class User
    {
        public virtual string Title { get; }
        public virtual Access[] Accesses { get; }
    }

    public enum Access
    {
        ReadName,
        ReadPhone,
        ReadPassport,
        ChangeName,
        ChangePhone,
        ChangePassport,
        CreateClient,
        RemoveClient,
    }
}
