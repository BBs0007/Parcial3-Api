using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial3.ValidationAttributes
{
    public class PesoArchivoAttribute :ValidationAttribute
    {
        private readonly double pesoArchivokb;
        public PesoArchivoAttribute(double pesoArchivokb)
        {
            this.pesoArchivokb = pesoArchivokb;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var formfile = value as IFormFile;

            if (formfile != null)
            {
                if (formfile.Length / 1024 > pesoArchivokb)
                {
                    return new ValidationResult($"El peso maximo para el archivo que envias es de {pesoArchivokb} " +
                        $"sin embargo has enviado un archivo con {formfile.Length*1024}");
                }
            }
            return ValidationResult.Success;
        }



    }
}
