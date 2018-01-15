import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/mergeMap';

import { Component, OnInit, ViewChild } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';

import { DialogService } from './shared/dialog.service';
import { ToastComponent } from './shared/toast.component';
import { ValidationErrorsModalComponent } from './shared/validation-errors.modal.component';
import { YesNoModalComponent } from './shared/yes-no-modal.component';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Breeze Angular Template';
  @ViewChild(YesNoModalComponent) public yesNoModal: YesNoModalComponent;
  @ViewChild(ValidationErrorsModalComponent) validationErrorsModal: ValidationErrorsModalComponent;
  @ViewChild(ToastComponent) toastComponent: ToastComponent;

  constructor(private _dialogService: DialogService, private router: Router,
    private activatedRoute: ActivatedRoute,
    private titleService: Title) {
  }

  ngOnInit() {
    this._dialogService.yesNoModal = this.yesNoModal;
    this._dialogService.validationErrorsModal = this.validationErrorsModal;
    this._dialogService.toastComponent = this.toastComponent;

    this.subscribeTitles();
  }

  subscribeTitles() {
    // when navigation ends, set the page title to the title data in the route
    // full explanation at https://toddmotto.com/dynamic-page-titles-angular-2-router-events
    this.router.events
    .filter((event) => event instanceof NavigationEnd)
    .map(() => this.activatedRoute)
    .map((route) => {
      while (route.firstChild) { route = route.firstChild; }
      return route;
    })
    .filter((route) => route.outlet === 'primary')
    .mergeMap((route) => route.data)
    .subscribe((event) => {
      const title = 'Template ' + (event['title'] ? ' | ' + event['title'] : '');
      this.titleService.setTitle(title);
    });
  }

}
