import { Component, OnInit } from '@angular/core';
import { LOANS } from '../mock-loans';

@Component({
  selector: 'app-display-loan-plans',
  templateUrl: './display-loan-plans.component.html',
  styleUrls: ['./display-loan-plans.component.css']
})
export class DisplayLoanPlansComponent implements OnInit {

  loans = LOANS;
  constructor() { }

  ngOnInit(): void {
  }

}
