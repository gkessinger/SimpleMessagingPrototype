using System.ComponentModel.DataAnnotations;

namespace SimpleMessaging.Message.Repository.Models
{
    public class MessageCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Message> Messages { get; set; }
    }
}