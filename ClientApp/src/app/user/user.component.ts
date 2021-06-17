import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { User } from 'src/data/models/user';

/**
 * The user-page basically, visible to others.
 */
@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  user: User;

  constructor(
    //private readonly userService: UserService
    private readonly route: ActivatedRoute,
  ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      if(params.id){
        //this.userService.get(this.userId).toPromise().then(user => this.user = user);
      }
    })
  }
}
