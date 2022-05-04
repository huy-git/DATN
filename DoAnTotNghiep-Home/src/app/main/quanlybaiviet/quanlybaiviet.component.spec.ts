import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuanlybaivietComponent } from './quanlybaiviet.component';

describe('QuanlybaivietComponent', () => {
  let component: QuanlybaivietComponent;
  let fixture: ComponentFixture<QuanlybaivietComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuanlybaivietComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuanlybaivietComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
