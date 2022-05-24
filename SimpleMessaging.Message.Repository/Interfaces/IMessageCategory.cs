namespace SimpleMessaging.Message.Repository.Interfaces
{
    public interface IMessageCategory
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}