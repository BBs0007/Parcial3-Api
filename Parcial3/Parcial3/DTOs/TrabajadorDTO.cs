using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial3.DTOs
{
    public class TrabajadorDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Especialidad { get; set; }
        public string Numero { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }
    }
}
