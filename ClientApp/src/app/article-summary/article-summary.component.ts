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

  get likes(): number{
    if(!this.article.beitragLikes || !this.article.beitragLikes.length) return 0;
    return this.article.beitragLikes.filter(bl => !bl.istDislike).length;
  }

  get dislikes(): number{
    if(!this.article.beitragLikes || !this.article.beitragLikes.length) return 0;
    return this.article.beitragLikes.filter(bl => bl.istDislike).length;
  }

  constructor(
    private authorizeService: AuthorizeService,
    private articleService: ArticleService,
  ) { }

  ngOnInit() {
    this.toggleLikes();
  }

  private toggleLikes(){
    this.authorizeService.getUser().subscribe(user => {
      this.liked = this.article.beitragLikes.find(bl => !bl.istDislike && bl.user.name == user.name) ? true : false;
      this.disliked = this.article.beitragLikes.find(bl => bl.istDislike && bl.user.name == user.name) ? true : false;
    })
  }

  likeArticle() {
    if(this.liked) return;
    this.articleService.likeArticle(this.article.id).subscribe(() => {
      this.liked = true;
      let like = new ArticleLike();
      like.istDislike = false;
      this.article.beitragLikes.push(like);
    });
  }

  dislikeArticle() {
    if(this.disliked) return;
    this.articleService.dislikeArticle(this.article.id).subscribe(() => {
      this.disliked = true;
      let like = new ArticleLike();
      like.istDislike = true;
      this.article.beitragLikes.push(like);
    });
  }

}
