import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailAsyncPage } from './detail-async.page';

describe('DetailAsyncPage', () => {
  let component: DetailAsyncPage;
  let fixture: ComponentFixture<DetailAsyncPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetailAsyncPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailAsyncPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
