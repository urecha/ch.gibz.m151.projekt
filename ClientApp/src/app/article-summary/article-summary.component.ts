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

  liked: boolean;
  disliked: boolean;

  constructor(
    private authorizeService: AuthorizeService,
    private articleService: ArticleService,
  ) { }

  ngOnInit() {
    this.authorizeService.getUser().toPromise().then(user => {
      let username = user.name;
      this.liked = this.article.beitragLikes.find(bl => !bl.istDislike && bl.user.name == username) ? true : false;
      this.disliked = this.article.beitragLikes.find(bl => bl.istDislike && bl.user.name == username) ? true : false;
    })
  }

  likeArticle(){
    this.authorizeService.isAuthenticated().subscribe(authenticated => {
      if(!authenticated){
        return;
      }
      this.articleService.likeArticle(this.article.id).subscribe(() => this.liked = !this.liked);
    })
  }

  dislikeArticle(){
    this.authorizeService.isAuthenticated().subscribe(authenticated => {
      if(!authenticated){
        return;
      }
      this.articleService.dislikeArticle(this.article.id).subscribe(() => this.disliked = !this.disliked);
    })
  }

}
