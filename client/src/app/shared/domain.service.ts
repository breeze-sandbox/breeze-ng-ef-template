import { Location } from '@angular/common';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../core/auth.service';
import { ErrorLogger } from '../core/error-logger';
import { UnitOfWork } from '../core/unit-of-work';
import { DialogService } from './dialog.service';


@Injectable()
export class DomainService {

  constructor(public uow: UnitOfWork, public authService: AuthService,
    public dialogService: DialogService, public errorLogger: ErrorLogger,
    public router: Router, public location: Location ) {
      // Note: can't include ActivatedRoute or ElementRef here
  }

}
