import { Component, Input, OnInit } from '@angular/core';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { Comment } from 'src/data/models/comment';
import { CommentService } from 'src/data/services/comment.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {
  @Input()
  comment: Comment;

  liked: boolean;

  disliked: boolean;

  get actualLikes(): number{
    if(!this.comment) return 0;
    return this.comment.likes.filter(l => !l.istDislike).length;
  }

  get dislikes(): number{
    if(!this.comment) return 0;
    return this.comment.likes.filter(l => l.istDislike).length;
  }

  constructor(
    private authorizeService: AuthorizeService,
    private commentService: CommentService,
  ) { }

  ngOnInit() {
    this.authorizeService.getUser().toPromise().then(user => {
      this.liked = this.comment.autor.name === user.name;
    })
  }

  private toggleLikes(){
    this.authorizeService.getUser().toPromise().then(user => {
      this.liked = this.comment.likes.find(cl => !cl.istDislike && cl.user.name == user.name) ? true : false;
      this.disliked = this.comment.likes.find(cl => cl.istDislike && cl.user.name == user.name) ? true : false;
    })
  }

  likeComment(){
    this.commentService.likeComment(this.comment.id).subscribe(async (alike) => {
      if(alike == null){
        const user = await this.authorizeService.getUser().toPromise();
        let index = this.comment.likes.indexOf(this.comment.likes.find(l => l.user.name == user.name));
        this.comment.likes.splice(index, 1);
      } else{
        this.comment.likes.push(alike);
      }
      this.toggleLikes();
    });
  }

  dislikeComment(){
    this.commentService.dislikeComment(this.comment.id).subscribe(async (alike) => {
      if(alike == null){
        const user = await this.authorizeService.getUser().toPromise();
        let index = this.comment.likes.indexOf(this.comment.likes.find(l => l.user.name == user.name));
        this.comment.likes.splice(index, 1);
      } else{
        this.comment.likes.push(alike);
      }
      this.toggleLikes();
    });
  }

}
