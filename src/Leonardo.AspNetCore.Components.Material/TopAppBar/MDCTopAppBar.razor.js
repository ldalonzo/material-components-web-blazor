import { MDCTopAppBar } from '@material/top-app-bar'

var MDCTopAppBarComponent = /** @class */ (function () {
  function MDCTopAppBarComponent () {
    var _this = this
    this.componentsById = {}

    this.attachTo = (domElement) => {
      const topAppBar = MDCTopAppBar.attachTo(domElement)
      _this.componentsById[domElement.id] = topAppBar
    }

    this.setScrollTarget = (target) => {
      _this.topAppBar.setScrollTarget(target)

      return true
    }

    this.listenToNav = (domElement, dotnetHelper) => {
      const topAppBar = _this.componentsById[domElement.id]
      topAppBar.listen('MDCTopAppBar:nav', () => {
        dotnetHelper.invokeMethodAsync('OnMDCTopAppBarNav')
      })
    }
  }

  return MDCTopAppBarComponent
}())

window.MDCTopAppBarComponent = new MDCTopAppBarComponent()
