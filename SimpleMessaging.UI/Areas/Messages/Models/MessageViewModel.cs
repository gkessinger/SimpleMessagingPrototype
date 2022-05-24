using System.ComponentModel.DataAnnotations;

namespace SimpleMessaging.UI.Models
{
    public class MessageViewModel
    {
        public MessageViewModel()
        {

        }

        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Creator")]
        public string Creator { get; set; }
        [Display(Name = "Message")]
        public string Body { get; set; }

        [Display(Name = "Post Date")]
        public string PostDate { get; set; }
    }
}