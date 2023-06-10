import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { RecipesService } from '../services/recipes.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { SnacksService } from '../snacks/snacks.service';

@Component({
  selector: 'app-recipe-add-edit',
  templateUrl: './recipe-add-edit.component.html',
  styleUrls: ['./recipe-add-edit.component.css'],
})
export class RecipeAddEditComponent implements OnInit {
  origins: string[] = ['Europe', 'Asia', 'America', 'Latam', 'Africa', 'Other'];

  recipesForm: FormGroup;

  constructor(
    private _fb: FormBuilder,
    private _recipeService: RecipesService,
    private _dialogRef: MatDialogRef<RecipeAddEditComponent>,
    private _snacksService: SnacksService,
    @Inject(MAT_DIALOG_DATA) public data: any // needed to pass the row data for editing
    
    
  ) {
    this.recipesForm = _fb.group({
      name: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(25)]],
      servings: ['', [Validators.pattern('^[1-9][0-9]*$'), Validators.maxLength(2)]],
      cost: ['', [Validators.pattern('^[1-9][0-9]*$'), Validators.maxLength(3)]],
      origin: '',
      instructions: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(150)]],
    });

  }
  ngOnInit(): void {
    // throw new Error('Method not implemented.');
    this.recipesForm.patchValue(this.data);
  }

  
  get nameControl(): FormControl{
    return this.recipesForm.get('name') as FormControl;
  }

  get instructionsControl(): FormControl{
    return this.recipesForm.get('instructions') as FormControl;
  }

  get servingsControl(): FormControl{
    return this.recipesForm.get('servings') as FormControl;
  }
  
  get costControl(): FormControl{
    return this.recipesForm.get('cost') as FormControl;
  }

  // form submits upon creating a new recipe or updating an existing one
  onFormSubmit() {
    if (!this.recipesForm.valid) return;

    if (this.data) {
      this._recipeService
        .updateRecipe(this.data.id, this.recipesForm.value)
        .subscribe({
          next: () => {
            this._dialogRef.close(true);
            // alert(`Updated recipe ${this.data.name} !!`);
            this._snacksService.openSnackBar(`Updated recipe ${this.data.name} !!`)
          },
          error: (err: any) => {
            console.error(err);
          },
        });
    } else { // data falsy
      this._recipeService.addRecipe(this.recipesForm.value).subscribe({
        next: () => {
          
          this._snacksService.openSnackBar(`Successfully added recipe ${this.recipesForm.value.name} !!`)

          // refresh list is done in app component after dialog close event
          this._dialogRef.close(true);
        },
        error: (err: any) => {
          console.error(err);
        },
      });
    }
  }
}
