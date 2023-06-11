import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { RecipeAddEditComponent } from './recipe-add-edit/recipe-add-edit.component';
import { RecipesService } from './services/recipes.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { RecipeDeleteComponent } from './recipe-delete/recipe-delete.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'angular-recipes';

  displayedColumns: string[] = [
    'name',
    'servings',
    'origin',
    'cost',
    'instructions',
    'actions',
  ];

  dataSource!: MatTableDataSource<any>;

  loadingSpinner: boolean = true;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  // add all injectables
  constructor(
    private recipeAddEditDialog: MatDialog,
    private _recipeService: RecipesService,
    private recipeDeleteDialog: MatDialog
  ) {}

  async showSpinner(show: boolean) {
    // adding some delay for the spinner to show up
    await new Promise((resolve) => setTimeout(resolve, 800));
    this.loadingSpinner = show;
  }

  ngOnInit(): void {
    this.getAllRecipes();
    this.showSpinner(false);
  }

  refreshRecipes(dialogRef: MatDialogRef<any, any>) {
    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this.showSpinner(true);
          this.getAllRecipes();
          this.showSpinner(false);
        }
      },
    });
  }

  openDeleteDialog(data: any) {
    const dialogRef = this.recipeDeleteDialog.open(RecipeDeleteComponent, {
      data,
    });

    this.refreshRecipes(dialogRef);
  }

  openAddRecipeModal() {
    const dialogRef = this.recipeAddEditDialog.open(RecipeAddEditComponent);
    this.refreshRecipes(dialogRef);
  }

  openEditRecipeDialog(data: any) {
    const dialogRef = this.recipeAddEditDialog.open(RecipeAddEditComponent, {
      // javascript data: data
      data,
    });

    this.refreshRecipes(dialogRef);
  }

  getAllRecipes() {
    this._recipeService.getAllRecipes().subscribe({
      next: (response) => {
        this.dataSource = new MatTableDataSource(response);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        console.log(response);
      },
      error: console.log,
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}
