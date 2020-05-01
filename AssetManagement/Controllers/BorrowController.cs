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
    public class BorrowController : BasesController<Borrow, BorrowRepository>
    {
        private readonly BorrowRepository _borrowRepository;
        private readonly ItemRepository _itemRepository;
        private readonly ReturnRepository _returnRepository;

        public BorrowController(
            BorrowRepository borrowRepository,
            ItemRepository itemRepository,
            ReturnRepository returnRepository) : base(borrowRepository)
        {
            this._borrowRepository = borrowRepository;
            this._itemRepository = itemRepository;
            this._returnRepository = returnRepository;
        }

        [HttpPost("PostBorrow")]
        public async Task<ActionResult<BorrowVM>> PostBorrow(BorrowVM entity)
        {
            // update status table item
            var put = await _itemRepository.Get(entity.Item_Id);
            if (put == null)
            {
                return NotFound();
            }
            put.Status = false;
            await _itemRepository.Put(put);

            // post to table borrow
            Borrow borrow = new Borrow
            {
                User_Id = entity.User_Id,
                Item_Id = entity.Item_Id,
                Status_Approval = "-",
                Borrow_Date = DateTimeOffset.Now
            };
            await _borrowRepository.Post(borrow);
            return CreatedAtAction("Get", new { id = entity.Id }, entity);

        }

        [HttpGet("GetApproval1")]
        public async Task<ActionResult<Borrow>> GetApproval1()
        {
            var get = await _borrowRepository.GetApproval1();
            if (get == null)
            {
                return NotFound();
            }

            return Ok(get);
        }

        [HttpGet("GetApproval2")]
        public async Task<ActionResult<Borrow>> GetApproval2()
        {
            var get = await _borrowRepository.GetApproval2();
            if (get == null)
            {
                return NotFound();
            }

            return Ok(get);
        }

        // Accept request App1
        [HttpPut("PutApproval1/{id}")]
        public async Task<ActionResult<Borrow>> PutApproval1(int id, Borrow entity)
        {
            var put = await _borrowRepository.Get(id);
            if (put == null)
            {
                return NotFound();
            }
            put.Approval_1 = true;

            await _borrowRepository.Put(put);
            return Ok("Successfully updated Borrow data");

        }

        // Accept request App2 and post data to Return table
        [HttpPut("PutApproval2/{id}")]
        public async Task<ActionResult<Borrow>> PutApproval2(int id, Borrow entity)
        {
            // update data status_approval to 'approved'
            var put = await _borrowRepository.Get(id);
            if (put == null)
            {
                return NotFound();
            }
            put.Approval_2 = true;
            put.Status_Approval = "Approved";

            await _borrowRepository.Put(put);

            //post data to table Return
            Return return_ = new Return
            {
                User_Id = put.User_Id,
                Item_Id = put.Item_Id,
                Status = "Not been returned"
            };

            await _returnRepository.Post(return_);
  
            return Ok("Successfully updated Borrow data");

        }

        // Decline request
        [HttpPut("DeclineApproval/{id}")]
        public async Task<ActionResult<Borrow>> DeclineApproval(int id, Borrow entity)
        {
            var put = await _borrowRepository.Get(id);
            if (put == null)
            {
                return NotFound();
            }
            put.Status_Approval = "Declined";

            await _borrowRepository.Put(put);
            return Ok("Successfully updated Borrow data");

        }

    }
}