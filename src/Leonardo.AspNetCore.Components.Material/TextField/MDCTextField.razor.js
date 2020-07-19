import { MDCTextField } from '@material/textfield'

var MDCTextFieldComponent = /** @class */ (function () {
  function MDCTextFieldComponent () {
    this.attachTo = (domElement, id) => {
      MDCTextField.attachTo(domElement)
    }
  }

  return MDCTextFieldComponent
}())

window.MDCTextFieldComponent = new MDCTextFieldComponent()