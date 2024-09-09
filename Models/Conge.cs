using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Conge
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateOnly DateSortie { get; set; }

        [Required]
        public DateOnly DateEntre { get; set; }
        [Required]

        public int UserId { get; set; }
        
        // Explicit foreign key relationship
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; } 

        [Required]
        public int IdTypeConge { get; set; }

        // Explicit foreign key relationship
        [ForeignKey(nameof(IdTypeConge))]
        public TypeConge? TypeConge { get; set; } 

        
    }
}
