import { Component, OnInit } from '@angular/core';
import { Loan } from '../loan';
import { PaymentPlanService } from '../payment-plan.service';


@Component({
  selector: 'app-add-loan',
  templateUrl: './add-loan.component.html',
  styleUrls: ['./add-loan.component.css']
})
export class AddLoanComponent implements OnInit {

  loan: Loan = {
    balance: 100,
    monthLeft: 5,
    rate: 2.5
  }
  constructor(private paymentPlanService: PaymentPlanService) { }

  ngOnInit(): void {
  }

  addLoan(): void {
    this.paymentPlanService.postLoanPlan(this.loan);
  }



}
