import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html'
})
export class MoviesComponent {
  public movies: Movie[];
  public params: object;
  public http: HttpClient;
  public baseUrl: string;


  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.route.queryParams
      .subscribe(params => {
        this.http.post<Movie[]>(this.baseUrl + 'movieslist', params).subscribe(data => {
          this.movies = data;
        });
      });
  }

}



interface Movie {
  id: number;
  title: string;
  year: DatePipe;
  rating: DoubleRange;
}
