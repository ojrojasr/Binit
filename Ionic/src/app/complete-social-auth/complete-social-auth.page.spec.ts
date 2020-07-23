import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompleteSocialAuthPage } from './complete-social-auth.page';

describe('CompleteSocialAuthPage', () => {
  let component: CompleteSocialAuthPage;
  let fixture: ComponentFixture<CompleteSocialAuthPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompleteSocialAuthPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompleteSocialAuthPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
