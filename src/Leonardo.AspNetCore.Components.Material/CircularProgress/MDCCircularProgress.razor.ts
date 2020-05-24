import { MDCCircularProgress } from '@material/circular-progress'

class MDCCircularProgressComponent {
  private readonly componentsById: Map<string, MDCCircularProgress> = new Map();

  public attachTo(root: Element, id: string) : void {
    const circularProgress = MDCCircularProgress.attachTo(root)
    this.componentsById.set(id, circularProgress )
  }

  public open(id: string) : void {
    const circularProgress = this.componentsById.get(id)
    if (circularProgress) {
      circularProgress.open()
    }
  }

  public setDeterminate(id: string, value: boolean) {
    const circularProgress = this.componentsById.get(id)
    if (circularProgress) {
      circularProgress.determinate = value
    }
  }

  public close(id: string) : void {
    const circularProgress = this.componentsById.get(id)
    if (circularProgress) {
      circularProgress.close()
    }
  }

  public dispose(id: string) : boolean {
    return this.componentsById.delete(id)
  }
}

declare global {
  interface Window { MDCCircularProgressComponent: MDCCircularProgressComponent }
}

window.MDCCircularProgressComponent = new MDCCircularProgressComponent()
