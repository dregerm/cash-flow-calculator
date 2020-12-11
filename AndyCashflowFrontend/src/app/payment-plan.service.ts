import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Loan } from './loan';
import { LoanPlan } from './loan-plan';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PaymentPlanService {

  private ppiUrl = '/getppi';  // URL to GET ppi method in web api
  private liUrl = '/getli'; //
  private baseUrl = 'http://localhost:4200';

  constructor(private http: HttpClient) { }

  getLoanPlan(): Observable<LoanPlan[]> {
    return this.http.get<LoanPlan[]>(this.baseUrl + this.ppiUrl);
  }

  getLoanIds(): Observable<Loan[]> {
    return this.http.get<Loan[]>(this.baseUrl + this.liUrl);
  }
  
  postLoanPlan(loanItem: Loan): Observable<Loan>{
    return this.http.post<Loan>(this.baseUrl + '/post', loanItem);
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