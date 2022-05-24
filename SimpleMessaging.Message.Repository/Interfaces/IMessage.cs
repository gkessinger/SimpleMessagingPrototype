namespace SimpleMessaging.Message.Repository.Models
{
    public interface IMessage
    {
        int Id { get; set; }
        MessageCategory Category { get; set; }
        MessageCreator Creator { get; set; }
        string Body { get; set; }
        DateTime PostDate { get; set; }
    }
}