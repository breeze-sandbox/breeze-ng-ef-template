import { Router } from '@angular/router';
import { UnitOfWork } from 'app/core/unit-of-work';

import { AuthUser } from './../core/auth.service';
import { UtilFns } from './../core/util-fns';
import { DialogService } from './../shared/dialog.service';
import { DomainService } from './domain.service';

export abstract class DomainBaseComponent {
  authUser: AuthUser;
  self: DomainBaseComponent;
  uow: UnitOfWork;
  UtilFns = UtilFns;
  protected _dialogService: DialogService;
  protected _router: Router;

  constructor(protected _domainService: DomainService) {
    this.authUser = _domainService.authService.getUser();
    this.uow = _domainService.uow;
    this._dialogService = _domainService.dialogService;
    this._router = _domainService.router;
    this.self = this;
  }

  hasUserFeature(featureId: number) {
    return (this.authUser.isAdmin);
  }

  /** get named param from the end of the current route */
  getRouteParam(name: string) {
    let rt = this._router.routerState.snapshot.root;
    while (rt.firstChild) { rt = rt.firstChild; }
    return rt.params[name];
  }

  /** get named param from the first occurance in the current route */
  getFirstParam(name: string) {
    let rt = this._router.routerState.snapshot.root;
    while (!rt.params[name] && rt.firstChild) { rt = rt.firstChild; }
    return rt.params[name];
  }
}

