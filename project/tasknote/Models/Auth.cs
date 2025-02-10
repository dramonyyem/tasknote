using System.ComponentModel.DataAnnotations;

namespace tasknote.Models
{
    public class Auth {

        [Key]
        public int AuthId { get; set; }
        
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        public string? Token { get; set; }



    }
}