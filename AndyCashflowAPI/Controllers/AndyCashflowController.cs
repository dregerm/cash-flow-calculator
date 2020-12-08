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
        public async Task<ActionResult<IEnumerable<LoanItemDTO>>> GetLoanItems()
        {
            //in here to some json format or something with all of the data
            //LoanItems is the variable with the list of loans?
            //iterate through LoanItems.
            return await _context.LoanItems
                .Select(x => LoanToDTO(x))
                .ToListAsync();
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

            return loanItem; // what it returns is the DTO version(compressed LoanItem)
            // what can we do in order to provide ID and the paymentPlan when people does get?
        }
        
        // PUT: api/AndyCashflow/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoanItem(int id, LoanItem loanItem)
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
        public async Task<ActionResult<LoanItemDTO>> CreateLoanItem(LoanItemDTO loanItemDTO)
        {
            var loanItem = new LoanItem{
                Balance = loanItemDTO.Balance,
                MonthLeft = loanItemDTO.MonthLeft,
                Rate = loanItemDTO.Rate
            };
            
            loanItem.Plan = loanPaymentPlanner(loanItemDTO);

            _context.LoanItems.Add(loanItem);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(
                nameof(GetLoanItem),
                new { id = loanItem.Id },
                LoanToDTO(loanItem));
 
        }

        // DELETE: api/AndyCashflow/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LoanItem>> DeleteLoanItem(int id)
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

        private bool LoanItemExists(int id)
        {
            return _context.LoanItems.Any(e => e.Id == id);
        }
        
        
        //bata is wondering what the person of having a static method
        private static LoanItemDTO LoanToDTO(LoanItem LoanItem) => 
        new LoanItemDTO{Balance = LoanItem.Balance, 
                        MonthLeft = LoanItem.MonthLeft,
                        Rate = LoanItem.Rate
        };
        

       private PaymentPlanItem[] loanPaymentPlanner(LoanItemDTO loanItem)
        {
            /*int monthsLeft = loanItem.MonthLeft;
            Console.WriteLine("MonthLeft: " + monthsLeft);
            decimal interestRate = loanItem.Rate;
            decimal originalBalance = loanItem.Balance;
            decimal remainingBalance = originalBalance; 
            decimal totalMonthlyPayment = getTotalMonthlyPayment(originalBalance, interestRate, monthsLeft); // equal amount is paid every month

            PaymentPlanItem[] ppiList = new PaymentPlanItem[monthsLeft]; 
            
            //loops through all months given to repay loanItem and creates a full repayment plan.
            for (int month = 0; month < monthsLeft; month++)
            {   decimal interestPayment = getInterestPayment(remainingBalance, interestRate);
                decimal principalPayment = getPrincipalPayment(totalMonthlyPayment, interestPayment);
                //above this line is previous remainingBalance
                remainingBalance = getRemainingBalance(remainingBalance, principalPayment);
                PaymentPlanItem ppi = new PaymentPlanItem(month + 1, interestPayment, principalPayment, remainingBalance);
                Console.WriteLine(ppi);
                ppiList[month] = ppi;
            }
            */
            PaymentPlanItem[] ppiList = new PaymentPlanItem[2];
            ppiList[0] = new PaymentPlanItem (1, (decimal)29, (decimal)100, (decimal)200);
            ppiList[1] = new PaymentPlanItem (2, (decimal)29, (decimal)100, (decimal)200);
   //         public PaymentPlanItem(int months, decimal interest, decimal principal, decimal remaining){  
               

            return ppiList;
        }
        
        private decimal getTotalMonthlyPayment(decimal balance, decimal rate, decimal month){
            return (balance * (rate / 1200)) / (decimal)Math.Pow((1 - ( 1 + (double)rate/ 1200)), (-1 * (double)month));
        }

        private decimal getInterestPayment(decimal remainingBalance, decimal rate){
            return (remainingBalance * rate / 1200);
        }
        private decimal getPrincipalPayment(decimal monthlyPayment, decimal interestPayment){
            return (monthlyPayment - interestPayment);
        }
        private decimal getRemainingBalance(decimal remainingBalance, decimal principalPayment){
            return (remainingBalance - principalPayment);
        }
    }
}
