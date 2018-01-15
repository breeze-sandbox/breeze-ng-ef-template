import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';

import { MessageBus } from '../core/message-bus';

export interface ToastConfig {
  message: string;
  title?: string;
  type?: 'info' | 'warning' | 'danger' | 'error' | 'success',
  displayTime?: number;
  visible?: boolean;
}

@Component({
  selector: 'app-toast',
  template: `
  <alert *ngIf="config" [type]="config.type" [dismissOnTimeout]="config.displayTime">
    <strong>{{config.title}}</strong> {{config.message}}
  </alert>
  `
})
export class ToastComponent implements OnInit, OnDestroy {
  config: ToastConfig;
  subscription: Subscription;

  ngOnInit() {
    this.subscription = MessageBus.messages.subscribe(msg => {
      if (msg && msg.type) {
        this.show(msg);
      }
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  show(config: ToastConfig) {
    config.message = config.message || '...';
    config.type = config.type || 'info';
    config.displayTime = config.displayTime || (config.type === 'danger' ? 4000 : 2000);
    config.visible = true;
    if (config.type === 'error') {
      config.type = 'danger';
    }
    this.config = config;
  }

}

