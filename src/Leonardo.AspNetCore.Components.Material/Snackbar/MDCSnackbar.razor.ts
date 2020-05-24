import { MDCSnackbar } from '@material/snackbar'

class MDCSnackbarComponent {
  readonly componentsById: any;

  constructor() {
    this.componentsById = {}
  }

  public attachTo(root: Element, id: string) {
    const snackbar = MDCSnackbar.attachTo(root)
    this.componentsById[id] = snackbar
  }

  public open(id: string) {
    const snackbar = this.componentsById[id]
    snackbar.open()
  }

  public setLabelText(id: string, labelText: string) {
    const snackbar = this.componentsById[id]
    snackbar.labelText = labelText
  }
}

declare global {
  interface Window { MDCSnackbarComponent: MDCSnackbarComponent }
}

window.MDCSnackbarComponent = new MDCSnackbarComponent()
