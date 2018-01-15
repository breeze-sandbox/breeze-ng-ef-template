import { MetadataStore } from 'breeze-client';

import { User } from './user';

export class TemplateRegistrationHelper {

    static register(metadataStore: MetadataStore) {
        metadataStore.registerEntityTypeCtor('User', User);
    }
}
