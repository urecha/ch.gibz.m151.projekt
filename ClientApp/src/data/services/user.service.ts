import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User, UserSummary } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  readonly baseRoute = `/User`;

  constructor(
    private httpClient: HttpClient,
  ) { }

  /**
   * Gets a simple usersummary..
   */
  getSummary(id?: string, username?: string): Observable<UserSummary>{
    const requestUrl = `${this.baseRoute}/summary/${id ? id : username ? username : ''}`;

    return this.httpClient.get<UserSummary>(requestUrl);
  }

  /**
   * Gets a user's summary along with its ranking and like-count
   */
  get(id?: string, username?: string): Observable<User>{
    const requestUrl = `${this.baseRoute}/${id ? id : username ? username : ''}`;

    return this.httpClient.get<User>(requestUrl);
  }

  /**
   * Gets the 3 top buenzlis (most likes overall)
   */
  getTopBuenzlis(): Observable<User[]>{
    const requestRoute = `${this.baseRoute}/top-buenzlis`;

    return this.httpClient.get<User[]>(requestRoute);
  }

  /**
   * Gets the 3 top buenzlis (most likes on articles)
   */
   getTopBuenzlisForArticles(): Observable<User[]>{
    const requestRoute = `${this.baseRoute}/top-buenzlis/articles`;

    return this.httpClient.get<User[]>(requestRoute);
  }

  /**
   * Gets the 3 top buenzlis (most likes on comments)
   */
   getTopBuenzlisForComments(): Observable<User[]>{
    const requestRoute = `${this.baseRoute}/top-buenzlis/comments`;

    return this.httpClient.get<User[]>(requestRoute);
  }

  /**
   * Gets the 3 top halbschueh (most dislikes overall)
   */
  getTopHalbschueh(): Observable<User[]>{
    const requestRoute = `${this.baseRoute}/top-halbschueh`;

    return this.httpClient.get<User[]>(requestRoute);
  }

  /**
   * Gets the 3 top halbschueh (most dislikes on articles)
   */
   getTopHalbschuehForArticles(): Observable<User[]>{
    const requestRoute = `${this.baseRoute}/top-halbschueh`;

    return this.httpClient.get<User[]>(requestRoute);
  }

  /**
   * Gets the 3 top halbschueh (most dislikes for comments)
   */
   getTopHalbschuehForComments(): Observable<User[]>{
    const requestRoute = `${this.baseRoute}/top-halbschueh`;

    return this.httpClient.get<User[]>(requestRoute);
  }
}
