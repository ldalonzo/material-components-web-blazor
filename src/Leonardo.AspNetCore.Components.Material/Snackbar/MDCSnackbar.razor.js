import { MDCSnackbar } from '@material/snackbar'

var MDCSnackbarComponent = /** @class */ (function () {
  function MDCSnackbarComponent () {
    var _this = this
    this.componentsById = {}

    this.attachTo = (domElement, id) => {
      _this.componentsById[id] = MDCSnackbar.attachTo(domElement)
    }

    this.open = (id) => {
      _this.componentsById[id].open()
    }

    this.setLabelText = (id, labelText) => {
      _this.componentsById[id].labelText = labelText
    }
  }

  return MDCSnackbarComponent
}())

window.MDCSnackbarComponent = new MDCSnackbarComponent()
