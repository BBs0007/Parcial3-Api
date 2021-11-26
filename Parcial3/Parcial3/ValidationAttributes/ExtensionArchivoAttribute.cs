using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial3.ValidationAttributes
{
    public class ExtensionArchivoAttribute : ValidationAttribute
    {
        private readonly string[] tiposValidos;

        public ExtensionArchivoAttribute(string[]tiposValidos)
        {
            this.tiposValidos = tiposValidos;
        }




        public ExtensionArchivoAttribute(TipoArchivo tipoArchivo)
        {
            if (tipoArchivo == TipoArchivo.Image)
            {
                tiposValidos = new[] { "image/png", "image/jpg", "image/gif" };

            }

        }



        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var formfile = value as IFormFile;
            if (formfile != null)
            {
                if (!tiposValidos.Contains(formfile.ContentType))
                {
                    return new ValidationResult($"Los tipos validos son {string.Join(",", tiposValidos)}");
                }


            }
            return ValidationResult.Success;
        }
    }
}
