import { Component, OnInit } from '@angular/core';
import { Loan } from './loan';
import { LoanPlan } from './loan-plan';
import { PaymentPlanService } from './payment-plan.service';
import { AggLoanPlan } from './agg-loan-plan';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Cash Flow Calculator';
  
  loan: Loan = {
    balance: 100,
    monthLeft: 5,
    rate: 2.5
  }
  loans: LoanPlan[];
  loanIds: Loan[];
  aggLoanPlanner: AggLoanPlan[];
  
  constructor(private paymentPlanService: PaymentPlanService) { }

  

  addLoan(): void {
    this.paymentPlanService.postLoanPlan(this.loan).subscribe(x => {
      this.getPaymentPlan();
      this.getLoanIds();
    });
  }

  ngOnInit(): void {
    this.getPaymentPlan();
    this.getLoanIds();
  }
  
  getPaymentPlan(): void {
    this.paymentPlanService.getLoanPlan().subscribe(loans => {
      this.loans = loans;
      this.getAgg();
    });

  }

  getLoanIds(): void {
    this.paymentPlanService.getLoanIds().subscribe(loanIds => this.loanIds = loanIds);
  }

  getAgg(): void {
    let month: number = 1;
    let aggMonth: AggLoanPlan;
    let aggPlan: AggLoanPlan[] = [];
    do {
      let intPayment = 0
      let prinPayment = 0;
      let rBal = 0;
      let monthCashflow = this.loans.filter(x => x.month === month);
      monthCashflow.forEach(element => {
        intPayment += element.interestPayment;
        prinPayment += element.principalPayment;
        rBal += element.remainingBalance;
      });

      aggMonth = {
        "month": month,
        "interestPayment": intPayment,
        "principalPayment": prinPayment,
        "remainingBalance": rBal
      }
      aggPlan.push(aggMonth);
      month = month + 1;
      
    }while(aggMonth.remainingBalance !== 0)

    aggPlan.pop();
    this.aggLoanPlanner = aggPlan;
  } 
}
