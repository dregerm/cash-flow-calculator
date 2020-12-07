public class LoanItem
{
    public long Id { get; set; }
    public long Balance { get; set; }
    public int MonthLeft { get; set; }
    public long Rate { get; set; }
    public PaymentPlanItem[] plan { get; set; }

}