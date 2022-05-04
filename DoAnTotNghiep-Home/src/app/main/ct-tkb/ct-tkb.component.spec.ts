import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CtTkbComponent } from './ct-tkb.component';

describe('CtTkbComponent', () => {
  let component: CtTkbComponent;
  let fixture: ComponentFixture<CtTkbComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CtTkbComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CtTkbComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
