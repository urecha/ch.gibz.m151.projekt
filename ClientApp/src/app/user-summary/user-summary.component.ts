import { Component, Input, OnInit } from '@angular/core';
import { UserSummary } from 'src/data/models/user';

@Component({
  selector: 'app-user-summary',
  templateUrl: './user-summary.component.html',
  styleUrls: ['./user-summary.component.css']
})
export class UserSummaryComponent implements OnInit {
  @Input()
  userId: string;

  summary: UserSummary;

  constructor(
    //private readonly userService: UserService
  ) { }

  ngOnInit() {
    //this.userService.getSummary(this.userId).toPromise().then(summary => this.summary = summary);
  }

}
