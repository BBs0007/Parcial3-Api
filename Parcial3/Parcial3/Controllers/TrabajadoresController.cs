using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial3.Data;
using Parcial3.DTOs;
using Parcial3.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial3.Controllers
{
    [ApiController]
    [Route("api/trabajadores")]
    public class TrabajadoresController : ExtendedBaseController<TrabajadorCreationDTO,Trabajador,TrabajadorDTO>
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;

        public TrabajadoresController(AppDbContext appDbContext,IMapper mapper,IAlmacenadorArchivos almacenadorArchivos)
            :base(appDbContext,mapper,"Trabajadores")
        {
            this.almacenadorArchivos = almacenadorArchivos;
            this.appDbContext = appDbContext;
            this.mapper = mapper;
            
        }

       

        public async override Task<ActionResult> Post([FromForm]TrabajadorCreationDTO creationDTO)
        {
            var entity = mapper.Map<Trabajador>(creationDTO);

            if(creationDTO.Foto != null)
            {
                string fotoUrl = await GuardarFoto(creationDTO.Foto);
                entity.Foto = fotoUrl;

            }

            appDbContext.Add(entity);

            await appDbContext.SaveChangesAsync();

            var dto = mapper.Map<TrabajadorDTO>(entity);

            return new CreatedAtActionResult(nameof(Get), "Trabajadores", new { id = entity.Id }, dto);
           
        }


        public async override Task<ActionResult> Put(int id, [FromForm]TrabajadorCreationDTO creationDTO)
        {

            var entity = await appDbContext.Trabajadores.FirstOrDefaultAsync(a => a.Id == id);

            if (entity == null)
            {
                return NotFound();
            }

            mapper.Map(creationDTO, entity);

            if (creationDTO.Foto != null)
            {
                if (!string.IsNullOrEmpty(entity.Foto))
                {
                    await almacenadorArchivos.Borrar(entity.Foto, ConstantesDeAplicacion.ContenedoresDeArchivos.ContenedorDeTrabajadores);
                }
                string fotoUrl = await GuardarFoto(creationDTO.Foto);
                entity.Foto = fotoUrl;
            }

            appDbContext.Entry(entity).State = EntityState.Modified;

            await appDbContext.SaveChangesAsync();

            return NoContent();
        }

        private async Task<string> GuardarFoto(IFormFile foto)
        {
            using var stream = new MemoryStream();

            await foto.CopyToAsync(stream);

            var fileBytes = stream.ToArray();

            return await almacenadorArchivos
                .Crear(fileBytes, foto.ContentType, Path.GetExtension(foto.FileName), ConstantesDeAplicacion.ContenedoresDeArchivos.ContenedorDeTrabajadores, Guid.NewGuid().ToString());
        }
    }
}
