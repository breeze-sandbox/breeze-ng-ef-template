// tslint:disable:no-trailing-whitespace
// tslint:disable:member-ordering
import { BaseEntity } from '../base-entity';

/// <code-import> Place custom imports between <code-import> tags

/// </code-import>

export class User extends BaseEntity  {

  /// <code> Place custom code between <code> tags
  
  /// </code>

  // Generated code. Do not place code below this line.
  id: number;
  userName: string;
  firstName: string;
  lastName: string;
  email: string;
  rowVersion: number;
  createdBy: string;
  createdByUserId: number;
  createdDate: Date;
  modifiedBy: string;
  modifiedByUserId: number;
  modifiedDate: Date;
}

