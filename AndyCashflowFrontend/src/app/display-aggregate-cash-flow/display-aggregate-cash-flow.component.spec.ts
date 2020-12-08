import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplayAggregateCashFlowComponent } from './display-aggregate-cash-flow.component';

describe('DisplayAggregateCashFlowComponent', () => {
  let component: DisplayAggregateCashFlowComponent;
  let fixture: ComponentFixture<DisplayAggregateCashFlowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DisplayAggregateCashFlowComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DisplayAggregateCashFlowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
