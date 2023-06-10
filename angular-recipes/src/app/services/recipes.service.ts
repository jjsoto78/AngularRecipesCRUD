import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


const BASE_API_URL = 'http://localhost:5000/recipes';

@Injectable({
  providedIn: 'root',
})
export class RecipesService {
  constructor(private _http: HttpClient) {}

  valiateData(data: any){
    if(!data.cost || data.cost < 0 || isNaN(data.cost)) data.cost = 0;
    if(!data.servings || data.servings < 0 || isNaN(data.servings)) data.servings = 0;
    // if(!data.servings) data.servings = 0;    
  }

  // method returns an Observable
  addRecipe(data: any) : Observable<any> {
    this.valiateData(data);
    return this._http.post(BASE_API_URL, data);
  }

  getAllRecipes() : Observable<any> {
    return this._http.get(BASE_API_URL);
  }

  deleteRecipe(id: number) : Observable<any> {
    return this._http.delete(`${BASE_API_URL}/${id}`);
  }

  updateRecipe(id: number, data: any) : Observable<any> {
    this.valiateData(data);
    return this._http.put(`${BASE_API_URL}/${id}`, data);
  }
}
