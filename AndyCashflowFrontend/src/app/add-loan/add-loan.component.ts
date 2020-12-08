import { Component, OnInit } from '@angular/core';
import {Loan } from '../loan';

@Component({
  selector: 'app-add-loan',
  templateUrl: './add-loan.component.html',
  styleUrls: ['./add-loan.component.css']
})
export class AddLoanComponent implements OnInit {

  loan: Loan = {
    balance: 100,
    monthLeft: 1000,
    rate: 2.5
  }
  constructor() { }

  ngOnInit(): void {
  }

}
