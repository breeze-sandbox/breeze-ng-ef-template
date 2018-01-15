import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuardService } from './core/auth-guard.service';
import { CoreModule } from './core/core.module';
import { EntityManagerProviderGuard } from './core/entity-manager-guard';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './home/login.component';

const appRoutes: Routes = [
  {
    path: 'login',
    data: { title: 'Login' },
    component: LoginComponent
  }, {
    path: 'home',
    data: { title: 'Home' },
    component: HomeComponent,
    canActivate: [AuthGuardService, EntityManagerProviderGuard],
  }, {
    path: '**',
    data: { title: 'Home' },
    component: HomeComponent,
    canActivate: [AuthGuardService, EntityManagerProviderGuard],
  }
];
@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes),
    CoreModule
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
