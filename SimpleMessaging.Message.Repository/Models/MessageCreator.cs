using System.ComponentModel.DataAnnotations;

namespace SimpleMessaging.Message.Repository.Models
{
    public class MessageCreator
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Message> Messages { get; set; }
    }
}