namespace SimpleMessaging.Message.Repository.Interfaces
{
    public interface IMessageCreator
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}