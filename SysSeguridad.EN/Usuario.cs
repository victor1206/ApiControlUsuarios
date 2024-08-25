using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//******************************
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysSeguridad.EN
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Rol")]
        [Required(ErrorMessage = "Rol es Obligatorio")]
        [Display(Name = "Rol")]
        public int IdRol { get; set; }
        [Required(ErrorMessage = "Nombre es Obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo 30 Caracteres")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Apellido es Obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo 30 Caracteres")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Nombre Usuario es Obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo 30 Caracteres")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password es Obligatorio")]
        [DataType(DataType.Password)]
        [StringLength(33, ErrorMessage = "Password debe estar entre 5 a 33 caracteres", MinimumLength =5)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Estatus es Obligatorio")]
        public byte Estatus { get; set; }
        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }
        public Rol Rol { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Confirmar Password es Obligatorio")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password y Confirmar Password deben ser igualew")]
        [StringLength(33, ErrorMessage = "Password debe estar entre 5 a 33 caracteres", MinimumLength = 5)]
        [Display(Name = "Confirmar Password")]
        public string ConfirmPassword_Aux { get; set; }
    }

    public enum Estatus_Usuario
    { 
        ACTIVO = 1,
        INACTIVO = 2
    }
}
