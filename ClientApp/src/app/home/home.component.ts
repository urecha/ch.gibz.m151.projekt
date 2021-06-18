import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { User } from 'src/data/models/user';
import { UserService } from 'src/data/services/user.service';
import { ArticleSummary } from '../../data/models/article';
import { ArticleService } from '../../data/services/article.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public SortingMethod = SortingMethod;

  public articles: ArticleSummary[];
  public buenzlis: User[];
  public halbschueh: User[];  

  public loading: boolean = true;

  public sortingMethod$: Observable<SortingMethod>;
  public currentSortingMethod: SortingMethod;

  constructor(
    private readonly articleService: ArticleService,
    private readonly userService: UserService,
  ) { }

  async ngOnInit() {
    this.selectSortingMethod(SortingMethod.LATEST);
    this.buenzlis = await this.userService.getTopBuenzlis().toPromise();
    this.halbschueh = await this.userService.getTopHalbschueh().toPromise();
  }

  public selectSortingMethod(method: SortingMethod) {
    this.currentSortingMethod = method;
    switch (this.currentSortingMethod) {
      case SortingMethod.HOTTEST: this.getHottest(); break;
      case SortingMethod.SHITTIEST: this.getShittiest(); break;
      case SortingMethod.LATEST:
      default: this.getLatest(); break;
    }
  }

  async getLatest() {
    try {
      this.loading = true;
      this.articles = await this.articleService.getSummaries().toPromise();
    } catch (error) {
      console.log(error);
    } finally {
      this.loading = false;
    }
  }

  async getShittiest() {
    try {
      this.loading = true;
      this.articles = await this.articleService.getShittiest().toPromise();
    } catch (error) {
      console.log(error);
    } finally {
      this.loading = false;
    }
  }

  async getHottest() {
    try {
      this.loading = true;
      this.articles = await this.articleService.getHottest().toPromise();
    } catch (error) {
      console.log(error);
    } finally {
      this.loading = false;
    }
  }

  public getIconClassForSortingMethod(method: SortingMethod){
    switch(method){
      case SortingMethod.HOTTEST: return 'fa-fire';
      case SortingMethod.SHITTIEST: return 'fa-poo';
      case SortingMethod.LATEST:
      default: return 'fa-sun';
    }
  }
}

export enum SortingMethod {
  LATEST = 'Neus zueg',
  HOTTEST = 'Laessigkeit',
  SHITTIEST = 'Chabis'
}

export enum BuenzliSortingMethod {
  OVERALL = 'Allgemein',
  ARTICLES = 'Artikel',
  COMMENTS = 'Saenf'
}