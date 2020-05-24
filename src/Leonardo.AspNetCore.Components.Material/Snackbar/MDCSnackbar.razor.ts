import { MDCSnackbar } from '@material/snackbar'

class MDCSnackbarComponent {
  private readonly componentsById: Map<string, MDCSnackbar> = new Map();

  public attachTo(root: Element, id: string) : void {
    const snackbar = MDCSnackbar.attachTo(root)
    this.componentsById.set(id, snackbar)
  }

  public open(id: string) : void {
    const snackbar = this.componentsById.get(id)
    if (snackbar) {
      snackbar.open()
    }
  }

  public setLabelText(id: string, labelText: string) : void {
    const snackbar = this.componentsById.get(id)
    if (snackbar) {
      snackbar.labelText = labelText
    }
  }

  public dispose(id: string) : boolean {
    return this.componentsById.delete(id)
  }
}

declare global {
  interface Window { MDCSnackbarComponent: MDCSnackbarComponent }
}

window.MDCSnackbarComponent = new MDCSnackbarComponent()
