using System.ComponentModel.DataAnnotations;

namespace tasknote.Models
{
    public class User {

        [Key]
        public int UserId { get; set; }
        
        public string? Name { get; set; }

    }
}