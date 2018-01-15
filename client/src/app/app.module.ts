import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule, Title } from '@angular/platform-browser';
import { NgIdleKeepaliveModule } from '@ng-idle/keepalive';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthService } from './core/auth.service';
import { CoreModule } from './core/core.module';
import { HeaderComponent } from './home/header.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './home/login.component';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    LoginComponent,

  ],
  imports: [
    BrowserModule,
    FormsModule,

    CoreModule,
    SharedModule,
    AppRoutingModule,

    NgIdleKeepaliveModule.forRoot()
  ],
  providers: [
    AuthService,
    Title
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

