using System.ComponentModel.DataAnnotations;

namespace BulkSmsAlertSystem.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
