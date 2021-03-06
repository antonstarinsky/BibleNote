
import { APP_INITIALIZER, Injector } from '@angular/core';
import { Store, StoreConfig, AllLogEventTypes } from 'reduce-store';
import { InitializeService } from './shared/services/initialize.service';

export const AppInitializer = {
  provide: APP_INITIALIZER,
  useFactory: initializeFactory,
  deps: [Injector],
  multi: true
}

export function initializeFactory(injector: Injector) {
  return () => {
    Store.config.set({
      cloneMethodName: 'clone', // optional, if Falsy then states are muttable
      disposeMethodName: 'ngOnDestroy', // optional, but required for usage of Store.state.subscribe method
      resolver: injector // optional, but required for ReduceStore service
    });

    //Store.logging.setConfiguration([]);
    //Store.logging.turnOn();

    const initService = injector.get(InitializeService);
    initService.init();
  }
}
