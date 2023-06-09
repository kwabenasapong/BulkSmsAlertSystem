using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BulkSmsAlertSystem.Models
{
    public class SmsMessage
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Recipient")]
        public string RecipientNumber { get; set; }

        [Required]
        public string Message { get; set; }

        [Display(Name = "Date Sent")]
        public DateTime DateSent { get; set; } = DateTime.Now;
    }
}
