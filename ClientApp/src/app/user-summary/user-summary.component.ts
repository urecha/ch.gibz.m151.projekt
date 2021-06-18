import { Component, Input, OnInit } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { User, UserSummary } from 'src/data/models/user';
import { UserService } from 'src/data/services/user.service';

/**
 * Though named accordingly, this component doesn't use the "UserSummary" data model, as that one
 * is meant to go within other models. The here used model is more fleshed out and provides
 * a rich overview.
 */
@Component({
  selector: 'app-user-summary',
  templateUrl: './user-summary.component.html',
  styleUrls: ['./user-summary.component.css']
})
export class UserSummaryComponent implements OnInit {
  @Input()
  user: User;

  picture: SafeHtml;

  constructor(
    private readonly userService: UserService,
    private readonly domSanitizer: DomSanitizer,
  ) { }

  ngOnInit() {
    this.picture = this.domSanitizer.bypassSecurityTrustHtml(this.userService.getGenericProfilePicture(this.user.name));
  }

}
