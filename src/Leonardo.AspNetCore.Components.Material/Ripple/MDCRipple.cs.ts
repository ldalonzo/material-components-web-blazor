import { MDCRipple } from '@material/ripple'

export class MDCRippleComponent {
  attachTo(root: Element) {
    MDCRipple.attachTo(root)
  }
}

declare global {
  interface Window { MDCRippleComponent: MDCRippleComponent }
}

window.MDCRippleComponent = new MDCRippleComponent()
