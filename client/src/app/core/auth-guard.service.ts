import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';

import { AuthService } from './auth.service';

@Injectable()
export class AuthGuardService implements CanActivate {

  constructor(private _router: Router, private _authService: AuthService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this._authService.isLoggedIn().then(t => {
      this._authService.redirectUrl = state.url;
      if (!t) {
        this._router.navigate(['/login'], {skipLocationChange: true});
        return false;
      }

      // make sure user has permissions to the route
      const user = this._authService.getUser();

      if (user.isAdmin) { return true; }
      // ...

      return t;
    });
  }
}
