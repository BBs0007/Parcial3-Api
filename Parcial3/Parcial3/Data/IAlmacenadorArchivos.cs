using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial3.Data
{
    public interface IAlmacenadorArchivos
    {
        public Task<string> Crear(byte[] file, string contentType, string extension, string container, string nombre);

        public Task Borrar(string ruta, string container);
    }
}
