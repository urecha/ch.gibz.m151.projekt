import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { Comment } from 'src/data/models/comment';
import { UserSummary } from 'src/data/models/user';
import { ArticleService } from 'src/data/services/article.service';
import { CommentService } from 'src/data/services/comment.service';
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

  commentMode: boolean = false;
  comment: string;
  commentTitel: string;

  liked: boolean;
  disliked: boolean;

  constructor(
    private readonly articleService: ArticleService,
    private readonly commentService: CommentService,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly authorizeService: AuthorizeService,
  ) { }

  async ngOnInit() {
    this.route.params.subscribe(async (params) => {
      if (params.id) {
        if (params.id == 'new') {
          this.article = new Article();

          const now = new Date(Date.now());
          this.article.datum = new Date(now.getTime() + now.getTimezoneOffset() * 60000);
          console.log(this.article.datum);

          this.article.autor = new UserSummary();
          this.article.beitragLikes = [];
          this.article.kommentare = [];

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

  async saveArticle() {
    if (!confirm("Besch secher? D'warnig und so hesch glese und bisch der bewusst was fuer Folge das ganze chan ha?")) {
      return;
    }
    try {
      this.article = await this.articleService.createOrUpdate(this.article).toPromise();
      this.editMode = false;
      this.router.navigate(['/article/' + this.article.id]);
    } catch (error) {
      console.log(error);
    }
  }

  saveComment() {
    const comment = new Comment();
    comment.articleId = this.article.id;
    comment.autor = new UserSummary();

    const now = new Date(Date.now());
    comment.datum = new Date(now.getTime() + now.getTimezoneOffset() * 60000);

    comment.inhalt = this.comment;
    comment.titel = this.commentTitel;
    comment.likes = [];

    this.commentService.createOrUpdate(comment).subscribe(comment => {
      this.article.kommentare.push(comment);
      this.commentMode = false;
      this.comment = '';
      this.commentTitel = '';
    });
  }

  likeArticle() {
    this.authorizeService.isAuthenticated().subscribe(authenticated => {
      if (!authenticated) {
        console.log('Unauthorized!');
        return;
      }
      this.articleService.likeArticle(this.article.id).subscribe(() => this.liked = !this.liked);
    })
  }

  dislikeArticle() {
    this.authorizeService.isAuthenticated().subscribe(authenticated => {
      if (!authenticated) {
        console.log('Unauthorized!');
        return;
      }
      this.articleService.dislikeArticle(this.article.id).subscribe(() => this.disliked = !this.disliked);
    })
  }
}
