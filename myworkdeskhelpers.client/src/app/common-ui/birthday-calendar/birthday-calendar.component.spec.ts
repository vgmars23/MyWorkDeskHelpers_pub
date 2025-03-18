import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BirthdayCalendarComponent } from './birthday-calendar.component';

describe('BirthdayCalendarComponent', () => {
  let component: BirthdayCalendarComponent;
  let fixture: ComponentFixture<BirthdayCalendarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BirthdayCalendarComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BirthdayCalendarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
