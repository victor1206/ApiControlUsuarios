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
    public class Rol
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nombre es Obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo 30 Caracteres")]
        public string Nombre { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }
        List<Usuario> Usuarios { get;set; }
    }
}
