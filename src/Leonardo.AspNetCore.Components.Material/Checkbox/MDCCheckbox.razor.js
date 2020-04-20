import { MDCCheckbox } from '@material/checkbox'

var MDCCheckboxComponent = /** @class */ (function () {
  function MDCCheckboxComponent () {
    var _this = this
    this.componentsById = {}

    this.attachTo = (domElement, id) => {
      _this.componentsById[id] = MDCCheckbox.attachTo(domElement)
    }

    this.setChecked = (id, value) => {
      const checkbox = _this.componentsById[id]
      checkbox.checked = value
    }
  }

  return MDCCheckboxComponent
}())

window.MDCCheckboxComponent = new MDCCheckboxComponent()
