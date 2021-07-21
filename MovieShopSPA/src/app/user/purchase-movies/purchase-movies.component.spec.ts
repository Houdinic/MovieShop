import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchaseMoviesComponent } from './purchase-movies.component';

describe('PurchaseMoviesComponent', () => {
  let component: PurchaseMoviesComponent;
  let fixture: ComponentFixture<PurchaseMoviesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PurchaseMoviesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PurchaseMoviesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
