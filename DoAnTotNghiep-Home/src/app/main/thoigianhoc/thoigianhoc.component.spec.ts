import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ThoigianhocComponent } from './thoigianhoc.component';

describe('ThoigianhocComponent', () => {
  let component: ThoigianhocComponent;
  let fixture: ComponentFixture<ThoigianhocComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ThoigianhocComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ThoigianhocComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
