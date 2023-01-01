import {Component, Inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";

import {Ingredient} from "./models/ingredient";
import {Recipe} from "./models/recipe";
import {RecipeIngredient} from "./models/recipe-ingredient";
import {StoreSection} from "./models/store-section";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})

export class AppComponent {
  title = 'Grocer - Home';
  public recipes: Recipe[] = []
  public ingredients: Ingredient[] = [];
  public recipeIngredients: RecipeIngredient[] = [];

  initRecipeIngredients: any[] = [
    { name: 'id',     display: 'Id' },
    { name: 'name',   display: 'Name' }
  ];
  displayRecipeIngredients: any[] = this.initRecipeIngredients.map(col => col.name);

  public storeSections: StoreSection[] = [];
  initStoreSections: any[] = [
    { name: 'id',     display: 'Id' },
    { name: 'name',   display: 'Name' }
  ];
  displayStoreSections: any[] = this.initStoreSections.map(col => col.name);

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    http.get<Recipe[]>(baseUrl + 'Recipe').subscribe(
      result => { this.recipes = result; },
      error => console.error(error));

    http.get<RecipeIngredient[]>(baseUrl + 'RecipeIngredient').subscribe(
      result => { this.recipeIngredients = result; },
      error => console.error(error));

    http.get<Ingredient[]>(baseUrl + 'Ingredient').subscribe(
      result => { this.ingredients = result; },
      error => console.error(error));

    http.get<StoreSection[]>(baseUrl + 'StoreSection').subscribe(
      result => { this.storeSections = result; },
      error => console.error(error));

  }


}
