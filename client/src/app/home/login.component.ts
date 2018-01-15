import { AfterViewInit, Component, Renderer2, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { AuthService } from '../core/auth.service';
import { ErrorLogger } from '../core/error-logger';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class LoginComponent implements AfterViewInit {
  username = 'guest';
  password = 'guess';
  error = '';
  success = '';
  working = false;

  constructor(private _authService: AuthService,
              private _renderer: Renderer2,
              private _route: ActivatedRoute, private _logger: ErrorLogger) {
  }

  ngAfterViewInit() {
    // put cursor in userName element
    const nameInput = this._renderer.selectRootElement('#username');
    nameInput.focus();
  }

  onSubmit() {
    this.error = '';
    if (!this.working) {
      this.working = true;
      this._authService.login(this.username, this.password).then(_ => {
        this.error = null;
        const user = this._authService.getUser();
        this.success = 'Logged in';
      }, error => {
        this._logger.log(error, 'error');
        if (error.status === 0) {
          this.error = 'Unable to contact server';
        } else {
          this.error = 'Invalid login name or password';
        }
        this.working = false;
      });
    }
  }

}
