import { MDCSelect } from '@material/select'

var MDCSelectComponent = /** @class */ (function () {
  function MDCSelectComponent () {
    var _this = this
    this.componentsById = {}

    this.attachTo = (domElement, dotnetHelper) => {
      const select = new MDCSelect(domElement)
      select.listen('MDCSelect:change', () => {
        dotnetHelper.invokeMethodAsync('OnChange', select.value, select.selectedIndex)
      })

      _this.componentsById[domElement.id] = select
    }

    this.setSelectedIndex = (domElement, selectedIndex) => {
      const select = _this.componentsById[domElement.id]
      select.selectedIndex = selectedIndex
    }
  }

  return MDCSelectComponent
}())

window.MDCSelectComponent = new MDCSelectComponent()
