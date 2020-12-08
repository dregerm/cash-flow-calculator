
namespace AndyCashflowAPI.Models
{
    public class PaymentPlanItem
    {
        public int month;
        public decimal interestPayment;
        public decimal principalPayment;
        public decimal remainingBalance;

        public PaymentPlanItem(int months, decimal interest, decimal principal, decimal remaining){  
            month = months;
            interestPayment = interest;
            principalPayment = principal;
            remainingBalance = remaining;
        }
    }
}

