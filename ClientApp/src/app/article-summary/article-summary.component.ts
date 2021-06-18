import { Component, Input, OnInit } from '@angular/core';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { ArticleService } from 'src/data/services/article.service';
import { ArticleSummary } from '../../data/models/article';

@Component({
  selector: 'app-article-summary',
  templateUrl: './article-summary.component.html',
  styleUrls: ['./article-summary.component.css']
})
export class ArticleSummaryComponent implements OnInit {
  @Input()
  article: ArticleSummary;

  username: string;

  liked: boolean;

  constructor(
    private authorizeService: AuthorizeService,
    private articleService: ArticleService,
  ) { }

  ngOnInit() {
    this.authorizeService.getUser().toPromise().then(user => {
      this.username = user.name;
      this.liked = this.article.autor.name == this.username;
    })
  }

  likeArticle(){
    this.articleService.likeArticle(this.article.id).subscribe(() => this.liked = !this.liked);
  }

}
