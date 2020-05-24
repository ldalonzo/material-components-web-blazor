import { MDCTextField } from '@material/textfield'

export class MDCTextFieldComponent {
  attachTo(root: Element, id: string) {
    MDCTextField.attachTo(root)
  }
}

declare global {
  interface Window { MDCTextFieldComponent: MDCTextFieldComponent }
}

window.MDCTextFieldComponent = new MDCTextFieldComponent()
