using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
//***********************************
using Microsoft.EntityFrameworkCore;
using SysSeguridad.EN;

namespace SysSeguridad.DAL
{
    public class DBContexto : DbContext
    {
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Data Source=VictorDuran;Initial Catalog=DbPracticaG01;Persist Security Info=True;User ID=sa;Password=Vanessa0820@1989");
            //optionsBuilder.UseSqlServer(@"workstation id = DbApiSysSeguridadG01.mssql.somee.com; packet size = 4096; user id = PruebasSmDatos; pwd = PruebaEsfe2022@; data source = DbApiSysSeguridadG01.mssql.somee.com; persist security info = False; initial catalog = DbApiSysSeguridadG01;");
            //optionsBuilder.UseSqlServer(@"workstation id=DbApiSysSeguridadG04.mssql.somee.com;packet size=4096;user id=PruebasSmDatos;pwd=PruebaEsfe2022@;data source=DbApiSysSeguridadG04.mssql.somee.com;persist security info=False;initial catalog=DbApiSysSeguridadG04;");
        }
    }
}
