import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SildeHeaderComponent } from './silde-header.component';

describe('SildeHeaderComponent', () => {
  let component: SildeHeaderComponent;
  let fixture: ComponentFixture<SildeHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SildeHeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SildeHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
