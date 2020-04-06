import { MDCSnackbar } from '@material/snackbar'

var MDCSnackbarComponent = /** @class */ (function () {
  function MDCSnackbarComponent () {
    var _this = this
    this.componentsById = {}

    this.attachTo = (domElement) => {
      _this.componentsById[domElement.id] = MDCSnackbar.attachTo(domElement)
    }

    this.open = (domElement) => {
      _this.componentsById[domElement.id].open()
    }

    this.setLabelText = (domElement, labelText) => {
      _this.componentsById[domElement.id].labelText = labelText
    }
  }

  return MDCSnackbarComponent
}())

window.MDCSnackbarComponent = new MDCSnackbarComponent()
