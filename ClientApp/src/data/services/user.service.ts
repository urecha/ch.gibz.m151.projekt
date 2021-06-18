import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserSummary } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  readonly baseRoute = `/User`;

  constructor(
    private httpClient: HttpClient,
  ) { }

  getSummary(id?: string, username?: string): Observable<UserSummary>{
    const requestUrl = `${this.baseRoute}`;

    return this.httpClient.get<UserSummary>(requestUrl);
  }
}
