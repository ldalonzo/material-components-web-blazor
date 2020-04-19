import { MDCTopAppBar } from '@material/top-app-bar'

var MDCTopAppBarComponent = /** @class */ (function () {
  function MDCTopAppBarComponent () {
    var _this = this
    this.componentsById = {}

    this.attachTo = (domElement, id) => {
      const topAppBar = MDCTopAppBar.attachTo(domElement)
      _this.componentsById[id] = topAppBar
    }

    this.listenToNav = (id, dotnetHelper) => {
      const topAppBar = _this.componentsById[id]
      topAppBar.listen('MDCTopAppBar:nav', () => {
        dotnetHelper.invokeMethodAsync('OnMDCTopAppBarNav')
      })
    }
  }

  return MDCTopAppBarComponent
}())

window.MDCTopAppBarComponent = new MDCTopAppBarComponent()
