using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Base;
using AssetManagement.Model;
using AssetManagement.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : BasesController<Item, ItemRepository>
    {
        private readonly ItemRepository _repository;

        public ItemController(ItemRepository repository) : base(repository)
        {
            this._repository = repository;
        }

        [HttpPost("PostItem")]
        public async Task<ActionResult<Item>> PostItem(Item entity)
        {
            entity.Status = true;
            entity.Create_Date = DateTimeOffset.Now;
            entity.Is_Delete = false;
            await _repository.Post(entity);
            return CreatedAtAction("Get", new { id = entity.Id }, entity);

        }

        [HttpGet("GetItemAdmin")]
        public async Task<ActionResult<Item>> GetItemAdmin()
        {
            var get = await _repository.GetItemAdmin();
            return Ok(new { data = get });
        }

        [HttpGet("GetItemUser")]
        public async Task<ActionResult<Item>> GetItemUser()
        {
            var get = await _repository.GetItemUser();
            return Ok(new { data = get });
        }

        [HttpPut("PutItem/{id}")]
        public async Task<ActionResult<Item>> PutItem(int id, Item entity)
        {
            var put = await _repository.Get(id);
            if (put == null)
            {
                return BadRequest();
            }
            if (entity.Brand != null)
            {
                put.Brand = entity.Brand;
            }
            if (entity.Cpu != null)
            {
                put.Cpu= entity.Cpu;
            }
            if (entity.Gpu != null)
            {
                put.Gpu = entity.Gpu;
            }
            if (entity.Ram != null)
            {
                put.Ram = entity.Ram;
            }
            if (entity.Display != null)
            {
                put.Display = entity.Display;
            }
            if (entity.Storage != null)
            {
                put.Storage = entity.Storage;
            }
            if (entity.Os != null)
            {
                put.Os = entity.Os;
            }

            put.Update_Date = DateTimeOffset.Now;
            await _repository.Put(put);
            return Ok("Update Succesfully");
        }

        [HttpPut("DeleteItem/{id}")]
        public async Task<ActionResult<Item>> DeleteItem(int id, Item entity)
        {
            var delete = await _repository.Get(id);
            //var delete = await _repository.Delete(id);
            if (delete == null)
            {
                return NotFound();
            }
            delete.Delete_Date = DateTimeOffset.Now;
            delete.Is_Delete = true;
            await _repository.Put(delete);
            return Ok("Delete Succesfully");
        }
    }
}