import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
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

  public loading: boolean = true;

  public sortingMethod$: Observable<SortingMethod>;
  public currentSortingMethod: SortingMethod = SortingMethod.LATEST;

  constructor(
    private readonly articleService: ArticleService,
  ) { }

  ngOnInit() {
    this.getLatest();
    this.sortingMethod$.subscribe(method => {
      this.currentSortingMethod = method;
      switch(method){
        case SortingMethod.HOTTEST: this.getHottest(); break;
        case SortingMethod.SHITTIEST: this.getShittiest(); break;
        case SortingMethod.LATEST:
        default: this.getLatest(); break;
      }
    })
  }

  public selectSortingMethod(method: SortingMethod){
    this.sortingMethod$ = of(method);
  }

  async getLatest(){
    try{
      this.loading = true;
      this.articles = await this.articleService.getSummaries().toPromise();
    } catch(error){
      console.log(error);
    } finally{
      this.loading = false;
    }
  }

  async getShittiest() {
    try {
      this.loading = true;
      this.articles = await this.articleService.getShittiest().toPromise();
    } catch (error) {
      console.log(error);
    } finally{
      this.loading = false;
    }
  }

  async getHottest() {
    try {
      this.loading = true;
      this.articles = await this.articleService.getHottest().toPromise();
    } catch (error) {
      console.log(error);
    } finally{
      this.loading = false;
    }
  }
}

export enum SortingMethod{
  LATEST ='Neus zueg',
  HOTTEST = 'Laessigkeit',
  SHITTIEST = 'Chabis'
}