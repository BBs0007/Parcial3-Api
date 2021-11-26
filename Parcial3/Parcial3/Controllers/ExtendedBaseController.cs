using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial3.Data;
using Parcial3.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial3.Controllers
{
    public class ExtendedBaseController<TCreation,TEntity,TDTO>: ControllerBase
        where TEntity:class ,IHaveID
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;
        private readonly string controllerName;
        public ExtendedBaseController(AppDbContext appDbContext,
            IMapper mapper, string controllerName)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
            this.controllerName = controllerName;
        }

        [HttpGet]

        public virtual async Task<ActionResult<List<TDTO>>> Get()
        {
            var entidades = await appDbContext.Set<TEntity>().ToListAsync();

            return mapper.Map<List<TDTO>>(entidades);
        }


        [HttpGet ("{id}")]
        public virtual async Task<ActionResult<TDTO>> Get(int id)
        {
            var entidad = await appDbContext.Set<TEntity>().FirstOrDefaultAsync(c => c.Id == id);

            if (entidad == null)
            {
                return NotFound();
            }

            return mapper.Map<TDTO>(entidad);

        }

        [HttpPost]
        public virtual async Task<ActionResult> Post(TCreation creationDTO)
        {
            var entidad = mapper.Map<TEntity>(creationDTO);

            appDbContext.Add(entidad);

            await appDbContext.SaveChangesAsync();

            var dto = mapper.Map<TDTO>(entidad);

            return new CreatedAtActionResult(nameof(Get), controllerName, new { id = entidad.Id }, dto);

        }


        [HttpPut("{id}")]
        public virtual async Task<ActionResult> Put(int id, TCreation creationDTO)
        {
            var entidad = await appDbContext.Set<TEntity>().FirstOrDefaultAsync(c => c.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }

            mapper.Map(creationDTO, entidad);

            appDbContext.Entry(entidad).State = EntityState.Modified;

            await appDbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(int id)
        {
            var entidad = await appDbContext.Set<TEntity>().FirstOrDefaultAsync(c => c.Id == id);

            if(entidad == null)
            {
                return NotFound();
            }
            appDbContext.Entry(entidad).State = EntityState.Deleted;

            await appDbContext.SaveChangesAsync();

            return NoContent();

        }


    }
}
