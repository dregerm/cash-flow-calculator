import { Component, OnInit } from '@angular/core';
import { PaymentPlanService } from '../payment-plan.service';
import { LoanPlan } from '../loan-plan';
import { Loan } from '../loan';


@Component({
  selector: 'app-display-loan-plans',
  templateUrl: './display-loan-plans.component.html',
  styleUrls: ['./display-loan-plans.component.css']
})
export class DisplayLoanPlansComponent implements OnInit {

  loans: LoanPlan[];
  loanIds: Loan[]
  constructor(private paymentPlanService: PaymentPlanService) { }

  ngOnInit(): void {
    this.getPaymentPlan();
    this.getLoanIds();
  }
  
  getPaymentPlan(): void {
    this.paymentPlanService.getLoanPlan().subscribe(loans => this.loans = loans);
  }

  getLoanIds(): void {
    this.paymentPlanService.getLoanIds().subscribe(loanIds => this.loanIds = loanIds);
    console.log(JSON.stringify(this.loanIds));
  }
}
