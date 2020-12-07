
namespace AndyCashflowAPI.Models
{
    public class PaymentPlanItem
    {
        public PaymentPlanItem(int months, long interest, long principal, long remaining){  
            public const int month = months;
            public const long interestPayment = interest;
            public const long principalPayment = principal;
            public const long remainingBalance = remaining;
        }
    }
}

