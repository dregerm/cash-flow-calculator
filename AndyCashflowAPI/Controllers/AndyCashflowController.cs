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

        // GET: api/AndyCashflow/ppi
        [HttpGet("ppi")]
        public async Task<ActionResult<IEnumerable<PaymentPlanItem>>> GetPaymentPlanItems()
        {
            //Console.WriteLine("Get ppi");
            return await _context.PaymentPlanItems.ToListAsync();
            /*
            List<PaymentPlanItem> ppi_selected = new List<PaymentPlanItem>();
            List<LoanItem> loanList = await _context.LoanItems.ToListAsync();
            List<PaymentPlanComposite> compList =  new List<PaymentPlanComposite>();
            
            //bata playing around 

            
            
            Console.WriteLine("LOAN LIST:" + loanList);
            Console.WriteLine("LOOP STARTS HERE");
            for(int a = 0; a < loanList.Count; a++){
                ppi_selected = await _context.PaymentPlanItems.Where(x => x.LoanId == loanList[a].Id).ToListAsync();
                Console.WriteLine("ppi_selected: " + ppi_selected);
                Console.WriteLine("A: " + ppi_selected[0]);
                Console.WriteLine("B: " + ppi_selected[1]);
                PaymentPlanComposite ppc = new PaymentPlanComposite(loanList[a], ppi_selected);
                Console.WriteLine("payment plan composite: " + ppc);
                Console.WriteLine("C: " + ppc.loanItem);
                Console.WriteLine("D: " + ppc.ppi[0]);
                Console.WriteLine("E: " + ppc.ppi[1]);
                compList.Add(ppc);
            }
            
            Console.WriteLine("LOOP FINISHED");
            Console.WriteLine("F: " + compList[0].ppi[0]);
            Console.WriteLine("G: " + compList[0].ppi[1]);
            return compList;
            */
            
        }

        // GET: api/AndyCashflow/li
        [HttpGet("li")]
        public async Task<ActionResult<IEnumerable<LoanItem>>> GetLoanItems()
        {
            //Console.WriteLine("Get li");
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
            //Console.WriteLine("POST new loan item");
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