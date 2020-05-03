using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Base
{
    public class BasesController<TEntity, TRepository> : ControllerBase
        where TEntity : class, IEntity
        where TRepository : IRepository<TEntity>
    {
        private readonly TRepository _requestRepository;

        public BasesController(TRepository repository) { this._requestRepository = repository; }

        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity entity)
        {
            await _requestRepository.Post(entity);
            return CreatedAtAction("Get", new { id = entity.Id }, entity);

        }

        [HttpGet]
        public async Task<ActionResult<TEntity>> Get()
        {
            var get = await _requestRepository.Get();
            return Ok(new { data = get });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            var get = await _requestRepository.Get(id);
            if (get == null)
            {
                return NotFound();
            }
            return Ok(get);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TEntity>> Put(int id, TEntity entity)
        {
            var put = await _requestRepository.Get(id);
            if (put == null)
            {
                return NotFound();
            }
            put = entity;
            await _requestRepository.Put(put);
            return Ok("Successfully updated data");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            var delete = await _requestRepository.Delete(id);
            if (delete == null)
            {
                return NotFound();
            }
            
            return delete;
        }

    }
}