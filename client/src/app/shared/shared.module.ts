import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AlertModule } from 'ngx-bootstrap/alert';

import { CoreModule } from '../core/core.module';
import { BackButtonComponent } from './back-button.component';
import { DialogService } from './dialog.service';
import { DomainService } from './domain.service';
import { EditColComponent } from './edit-col.component';
import { InfoComponent } from './info.component';
import { ToastComponent } from './toast.component';
import { ValidationErrorsModalComponent } from './validation-errors.modal.component';
import { YesNoModalComponent } from './yes-no-modal.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,

    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    AlertModule.forRoot(),

    CoreModule
  ],
  exports: [
    YesNoModalComponent,
    ValidationErrorsModalComponent,
    ToastComponent,
    BackButtonComponent,
    EditColComponent,
    InfoComponent
  ],
  declarations: [
    EditColComponent,
    InfoComponent,
    YesNoModalComponent,
    ValidationErrorsModalComponent,
    ToastComponent,
    BackButtonComponent
  ],
  providers: [
    DialogService,
    DomainService
  ],
})
export class SharedModule { }
