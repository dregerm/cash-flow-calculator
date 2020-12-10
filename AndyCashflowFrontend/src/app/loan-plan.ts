export interface LoanPlan {
    id : number;
    loanId: number;
    month: number;
    interestPayment: number;
    principalPayment: number;
    remainingBalance: number;
  }