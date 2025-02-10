using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tasknote.Models
{
    public class Note {
        
        [Key]
        public int NoteId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}