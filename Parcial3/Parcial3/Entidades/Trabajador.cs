using Parcial3.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial3.Entidades
{
    public class Trabajador :IHaveID
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Especialidad { get; set; }
        public string Numero { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }
    }
}
