import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Article } from '../models/article';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {
  readonly baseRoute = '/Beitrag';

  constructor(
    private readonly httpClient: HttpClient,
  ) { }

  getAll(): Observable<Article[]> {
    const requestUrl = `${this.baseRoute}`;

    return this.httpClient.get<Article[]>(requestUrl);
  }
  
}
