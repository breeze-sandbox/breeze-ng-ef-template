import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot } from '@angular/router';

import { TemplateManagerProvider } from './entity-manager-provider';

@Injectable()
export class EntityManagerProviderGuard implements CanActivate {
    constructor(private provider: TemplateManagerProvider) { }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const p1 = this.provider.prepare();
        return p1.then(() => true);
    }
}
