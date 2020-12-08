
namespace AndyCashflowAPI.Models
{
    //loan item did not have namespace before.
    public class LoanItemDTO
    {
        public decimal Balance { get; set; }
        public int MonthLeft { get; set; }
        public decimal Rate { get; set; }
    }
}
