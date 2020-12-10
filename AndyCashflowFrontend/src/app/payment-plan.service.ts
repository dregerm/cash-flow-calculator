import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Loan } from './loan';
import { LoanPlan } from './loan-plan';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PaymentPlanService {

  private ppiUrl = 'api/AndyCashflow/ppi';  // URL to GET ppi method in web api
  private liUrl = 'api/AndyCashflow/li'; //
  private baseUrl = 'localhost:5001/';

  constructor(private http: HttpClient) { }

  getLoanPlan(): Observable<LoanPlan[]> {
    //return of(LOANS);

    return this.http.get<LoanPlan[]>(this.baseUrl + this.ppiUrl);
  }

  postLoanPlan(loanItem: Loan): Observable<any>{
    return this.http.post(this.baseUrl, loanItem);
    
  }
}


/* Michael looking at different stuffs

from http://youmightnotneedjquery.com/#post
 var request = new XMLHttpRequest();
request.open('POST', '/my/url', true);
request.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded; charset=UTF-8');
request.send(data);

other thing i had was from
https://stackoverflow.com/questions/49014689/http-post-request-with-typescript
*/