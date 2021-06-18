import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserSummary } from 'src/data/models/user';
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

  editMode: boolean = false;

  constructor(
    private readonly articleService: ArticleService,
    private readonly route: ActivatedRoute,
    private readonly authorizeService: AuthorizeService,
  ) { }

  async ngOnInit() {
    this.route.params.subscribe(async (params) => {
      if (params.id) {
        if (params.id == 'new') {
          this.article = new Article();
          this.article.datum = new Date(Date.now());
          this.article.autor = new UserSummary();
          await this.authorizeService.getUser().subscribe(user => {
            console.log(user);
            this.article.autor.name = user.name;
          });
          this.editMode = true;
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
