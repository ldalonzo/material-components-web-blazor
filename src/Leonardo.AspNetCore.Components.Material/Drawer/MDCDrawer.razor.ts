import { MDCDrawer } from '@material/drawer'

class MDCDrawerComponent {
  private readonly componentsById: Map<string, MDCDrawer> = new Map();

  public attachTo(root: Element, id: string): void {
    const drawer = MDCDrawer.attachTo(root)
    this.componentsById.set(id, drawer)
  }

  public toggleOpen(id: string) : void {
    const drawer = this.componentsById.get(id)
    if (drawer) {
      drawer.open = !drawer.open
    }
  }

  public dispose(id: string) : boolean {
    return this.componentsById.delete(id)
  }
}

declare global {
  interface Window { MDCDrawerComponent: MDCDrawerComponent }
}

window.MDCDrawerComponent = new MDCDrawerComponent()
