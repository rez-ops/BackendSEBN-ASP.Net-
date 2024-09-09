using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class LoginModel
{
        [Required]
        public int Matricule { get; set; }

        [Required]
        public string Password { get; set; }


        
}
