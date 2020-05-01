using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Base;
using AssetManagement.Model;
using AssetManagement.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class RequestController : BasesController<Request, RequestRepository>
    {
        private readonly RequestRepository _repository;

        public RequestController(RequestRepository requestRepository) : base(requestRepository)
        {
            this._repository = requestRepository;
        }

        [HttpPost("PostRequest")]
        public async Task<ActionResult<Request>> PostRequest(Request entity)
        {
            entity.Request_Date = DateTimeOffset.Now;
            entity.Status_Approval = "-";
            await _repository.Post(entity);
            return CreatedAtAction("Get", new { id = entity.Id }, entity);

        }

        [HttpGet("GetApproval1")]
        public async Task<ActionResult<Request>> GetApproval1()
        {
            var get = await _repository.GetApproval1();
            if (get == null)
            {
                return NotFound();
            }

            return Ok(get);
        }

        [HttpGet("GetApproval2")]
        public async Task<ActionResult<Request>> GetApproval2()
        {
            var get = await _repository.GetApproval2();
            if (get == null)
            {
                return NotFound();
            }

            return Ok(get);
        }

        //// Update for Admin
        //[HttpPut("PutRequest/{id}")]
        //public async Task<ActionResult<Request>> PutRequest(int id, Request entity)
        //{
        //    var put = await _repository.Get(id);
        //    if (put == null)
        //    {
        //        return NotFound();
        //    }
        //    if (entity.Specification != null)
        //    {
        //        put.Specification = entity.Specification;
        //    }
        //    if (entity.Item_Name != null)
        //    {
        //        put.Item_Name = entity.Item_Name;
        //    }

        //    await _repository.Put(put);
        //    return Ok("Successfully updated Request data");

        //}

        // Accept request App1
        [HttpPut("PutApproval1/{id}")]
        public async Task<ActionResult<Request>> PutApproval1(int id, Request entity)
        {
            var put = await _repository.Get(id);
            if (put == null)
            {
                return NotFound();
            }
            put.Approval_1 = true;

            await _repository.Put(put);
            return Ok("Successfully updated Request data");

        }

        // Accept request App2
        [HttpPut("PutApproval2/{id}")]
        public async Task<ActionResult<Request>> PutApproval2(int id, Request entity)
        {
            var put = await _repository.Get(id);
            if (put == null)
            {
                return NotFound();
            }
            put.Approval_2 = true;
            put.Status_Approval = "Approved";

            await _repository.Put(put);
            return Ok("Successfully updated Request data");

        }

        // Decline request
        [HttpPut("DeclineApproval/{id}")]
        public async Task<ActionResult<Request>> DeclineApproval(int id, Request entity)
        {
            var put = await _repository.Get(id);
            if (put == null)
            {
                return NotFound();
            }
            put.Status_Approval = "Declined";

            await _repository.Put(put);
            return Ok("Successfully updated Request data");

        }
    }
}