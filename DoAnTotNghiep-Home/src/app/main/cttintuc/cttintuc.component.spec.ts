import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CttintucComponent } from './cttintuc.component';

describe('CttintucComponent', () => {
  let component: CttintucComponent;
  let fixture: ComponentFixture<CttintucComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CttintucComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CttintucComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
