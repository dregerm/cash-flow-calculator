using System.ComponentModel.DataAnnotations;
public class PaymentPlanItem
{
    [Key] public int Id { get; set; }
    public int LoanId { get; set; }
    public int Month { get; set; }
    public decimal InterestPayment { get; set; }
    public decimal PrincipalPayment { get; set; }
    public decimal RemainingBalance { get; set; }
    public PaymentPlanItem() {}
    public PaymentPlanItem(int loanId, int months, decimal interest, decimal principal, decimal remaining){  
        this.LoanId = loanId;
        Month = months;
        InterestPayment = interest;
        PrincipalPayment = principal;
        RemainingBalance = remaining;
    }
    public override string ToString() {
    return "LoanID: " + LoanId + " Month: " + Month + " Interest: " + InterestPayment + " Principle: " + PrincipalPayment + " Remaining: " + RemainingBalance;
    } 
    
}
    


