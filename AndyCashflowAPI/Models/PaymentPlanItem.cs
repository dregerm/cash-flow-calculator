using System.ComponentModel.DataAnnotations;
namespace AndyCashflowAPI.Models
{
    public class PaymentPlanItem
    {
        [Key] public int Id { get; set; }
        public int loanId;
        public int month;
        public decimal interestPayment;
        public decimal principalPayment;
        public decimal remainingBalance;
        public PaymentPlanItem() {}
        public PaymentPlanItem(int loanId, int months, decimal interest, decimal principal, decimal remaining){  
            this.loanId = loanId;
            month = months;
            interestPayment = interest;
            principalPayment = principal;
            remainingBalance = remaining;
        }
        
    }
}

