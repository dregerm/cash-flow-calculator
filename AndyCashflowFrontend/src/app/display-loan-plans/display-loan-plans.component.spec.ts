import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplayLoanPlansComponent } from './display-loan-plans.component';

describe('DisplayLoanPlansComponent', () => {
  let component: DisplayLoanPlansComponent;
  let fixture: ComponentFixture<DisplayLoanPlansComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DisplayLoanPlansComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DisplayLoanPlansComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
