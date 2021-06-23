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
    this.toggleLikes();
  }

  private toggleLikes(){
    this.authorizeService.getUser().toPromise().then(user => {
      this.liked = this.article.beitragLikes.find(bl => !bl.istDislike && bl.user.name == user.name) ? true : false;
      this.disliked = this.article.beitragLikes.find(bl => bl.istDislike && bl.user.name == user.name) ? true : false;
    })
  }

  async likeArticle() {
    this.articleService.likeArticle(this.article.id).subscribe(async (alike) => {
      if(alike == null){
        const user = await this.authorizeService.getUser().toPromise();
        let index = this.article.beitragLikes.indexOf(this.article.beitragLikes.find(l => l.user.name == user.name));
        this.article.beitragLikes.splice(index, 1);
      } else{
        this.article.beitragLikes.push(alike);
      }
      this.toggleLikes();
    });
  }

  async dislikeArticle() {
    this.articleService.dislikeArticle(this.article.id).subscribe(async (alike) => {
      if(alike == null){
        const user = await this.authorizeService.getUser().toPromise();
        let index = this.article.beitragLikes.indexOf(this.article.beitragLikes.find(l => l.user.name == user.name));
        this.article.beitragLikes.splice(index, 1);
      } else{
        this.article.beitragLikes.push(alike);
      }
      this.toggleLikes();
    });
  }

}
