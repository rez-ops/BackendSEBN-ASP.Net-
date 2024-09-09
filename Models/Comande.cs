using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Comande
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }
  public string? TypeBon { get; set; }
  [ForeignKey("Department")]
  public int DepartmentId { get; set; }
  
  public Department? Department { get; set; } 
  public int UserId { get; set; }
  [ForeignKey(nameof(UserId))]
  public User? User { get; set; } 

  public string? TypePiece { get; set; }
  // Reflects the relationship with existing Articles
  public List<Article> Articles { get; set; } = new List<Article>();
  public DateOnly Date { get; set; } 
  
}
