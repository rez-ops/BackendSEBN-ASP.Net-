using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class User
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nom { get; set; }
        public string Prenom { get; set; }
        // doit etre unique !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public int Matricule { get; set; }
        public char Shift { get; set; }
        public string Direction { get; set; }
        public string Zone { get; set; }
        
        public string Password { get; set; } 
        public string Role { get; set; }
        public int TotalLeaveDaysTaken { get; set; }=0;
        public int MaxLeaveDaysPerYear { get; set; } = 21;
        public string Token { get; set; }
    }
}
