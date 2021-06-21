import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
  public BuenzliSortingMethod = BuenzliSortingMethod;

  public articles: ArticleSummary[];
  public buenzlis: User[];
  public halbschueh: User[];

  public loading: boolean = true;
  public loadingBuenzlis: boolean = true;

  public currentSortingMethod: SortingMethod;
  public currentBuenzliSortingMethod: BuenzliSortingMethod;

  constructor(
    private readonly articleService: ArticleService,
    private readonly userService: UserService,
    private readonly router: Router,
  ) { }

  async ngOnInit() {
    this.selectSortingMethod(SortingMethod.LATEST);
    this.selectBuenzliSortingMethod(BuenzliSortingMethod.OVERALL);
  }

  public createArticle(){
    this.router.navigate(['article', 'new']);
  }

  public async selectBuenzliSortingMethod(method: BuenzliSortingMethod) {
    this.loadingBuenzlis = true;
    this.currentBuenzliSortingMethod = method;
    switch (this.currentBuenzliSortingMethod) {
      case BuenzliSortingMethod.ARTICLES: {
        this.buenzlis = await this.userService.getTopBuenzlisForArticles().toPromise();
        this.halbschueh = await this.userService.getTopHalbschuehForArticles().toPromise();
        this.loadingBuenzlis = false;
        break;
      }
      case BuenzliSortingMethod.COMMENTS: {
        this.buenzlis = await this.userService.getTopBuenzlisForComments().toPromise();
        this.halbschueh = await this.userService.getTopHalbschuehForComments().toPromise();
        this.loadingBuenzlis = false;
        break;
      }
      case BuenzliSortingMethod.OVERALL: 
      default: {
        this.buenzlis = await this.userService.getTopBuenzlis().toPromise();
        this.halbschueh = await this.userService.getTopHalbschueh().toPromise();
        this.loadingBuenzlis = false;
        break;
      }
    }
  }

  public async selectSortingMethod(method: SortingMethod) {
    this.currentSortingMethod = method;
    switch (this.currentSortingMethod) {
      case SortingMethod.HOTTEST: await this.getHottest(); break;
      case SortingMethod.SHITTIEST: await this.getShittiest(); break;
      case SortingMethod.LATEST:
      default: await this.getLatest(); break;
    }
  }

  async getLatest() {
    try {
      this.loading = true;
      this.articles = await this.articleService.getSummaries().toPromise();
      console.log(this.loading);
    } catch (error) {
      console.log(error);
    } finally {
      this.loading = false;
      console.log(this.loading);
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

  public getIconClassForSortingMethod(method: SortingMethod) {
    switch (method) {
      case SortingMethod.HOTTEST: return 'fa-fire';
      case SortingMethod.SHITTIEST: return 'fa-poo';
      case SortingMethod.LATEST:
      default: return 'fa-sun';
    }
  }

  public getIconClassForBuenzliSortingMethod(method: BuenzliSortingMethod) {
    switch (method) {
      case BuenzliSortingMethod.OVERALL: return 'fa-trophy';
      case BuenzliSortingMethod.ARTICLES: return 'fa-newspaper';
      case BuenzliSortingMethod.COMMENTS:
      default: return 'fa-comment-dots';
    }
  }
}

export enum SortingMethod {
  LATEST = 'Neueschtem',
  HOTTEST = 'Laessigkeit',
  SHITTIEST = 'Chabis'
}

export enum BuenzliSortingMethod {
  OVERALL = 'Allgemein',
  ARTICLES = 'Artikel',
  COMMENTS = 'Saenf'
}