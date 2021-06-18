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
    this.commentService.likeComment(this.comment.id).subscribe(() => this.liked = !this.liked);
  }

}
