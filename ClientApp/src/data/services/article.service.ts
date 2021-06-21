import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Article, ArticleSummary } from '../models/article';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {
  readonly baseRoute = '/api/Beitrag';

  readonly httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

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

  /**
   * Creates or updates an article.
   * @param article Article to create
   * @returns Created article (with id)
   */
  createOrUpdate(article: Article): Observable<Article>{
    const requestUrl = `${this.baseRoute}`;
    console.log(article);
    const json = JSON.stringify(article);
    console.log(json);

    return this.httpClient.post<Article>(requestUrl, json, this.httpOptions);
  }

  /**
   * Deletes the article with given id along with all its comments and likes and their likes.
   */
  deleteArticle(id: number): Observable<void>{
    const requestUrl = `${this.baseRoute}/${id}`;

    return this.httpClient.delete<void>(requestUrl, this.httpOptions);
  }

  /**
   * Likes or unlikes an article, depending on whether it was liked or not before
   */
   likeArticle(id: number): Observable<void>{
    const requestUrl = `${this.baseRoute}/${id}/like`;

    return this.httpClient.get<void>(requestUrl);
  }

  /**
   * Dislikes or un-dislikes an article, depending on whether it was unliked or not before
   */
  dislikeArticle(id: number): Observable<void>{
    const requestUrl = `${this.baseRoute}/${id}/dislike`;

    return this.httpClient.get<void>(requestUrl);
  }
  
}
