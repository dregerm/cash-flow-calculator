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
        public async Task<ActionResult<IEnumerable<PaymentPlanItem>>> GetPaymentPlanItems()
        {
            List<PaymentPlanItem> ppi = new List<PaymentPlanItem>();
            this.ppi = await context.PaymentPlanItems.Select(x => x.LoanId == this.loanItem.Id).ToListAsync();
            PaymentPlanComposite ppc = new PaymentPlanComposite();
            return await _context.PaymentPlanItems
                //.Select(x => LoanToDTO(x)) //DO WE WANT T0 PROVIDE USERS WITH LOAN OR LOANDTO? WE COULD HAVE //.Select(x => x)
                .ToListAsync();
            .
            //return new PaymentPlanComposite();
            
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
                    
            _context.LoanItems.Add(loanItem);
            await _context.SaveChangesAsync();
            
            loanPaymentPlanner(loanItem);
            
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
        

       private void loanPaymentPlanner(LoanItem loanItem)
        {
            int monthsLeft = loanItem.MonthLeft;
            Console.WriteLine("MonthLeft: " + monthsLeft);
            decimal interestRate = loanItem.Rate;
            decimal originalBalance = loanItem.Balance;
            decimal remainingBalance = originalBalance; 
            decimal totalMonthlyPayment = getTotalMonthlyPayment(originalBalance, interestRate, monthsLeft); // equal amount is paid every month

            List<PaymentPlanItem> ppiList = new List<PaymentPlanItem>(); 
            
            //loops through all months given to repay loanItem and creates a full repayment plan.
            for (int month = 0; month < monthsLeft; month++)
            {   decimal interestPayment = getInterestPayment(remainingBalance, interestRate);
                decimal principalPayment = getPrincipalPayment(totalMonthlyPayment, interestPayment);
                //above this line is previous remainingBalance
                remainingBalance = getRemainingBalance(remainingBalance, principalPayment);
                PaymentPlanItem ppi = new PaymentPlanItem(loanItem.Id, month + 1, interestPayment, principalPayment, remainingBalance);
                Console.WriteLine(ppi);
                ppiList.Add(ppi);
            }
               
            _context.PaymentPlanItems.AddRange(ppiList);
            _context.SaveChanges();
            
        }
        
        private decimal getTotalMonthlyPayment(decimal balance, decimal rate, decimal month){
            decimal a = (balance * (rate/1200));
            decimal c = (1 + rate/1200);
            decimal d = -1 * month;
            decimal b = 1 - (decimal)Math.Pow((double)c, (double)d);
            Console.WriteLine("TotalMonthlyPayment: " + (a/b));
            return (a / b);
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
