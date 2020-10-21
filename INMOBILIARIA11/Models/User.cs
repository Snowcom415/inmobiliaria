using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INMOBILIARIA11.Models
{
    public class User
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Apellido { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]

        public string Contraseña { get; set; }

        [NotMapped]
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Contraseña")]
        public string ConfirmarContraseña { get; set; }
        public string FullName()
        {
            return this.Nombre + " " + this.Nombre;
        }

    }
}