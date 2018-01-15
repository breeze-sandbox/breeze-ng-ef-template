import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BreezeBridgeHttpClientModule } from 'breeze-bridge2-angular';

import { AuthGuardService } from './auth-guard.service';
import { AuthHttpInterceptor } from './auth-http-interceptor';
import { CanDeactivateGuard } from './can-deactivate-guard.service';
import { EntityManagerProviderGuard } from './entity-manager-guard';
import { ErrorLogger } from './error-logger';
import { TemplateManagerProvider } from 'app/core/entity-manager-provider';
import { UnitOfWork } from 'app/core/unit-of-work';
import { AuthService } from 'app/core/auth.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    BreezeBridgeHttpClientModule
  ],
  exports: [
  ],
  declarations: [
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthHttpInterceptor,
      multi: true
    },
    ErrorLogger,
    CanDeactivateGuard,
    AuthService,
    AuthGuardService,
    TemplateManagerProvider,
    EntityManagerProviderGuard,
    UnitOfWork
  ],
})
export class CoreModule { }
