import { MDCSnackbar } from '@material/snackbar'

var MDCSnackbarComponent = /** @class */ (function () {
  function MDCSnackbarComponent () {
    var _this = this
    this.componentsById = {}

    this.attachTo = (domElement) => {
      _this.componentsById[domElement.id] = MDCSnackbar.attachTo(domElement)
    }
  }

  return MDCSnackbarComponent
}())

window.MDCSnackbarComponent = new MDCSnackbarComponent()
