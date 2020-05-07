using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Base;
using AssetManagement.Model;
using AssetManagement.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnController : BasesController<Return, ReturnRepository>
    {
        private readonly ReturnRepository _returnRepository;
        private readonly ItemRepository _itemRepository;

        public ReturnController(
            ReturnRepository returnRepository,
            ItemRepository itemRepository) : base(returnRepository)
        {
            this._returnRepository = returnRepository;
            this._itemRepository = itemRepository;
        }

        [HttpPut("ReturnDone/{item_id}")]
        public async Task<ActionResult<Return>> ReturnDone(int item_id, Return entity)
        {
            // update Return data status to 'Done'
            var putReturn = await _returnRepository.GetReturnByItemId(item_id);
            if (putReturn == null)
            {
                return NotFound();
            }
            putReturn.Return_Date = DateTimeOffset.Now;
            putReturn.Status = "Done";
            putReturn.Condition = entity.Condition;

            await _returnRepository.Put(putReturn);

            // update Item data status to 'True (Available)'
            var putItem = await _itemRepository.Get(putReturn.Item_Id);
            if (putItem == null)
            {
                return NotFound();
            }
            putItem.Status = true;

            await _itemRepository.Put(putItem);

            return Ok("Successfully updated Return data");

        }

        [HttpGet("GetReturnUser/{id}")]
        public async Task<ActionResult<ReturnVM>> GetReturnUser(int id)
        {
            var get = await _returnRepository.GetReturnUser(id);
            if (get == null)
            {
                return NotFound();
            }

            return Ok(get);
        }

        [HttpGet("GetReturnAdmin")]
        public async Task<ActionResult<ReturnVM>> GetReturnAdmin()
        {
            var get = await _returnRepository.GetReturnAdmin();
            if (get == null)
            {
                return NotFound();
            }

            return Ok(get);
        }
    }
}