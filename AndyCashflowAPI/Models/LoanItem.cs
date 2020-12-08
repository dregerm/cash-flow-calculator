using System.Collections.Generic;
namespace AndyCashflowAPI.Models
{
    //loan item did not have namespace before.
    public class LoanItem
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public int MonthLeft { get; set; }
        public decimal Rate { get; set; }
        //public List<PaymentPlanItem> Plan { get; set; }
    }
}
