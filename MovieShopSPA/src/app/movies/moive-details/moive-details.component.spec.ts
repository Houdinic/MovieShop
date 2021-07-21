import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MoiveDetailsComponent } from './moive-details.component';

describe('MoiveDetailsComponent', () => {
  let component: MoiveDetailsComponent;
  let fixture: ComponentFixture<MoiveDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MoiveDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MoiveDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
