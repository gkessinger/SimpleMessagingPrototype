using System.ComponentModel.DataAnnotations;

namespace SimpleMessaging.Message.API.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int CreatorId { get; set; }
        public string Body { get; set; }
        public DateTime PostDate { get; set; }
    }
}