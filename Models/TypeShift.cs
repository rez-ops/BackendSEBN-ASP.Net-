using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class TypeShift
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string TypeName { get; set; }
    public string DebutHeure { get; set; }
    public string FinHeure { get; set; }
   

}
