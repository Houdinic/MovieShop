import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor() { }
 getTopRevenueMovies()  {
    //make an ajax HTTP call to API https://localhost:44381/api/Movies/toprevenue

  }
}        
