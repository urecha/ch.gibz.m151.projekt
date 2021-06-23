import { Component, Input, OnInit } from '@angular/core';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { Comment } from 'src/data/models/comment';
import { CommentLike } from 'src/data/models/commentLike';
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

  likeComment(){
    this.commentService.likeComment(this.comment.id).subscribe(() => {
      this.liked = !this.liked
      let mockLike = new CommentLike();
      mockLike.istDislike = false;
      this.comment.likes.push(mockLike);
    });
  }

  dislikeComment(){
    this.commentService.dislikeComment(this.comment.id).subscribe(() => {
      this.disliked = !this.disliked
      let mockLike = new CommentLike();
      mockLike.istDislike = true;
      this.comment.likes.push(mockLike);
    });
  }

}
