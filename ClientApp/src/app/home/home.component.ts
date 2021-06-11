import { Component, OnInit } from '@angular/core';
import { Article } from '../../data/models/article';
import { ArticleService } from '../../data/services/article.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  constructor(
    private readonly articleService: ArticleService,
  ) { }

  ngOnInit() {
    this.articleService.getAll().toPromise().then(articles => this.articles = articles);
  }

  articles: Article[];
}
