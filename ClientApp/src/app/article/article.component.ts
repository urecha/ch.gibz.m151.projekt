import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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

  liked: boolean;
  disliked: boolean;

  constructor(
    private readonly articleService: ArticleService,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
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
            this.article.autor.name = user.name;
          });
          this.editMode = true;
        } else {
          try {
            this.loading = true;
            this.article = await this.articleService.get(params.id).toPromise();

            this.authorizeService.getUser().toPromise().then(user => {
              this.liked = this.article.beitragLikes.find(bl => !bl.istDislike && bl.user.name == user.name) ? true : false;
              this.disliked = this.article.beitragLikes.find(bl => bl.istDislike && bl.user.name == user.name) ? true : false;
            })
          } catch (error) {
            console.log(error);
          } finally {
            this.loading = false;
          }
        }
      }
    })
  }

  async saveArticle(){
    if(!confirm("Besch secher? D'warnig und so hesch glese und bisch der bewusst was fuer Folge das ganze chan ha?")){
      return;
    }
    try{
      this.article = await this.articleService.createOrUpdate(this.article).toPromise();
      this.editMode = false;
      this.router.navigate(['/article/' + this.article.id]);
    } catch(error){
      console.log(error);
    }
  }

  likeArticle(){
    this.authorizeService.isAuthenticated().subscribe(authenticated => {
      if(!authenticated){
        console.log('Unauthorized!');
        return;
      }
      this.articleService.likeArticle(this.article.id).subscribe(() => this.liked = !this.liked);
    })
  }

  dislikeArticle(){
    this.authorizeService.isAuthenticated().subscribe(authenticated => {
      if(!authenticated){
        console.log('Unauthorized!');
        return;
      }
      this.articleService.dislikeArticle(this.article.id).subscribe(() => this.disliked = !this.disliked);
    })
  }
}
