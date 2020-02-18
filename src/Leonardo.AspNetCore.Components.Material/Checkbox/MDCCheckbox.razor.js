import { MDCCheckbox } from '@material/checkbox'

var MDCCheckboxComponent = /** @class */ (function () {
  function MDCCheckboxComponent () {
    var _this = this
    this.componentsById = {}

    this.attachTo = (checkboxElement, dotnetHelper) => {
      const checkbox = new MDCCheckbox(checkboxElement)

      _this.componentsById[checkboxElement.id] = checkbox
    }

    this.setChecked = (checkboxElement, value) => {
      const checkbox = _this.componentsById[checkboxElement.id]
      checkbox.checked = value
    }
  }

  return MDCCheckboxComponent
}())

window.MDCCheckboxComponent = new MDCCheckboxComponent()
