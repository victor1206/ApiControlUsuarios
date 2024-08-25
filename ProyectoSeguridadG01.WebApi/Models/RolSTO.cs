using SysSeguridad.EN;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoSeguridadG01.WebApi.Models
{
    public class RolSTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }
        List<Usuario> Usuarios { get; set; }
    }
}
