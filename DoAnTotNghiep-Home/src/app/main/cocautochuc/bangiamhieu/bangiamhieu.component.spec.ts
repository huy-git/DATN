import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BangiamhieuComponent } from './bangiamhieu.component';

describe('BangiamhieuComponent', () => {
  let component: BangiamhieuComponent;
  let fixture: ComponentFixture<BangiamhieuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BangiamhieuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BangiamhieuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
