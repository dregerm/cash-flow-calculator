using System.ComponentModel.DataAnnotations;
public class PaymentPlanItem
{
    [Key] public int Id { get; set; }
    public int LoanId;
    public int Month;
    public decimal InterestPayment;
    public decimal PrincipalPayment;
    public decimal RemainingBalance;
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
    


