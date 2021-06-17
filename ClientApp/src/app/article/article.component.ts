import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ArticleService } from 'src/data/services/article.service';
import { Article } from '../../data/models/article';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css']
})
export class ArticleComponent implements OnInit {
  article: Article;

  loading: boolean = true;

  constructor(
    private readonly articleService: ArticleService,
    private readonly route: ActivatedRoute,
  ) { }

  ngOnInit() {
    this.route.params.subscribe(async (params) => {
      if (params.id) {
        if (params.id == 'new') {
          this.article = new Article();
        } else {
          try {
            this.loading = true;
            this.article = await this.articleService.get(params.id).toPromise();
          } catch (error) {
            console.log(error);
          } finally {
            this.loading = false;
          }
        }
      }
    })
  }

}
