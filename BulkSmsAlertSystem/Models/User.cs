using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace BulkSmsAlertSystem.Models
{
    public class User : IdentityUser
    {
        /*public int Id { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }*/

        [Required]
        public required string Password { get; set; }
       /* public string UserName { get; internal set; }*/
    }
}
