using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Base;
using AssetManagement.Model;
using AssetManagement.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[Controller]")]
    [ApiController]
    public class RequestController : BasesController<Request, RequestRepository>
    {
        private readonly RequestRepository _requestRepository;
        private readonly ItemRepository _itemRepository;

        public RequestController(
            RequestRepository requestRepository,
            ItemRepository itemRepository) : base(requestRepository)
        {
            this._requestRepository = requestRepository;
            this._itemRepository = itemRepository;
        }

        [HttpPost("PostRequest")]
        public async Task<ActionResult<Request>> PostRequest(Request entity)
        {
            entity.Request_Date = DateTimeOffset.Now;
            entity.Status_Approval = "Waiting";
            await _requestRepository.Post(entity);
            return CreatedAtAction("Get", new { id = entity.Id }, entity);

        }

        [HttpPost("AddRequestItem")]
        public async Task<ActionResult<RequestVM>> AddRequestItem(RequestVM entity)
        {
            // update status to added
            var put = await _requestRepository.Get(entity.Id);
            put.Status_Approval = "Added";
            await _requestRepository.Put(put);

            // add item to item table
            Item item = new Item
            {
                Status = true,
                Create_Date = DateTimeOffset.Now,
                Is_Delete = false,
                Brand = entity.Brand,
                Cpu = entity.Cpu,
                Gpu = entity.Gpu,
                Ram = entity.Ram,
                Display = entity.Display,
                Storage = entity.Storage,
                Os = entity.Os
            };
            
            await _itemRepository.Post(item);
            return CreatedAtAction("Get", new { id = entity.Id }, entity);

        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<RequestVM>> GetById(int id)
        {
            var get = await _requestRepository.GetById(id);
            if (get == null)
            {
                return NotFound();
            }

            return Ok(get);
        }

        [HttpGet("GetAdmin")]
        public async Task<ActionResult<RequestVM>> GetAdmin()
        {
            var get = await _requestRepository.GetAdmin();
            if (get == null)
            {
                return NotFound();
            }

            return Ok(get);
        }

        // get for request view user
        [HttpGet("GetByUserId/{user_id}")]
        public async Task<ActionResult<Request>> GetByUserId(int user_id)
        {
            var get = await _requestRepository.GetByUserId(user_id);
            if (get == null)
            {
                return NotFound();
            }
            return Ok(get);
            //return get;
        }

        [HttpGet("GetApproval1")]
        public async Task<ActionResult<RequestVM>> GetApproval1()
        {
            var get = await _requestRepository.GetApproval1();
            if (get == null)
            {
                return NotFound();
            }

            return Ok(get);
        }

        [HttpGet("GetApproval2")]
        public async Task<ActionResult<RequestVM>> GetApproval2()
        {
            var get = await _requestRepository.GetApproval2();
            if (get == null)
            {
                return NotFound();
            }

            return Ok(get);
        }

        // Accept request App1
        [HttpPut("PutApproval1/{id}")]
        public async Task<ActionResult<Request>> PutApproval1(int id, Request entity)
        {
            var put = await _requestRepository.Get(id);
            if (put == null)
            {
                return NotFound();
            }
            put.Approval_1 = true;

            await _requestRepository.Put(put);
            return Ok("Successfully updated Request data");

        }

        // Accept request App2
        [HttpPut("PutApproval2/{id}")]
        public async Task<ActionResult<Request>> PutApproval2(int id, Request entity)
        {
            var put = await _requestRepository.Get(id);
            if (put == null)
            {
                return NotFound();
            }
            put.Approval_2 = true;
            put.Status_Approval = "Approved";

            await _requestRepository.Put(put);
            return Ok("Successfully updated Request data");

        }

        // Decline request
        [HttpPut("DeclineApproval/{id}")]
        public async Task<ActionResult<Request>> DeclineApproval(int id, Request entity)
        {
            var put = await _requestRepository.Get(id);
            if (put == null)
            {
                return NotFound();
            }
            put.Status_Approval = "Declined";

            await _requestRepository.Put(put);
            return Ok("Successfully updated Request data");

        }
    }
}