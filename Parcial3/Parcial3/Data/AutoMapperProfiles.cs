using AutoMapper;
using Parcial3.DTOs;
using Parcial3.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial3.Data
{
    public class AutoMapperProfiles :Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Trabajador, TrabajadorDTO>()
                 .ReverseMap();

            CreateMap<TrabajadorCreationDTO, Trabajador>()
                .ForMember(m => m.Foto, options => options.Ignore());
        }
    }
}
