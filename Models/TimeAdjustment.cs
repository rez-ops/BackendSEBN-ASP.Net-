using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class TimeAdjustment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
      
        public string? duHeure { get; set; }
        
        public string? auHeure { get; set; }
        public int IdTypeShift { get; set; }

        // Propriété de navigation pour TypeShift
        [ForeignKey("IdTypeShift")]
        public TypeShift? TypeShift { get; set; }

        public int UserId { get; set; }

        // Propriété de navigation pour User
        [ForeignKey("UserId")]
        public User? User { get; set; }

        public DateOnly Date { get; set; }
    }
}
