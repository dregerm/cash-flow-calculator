using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndyCashflowAPI.Models;

namespace AndyCashflowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AndyCashflowController : ControllerBase
    {
        private readonly LoanContext _context;

        public AndyCashflowController(LoanContext context)
        {
            _context = context;
        }

        // GET: api/AndyCashflow
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanItem>>> GetLoanItems()
        {
            //in here to some json format or something with all of the data
            //LoanItems is the variable with the list of loans?
            //iterate through LoanItems. 
            return await _context.LoanItems.ToListAsync();
        }

        // GET: api/AndyCashflow/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanItem>> GetLoanItem(long id)
        {
            var loanItem = await _context.LoanItems.FindAsync(id);

            if (loanItem == null)
            {
                return NotFound();
            }

            return loanItem;
        }

        // PUT: api/AndyCashflow/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoanItem(long id, LoanItem loanItem)
        {
            if (id != loanItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(loanItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AndyCashflow
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<LoanItem>> PostLoanItem(LoanItem loanItem)
        {
            loanPaymentPlanner(loanItem);
            _context.LoanItems.Add(loanItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoanItem", new { id = loanItem.Id }, loanItem);
        }

        // DELETE: api/AndyCashflow/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LoanItem>> DeleteLoanItem(long id)
        {
            var loanItem = await _context.LoanItems.FindAsync(id);
            if (loanItem == null)
            {
                return NotFound();
            }

            _context.LoanItems.Remove(loanItem);
            await _context.SaveChangesAsync();

            return loanItem;
        }

        private bool LoanItemExists(long id)
        {
            return _context.LoanItems.Any(e => e.Id == id);
        }

        private void loanPaymentPlanner(LoanItem loanItem)
        {
            PaymentPlanItem[] otherList = new PaymentPlanItem[loanItem.MonthLeft];
            long length = loanItem.MonthLeft;
            List<string> AuthorList = new List<string>();

            for (int i = 0; i < length; i++)
            {
                //long interest = 1 + 1;    
                //otherList[i] = new PaymentPlanItem(interest=interest)
                otherList[i].month = i;
                otherList[i].remainingBalance = 29384239;

                Console.WriteLine(i);

            }



            //public DbSet<LoanItem> LoanItems { get; set; }
        }
    }
}
