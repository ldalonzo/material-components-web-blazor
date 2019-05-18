import { MDCRipple } from '@material/ripple'

var MDCRippleComponent = /** @class */ (function () {
  function MDCRippleComponent () {
    var _this = this
    this.ripples = {}

    this.attachTo = (domElement) => {
      _this.ripples[domElement] = new MDCRipple(domElement)
      return true
    }
  }

  return MDCRippleComponent
}())

window.MDCRippleComponent = new MDCRippleComponent()
