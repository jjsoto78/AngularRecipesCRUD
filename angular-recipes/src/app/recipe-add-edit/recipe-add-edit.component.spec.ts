import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipeAddEditComponent } from './recipe-add-edit.component';

describe('RecipeAddEditComponent', () => {
  let component: RecipeAddEditComponent;
  let fixture: ComponentFixture<RecipeAddEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RecipeAddEditComponent]
    });
    fixture = TestBed.createComponent(RecipeAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
