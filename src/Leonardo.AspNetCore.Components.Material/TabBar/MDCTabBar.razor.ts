import { MDCTabBar, MDCTabBarActivatedEvent } from '@material/tab-bar'

class MDCTabBarComponent {
  private readonly componentsById: Map<string, MDCTabBar> = new Map();

  public attachTo(root: Element, id: string): void {
    const tabBar = MDCTabBar.attachTo(root)
    this.componentsById.set(id, tabBar)
  }

  public listenToActivated(id: string, handler: dotNetHandler): void {
    const tabBar = this.componentsById.get(id)
    if (tabBar) {
      tabBar.listen<MDCTabBarActivatedEvent>('MDCTabBar:activated', evt => {
        handler.invokeMethodAsync('OnTabActivated', evt.detail)
      })
    }
  }

  public dispose(id: string): boolean {
    return this.componentsById.delete(id)
  }
}

declare global {
  interface Window { MDCTabBarComponent: MDCTabBarComponent }
}

interface dotNetHandler {
  invokeMethodAsync<T>(methodName: string, ...args: any): Promise<T>;
}

window.MDCTabBarComponent = new MDCTabBarComponent()
