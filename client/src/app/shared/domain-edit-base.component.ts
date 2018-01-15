import { UnitOfWork } from '../core/unit-of-work';
import { UtilFns } from '../core/util-fns';
import { BaseEntity } from '../model/entities/base-entity';
import { DomainBaseComponent } from './domain-base.component';
import { DomainService } from './domain.service';

export abstract class DomainEditBaseComponent extends DomainBaseComponent {
  uow: UnitOfWork;
  isPageReady: boolean;
  protected _isDeactivating: boolean;
  protected _wasSavedOrCancelled: boolean;

  constructor(protected _domainService: DomainService) {
    super(_domainService);
  }

  /** for soft deletes */
  deleteEntity(e: BaseEntity) {
    e.entityAspect.setDeleted();
    // if (e.entityAspect && e.entityAspect.entityState.isAdded()) {
    //   e.entityAspect.setDetached();
    // } else {
    //   e['status'] = 3;
    // }
  }

  // call saveChanges with shouldExit false if you don't want to leave page after saveChanges
  saveChanges(message: string = 'Saved', shouldExit: boolean = true) {
    return new Promise((resolve) => {
      setTimeout(() => {
        resolve(this.saveChangesCore(message, shouldExit));
      }, 100);
    });
  }

  saveChangesCore(message: string, shouldExit: boolean) {
    return this.beforeSaveChanges(message).then(() => {
      if (!this.uow.hasChanges()) {
        return Promise.resolve(this.goBackOrReturnTrue(shouldExit));
      }
      return this.uow.commit().then((r) => {
        if (!this.uow.hasChanges()) {
          this._dialogService.toast({ message: message, type: 'success' });
          this._wasSavedOrCancelled = shouldExit && true;
          return this.goBackOrReturnTrue(shouldExit);
        } else {
          const entityErrors = (r as any).entityErrors;
          this._dialogService.showValidationErrors(entityErrors);
        }
      });
    });
  }

  // override to disable save button
  canSave() {
    return true;
  }

  cancelChanges() {
    UtilFns.forceLoseFocus();
    if (!this.hasChanges()) {
      return Promise.resolve(this.goBackOrReturnTrue());
    }
    return this._dialogService.promptToSave().then(r => {
      if (r.index === 2) {
        return false;
      } else if (r.index === 0 && !this.canSave()) {
        this._dialogService.toast({ message: 'Unable to save', type: 'error' });
        return false;
      }
      this._wasSavedOrCancelled = true;
      if (r.index === 0) {
        return this.saveChanges().then(() => true);
      } else if (r.index === 1) {
        this.uow.rollback();
        this._dialogService.toast('Changes discarded');
        return this.goBackOrReturnTrue();
      }
    });
  }

  canDeactivate() {
    if (this._wasSavedOrCancelled) { return true; }
    this._isDeactivating = true;
    return this.cancelChanges().then(r => {
      this._isDeactivating = false;
      return r;
    });
  }

  // should be overridden as needed to update any uow changes that
  // might not have already been completed.
  beforeSaveChanges(message: string): Promise<boolean> {
    return Promise.resolve(true);
  }

  // should be overridden if there is a beforeSaveChanges that can cause
  // changes not already in the the uow.
  hasChanges() {
    return this.uow.hasChanges();
  }

  goBackOrReturnTrue(shouldExit: boolean = true) {
    if (shouldExit && !this._isDeactivating) {
      this.goBack();
    }
    return true;
  }

  abstract goBack();

}

