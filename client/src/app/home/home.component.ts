import { Component, ViewEncapsulation } from '@angular/core';
import { DomainBaseComponent } from 'app/shared/domain-base.component';
import { DomainService } from 'app/shared/domain.service';
import { User } from 'app/model/entities/template/user';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class HomeComponent extends DomainBaseComponent implements OnInit {

  users: User[];

  constructor(domainService: DomainService) {
    super(domainService);
  }

  ngOnInit() {
    const p1 = this.uow.createQuery(User).take(10).execute().then(r => {
      this.users = r;
    });
  }

  get user() {
    return this.authUser;
  }

  navigateHome() {
    this._router.navigate(['/home']);
  }
}
