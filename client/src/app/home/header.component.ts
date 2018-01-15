import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from './../core/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html'
})

export class HeaderComponent implements OnInit {
  version = '1.0.44';


  constructor(private _router: Router, private _authService: AuthService) {
  }

  ngOnInit() { }

  get user() {
    return this._authService.getUser();
  }

  logout() {
    this._authService.redirectUrl = null;
    this._authService.logout();
  }

}

