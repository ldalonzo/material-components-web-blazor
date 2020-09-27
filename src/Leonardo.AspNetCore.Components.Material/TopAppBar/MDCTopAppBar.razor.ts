import { MDCTopAppBar } from '@material/top-app-bar'

class MDCTopAppBarComponent {
  private readonly componentsById: Map<string, MDCTopAppBar> = new Map();

  public attachTo(root: Element, id: string) : void {
    const topAppBar = MDCTopAppBar.attachTo(root)
    this.componentsById.set(id, topAppBar)
  }

  public listenToNav(id: string, handler: dotNetHandler) : void {
    const topAppBar = this.componentsById.get(id)
    if (topAppBar) {
      topAppBar.listen('MDCTopAppBar:nav', () => {
        handler.invokeMethodAsync('OnMDCTopAppBarNav')
      })
    }
  }

  public dispose(id: string) : boolean {
    return this.componentsById.delete(id)
  }
}

declare global {
  interface Window { MDCTopAppBarComponent: MDCTopAppBarComponent }
}

interface dotNetHandler {
  invokeMethodAsync<T>(methodName: string, ...args: any): Promise<T>;
}

window.MDCTopAppBarComponent = new MDCTopAppBarComponent()
