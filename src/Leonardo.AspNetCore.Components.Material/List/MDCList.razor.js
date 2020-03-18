import { MDCList } from '@material/list'

var MDCListComponent = /** @class */ (function () {
  function MDCListComponent () {
    this.attachTo = (domElement, wrapFocus) => {
      const list = MDCList.attachTo(domElement)
      list.wrapFocus = wrapFocus
    }
  }

  return MDCListComponent
}())

window.MDCListComponent = new MDCListComponent()
