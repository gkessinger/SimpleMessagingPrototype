using System.ComponentModel.DataAnnotations;

namespace SimpleMessaging.Message.Repository.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public virtual MessageCategory Category { get; set; }
        public int CreatorId { get; set; }
        public virtual MessageCreator Creator { get; set; }
        public string Body { get; set; }
        public DateTime PostDate { get; set; }
    }
}