using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class BadgeManquant
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public DateOnly Date { get; set; }
    
    public string Entre { get; set; }
    
    public string Sortie { get; set; }

    public int UserId { get; set; }

    [ForeignKey("UserId")]
    
    public User? User { get; set; }
}
