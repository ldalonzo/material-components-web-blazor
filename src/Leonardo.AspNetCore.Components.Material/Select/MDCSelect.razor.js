import { MDCSelect } from '@material/select'

var MDCSelectComponent = /** @class */ (function () {
  function MDCSelectComponent () {
    var _this = this
    this.componentsById = {}

    this.attachTo = (domElement, id) => {
      _this.componentsById[id] = MDCSelect.attachTo(domElement)
    }

    this.listenToChange = (id, dotnetHelper) => {
      const select = _this.componentsById[id]
      select.listen('MDCSelect:change', evt => {
        dotnetHelper.invokeMethodAsync('OnChange', evt.detail)
      })
    }

    this.setSelectedIndex = (id, selectedIndex) => {
      const select = _this.componentsById[id]
      select.selectedIndex = selectedIndex
    }
  }

  return MDCSelectComponent
}())

window.MDCSelectComponent = new MDCSelectComponent()
