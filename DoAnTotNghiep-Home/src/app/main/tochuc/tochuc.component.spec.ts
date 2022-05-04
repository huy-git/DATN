import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TochucComponent } from './tochuc.component';

describe('TochucComponent', () => {
  let component: TochucComponent;
  let fixture: ComponentFixture<TochucComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TochucComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TochucComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
