import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Article, ArticleSummary } from '../models/article';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {
  readonly baseRoute = '/Beitrag';

  constructor(
    private readonly httpClient: HttpClient,
  ) { }

  /**
   * Gets summaries of the latest articles. 
   */
  getSummaries(): Observable<ArticleSummary[]> {
    const requestUrl = `${this.baseRoute}`;

    return this.httpClient.get<ArticleSummary[]>(requestUrl);
  }

  /**
   * Gets summaries of the hottest articles
   */
  getHottest(): Observable<ArticleSummary[]>{
    const requestUrl = `${this.baseRoute}/hottest`;

    return this.httpClient.get<ArticleSummary[]>(requestUrl);
  }

  /**
   * Gets summaries of the shittiest articles
   */
  getShittiest(): Observable<ArticleSummary[]>{
    const requestUrl = `${this.baseRoute}/shittiest`;

    return this.httpClient.get<ArticleSummary[]>(requestUrl);
  }

  /**
   * Get a single article yb id
   */
  get(id: number): Observable<Article>{
    const requestUrl = `${this.baseRoute}/${id}`;

    return this.httpClient.get<Article>(requestUrl);
  }
  
}
