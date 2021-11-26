using Microsoft.AspNetCore.Http;
using Parcial3.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial3.DTOs
{
    public class TrabajadorCreationDTO
    {
   
        public string Nombre { get; set; }
        public string Especialidad { get; set; }
        public string Numero { get; set; }
        public string Descripcion { get; set; }

        [ExtensionArchivo(TipoArchivo.Image)]
        [PesoArchivo(15120)]
        public IFormFile Foto { get; set; }
    }
}
