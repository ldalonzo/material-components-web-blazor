import { MDCSelect } from '@material/select'

var MDCSelectComponent = /** @class */ (function () {
  function MDCSelectComponent () {
    this.attachTo = (domElement) => {
      const select = new MDCSelect(domElement)
    }
  }

  return MDCSelectComponent
}())

window.MDCSelectComponent = new MDCSelectComponent()
