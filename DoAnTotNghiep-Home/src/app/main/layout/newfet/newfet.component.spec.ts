import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewfetComponent } from './newfet.component';

describe('NewfetComponent', () => {
  let component: NewfetComponent;
  let fixture: ComponentFixture<NewfetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewfetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewfetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
