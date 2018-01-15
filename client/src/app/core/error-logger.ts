import { Injectable } from '@angular/core';
import { MessageBus } from 'app/core/message-bus';

import { UtilFns } from './util-fns';

// import notify from 'devextreme/ui/notify';

export type ErrorLevel = 'info'|'warning'|'error'|'success';

export class ErrorEntry {
  constructor(public error: any, public errorLevel: ErrorLevel) {

  }

  formatError() {
    return UtilFns.getErrorMessage(this.error);
  }
}

@Injectable()
export class ErrorLogger {
  _errors: ErrorEntry[] = [];
  constructor() {
  }

  log(e: string | any, errorLevel?: ErrorLevel) {
    console.log(e);
    errorLevel = errorLevel || 'error';
    const ee = new ErrorEntry(e, errorLevel);
    this._errors.push(ee);
    // shows a toast
    MessageBus.notify({ message: ee.formatError(), type: errorLevel });
  }

  getErrors() {
    return this._errors;
  }
}
