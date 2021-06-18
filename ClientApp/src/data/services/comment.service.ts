import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Comment } from '../models/comment';

@Injectable({
  providedIn: 'root',
})
export class CommentService {
  readonly baseRoute = '/Kommentar';

  constructor(
    private readonly httpClient: HttpClient,
  ) { }

  getForArticle(articleId: string): Observable<Comment[]> {
    const requestUrl = `${this.baseRoute}/${articleId}`;

    return this.httpClient.get<Comment[]>(requestUrl);
  }

  /**
   * Creates or updates a comment
   */
  createOrUpdate(comment: Comment): Observable<Comment>{
    const requestUrl = `${this.baseRoute}`;

    return this.httpClient.post<Comment>(requestUrl, comment);
  }

  /**
   * Deletes the comment with given id
   */
  deleteComment(id: string): Observable<void>{
    const requestUrl = `${this.baseRoute}`;

    return this.httpClient.delete<void>(requestUrl);
  }

  /**
   * Likes or unlikes a comment, depending on whether it was liked or not before
   */
  likeComment(id: number): Observable<void>{
    const requestUrl = `${this.baseRoute}/${id}/like`;

    return this.httpClient.get<void>(requestUrl);
  }

  /**
   * Disikes or undislikes a comment, depending on whether it was disliked or not before
   */
   disikeComment(id: number): Observable<void>{
    const requestUrl = `${this.baseRoute}/${id}/dislike`;

    return this.httpClient.get<void>(requestUrl);
  }
}
