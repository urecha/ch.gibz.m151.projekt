import { Component, Input, OnInit } from '@angular/core';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { ArticleLike } from 'src/data/models/articleLike';
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
    this.toggleLikes();
  }

  private toggleLikes(){
    this.authorizeService.getUser().toPromise().then(user => {
      this.liked = this.article.beitragLikes.find(bl => !bl.istDislike && bl.user.name == user.name) ? true : false;
      this.disliked = this.article.beitragLikes.find(bl => bl.istDislike && bl.user.name == user.name) ? true : false;
    })
  }

  likeArticle() {
    this.articleService.likeArticle(this.article.id).subscribe(() => {
      this.liked = !this.liked;
      let like = new ArticleLike();
      like.istDislike = false;
      this.article.beitragLikes.push(like);
    });
  }

  dislikeArticle() {
    this.articleService.dislikeArticle(this.article.id).subscribe(() => {
      this.disliked = !this.disliked
      let like = new ArticleLike();
      like.istDislike = true;
      this.article.beitragLikes.push(like);
    });
  }

}
