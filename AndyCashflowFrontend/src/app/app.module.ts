import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddLoanComponent } from './add-loan/add-loan.component';
import { DisplayLoanPlansComponent } from './display-loan-plans/display-loan-plans.component';
import { DisplayAggregateCashFlowComponent } from './display-aggregate-cash-flow/display-aggregate-cash-flow.component';

@NgModule({
  declarations: [
    AppComponent,
    AddLoanComponent,
    DisplayLoanPlansComponent,
    DisplayAggregateCashFlowComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
