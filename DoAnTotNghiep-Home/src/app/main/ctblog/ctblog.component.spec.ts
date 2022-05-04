import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CtblogComponent } from './ctblog.component';

describe('CtblogComponent', () => {
  let component: CtblogComponent;
  let fixture: ComponentFixture<CtblogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CtblogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CtblogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
