import { MDCRipple } from '@material/ripple'

var MDCRippleComponent = /** @class */ (function () {
  function MDCRippleComponent () {
    this.attachTo = (domElement) => {
      MDCRipple.attachTo(domElement)
    }
  }

  return MDCRippleComponent
}())

window.MDCRippleComponent = new MDCRippleComponent()
