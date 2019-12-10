import { MDCSelect } from '@material/select'

var MDCSelectComponent = /** @class */ (function () {
  function MDCSelectComponent () {
    this.attachTo = (domElement, dotnetHelper) => {
      const select = new MDCSelect(domElement)
      select.listen('MDCSelect:change', () => {
        dotnetHelper.invokeMethodAsync('OnChange', select.value, select.selectedIndex)
      })
    }
  }

  return MDCSelectComponent
}())

window.MDCSelectComponent = new MDCSelectComponent()
