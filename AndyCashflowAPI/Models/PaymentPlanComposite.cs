//using Microsoft.EntityFrameworkCore;
///using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AndyCashflowAPI.Models
{
    //loan item did not have namespace before.
    public class PaymentPlanComposite
    {
        public LoanItem loanItem;
        public List<PaymentPlanItem> ppi = new List<PaymentPlanItem>();
        
        public PaymentPlanComposite(){}
        public PaymentPlanComposite(LoanItem loanItem, List<PaymentPlanItem> plan){
            this.loanItem = loanItem;
            this.ppi = plan;
        }
        
    }
}