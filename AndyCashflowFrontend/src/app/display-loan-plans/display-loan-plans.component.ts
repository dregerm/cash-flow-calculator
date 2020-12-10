import { Component, OnInit } from '@angular/core';
import { PaymentPlanService } from '../payment-plan.service';
import { LoanPlan } from '../loan-plan';


@Component({
  selector: 'app-display-loan-plans',
  templateUrl: './display-loan-plans.component.html',
  styleUrls: ['./display-loan-plans.component.css']
})
export class DisplayLoanPlansComponent implements OnInit {

  loans: LoanPlan[];
  constructor(private paymentPlanService: PaymentPlanService) { }

  ngOnInit(): void {
    this.getPaymentPlan();
  }
  
  getPaymentPlan(): void {
    this.paymentPlanService.getLoanPlan().subscribe(loans => this.loans = loans);
  }
}
