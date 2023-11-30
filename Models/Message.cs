using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupSpace2023.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        [Display (Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Boodschap")]
        [Required]
        public string Body { get; set; }

        [Display(Name = "Verzonden")]
        [Required]
        public DateTime Sent {  get; set; }

        public DateTime Deleted { get; set; } = DateTime.MaxValue;

        [ForeignKey ("Groep")]
        [Display(Name = "Ontvanger")]
        public int RecipientId { get; set; } // object kan geen key zijn dus bind het aan id van groep

        [Display(Name = "Ontvanger")]
        public Groep? Recipient { get; set; }
    }
}
