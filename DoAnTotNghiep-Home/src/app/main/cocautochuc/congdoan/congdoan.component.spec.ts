import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CongdoanComponent } from './congdoan.component';

describe('CongdoanComponent', () => {
  let component: CongdoanComponent;
  let fixture: ComponentFixture<CongdoanComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CongdoanComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CongdoanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
