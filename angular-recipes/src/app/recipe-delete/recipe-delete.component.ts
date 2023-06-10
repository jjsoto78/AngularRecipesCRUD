import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder } from '@angular/forms';
import { RecipesService } from '../services/recipes.service';
import { SnacksService } from '../snacks/snacks.service';

@Component({
  selector: 'app-recipe-delete',
  templateUrl: './recipe-delete.component.html',
  styleUrls: ['./recipe-delete.component.css'],
})
export class RecipeDeleteComponent {
  constructor(
    private _fb: FormBuilder,
    private _dialogRef: MatDialogRef<RecipeDeleteComponent>,
    private _snacksService: SnacksService,
    private _recipeService: RecipesService,
    @Inject(MAT_DIALOG_DATA) public data: any // needed to pass recipe name
  ) {}

  deleteRecipe() {
    if (this.data) {
      this._recipeService.deleteRecipe(this.data.id).subscribe({
        next: (val: any) => {
          this._dialogRef.close(true); // returns true to calling component
          // alert(`Updated recipe ${this.data.name} !!`);
          this._snacksService.openSnackBar(
            `Deleted recipe ${this.data.name} !!`
          );
        },
        error: (err: any) => {
          console.error(err);
        },
      });
    }
  }
}
