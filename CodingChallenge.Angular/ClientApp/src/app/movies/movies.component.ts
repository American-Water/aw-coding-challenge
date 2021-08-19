import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html'
})
export class MoviesComponent {
  public movies: Movie[];


  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    //http.get<Movies[]>(baseUrl + 'movieslist').subscribe(result => {
    //  this.movies = result;
    //}, error => console.error(error));

    //const headers = { 'Authorization': 'Bearer my-token', 'My-Custom-Header': 'foobar' };
    const body = { SortColumn: 'year' };
    http.post<Movie[]>(baseUrl + 'movieslist', body).subscribe(data => {
      this.movies = data;
    });
  }
}



interface Movie {
  id: number;
  title: string;
  year: DatePipe;
  rating: DoubleRange;
}
