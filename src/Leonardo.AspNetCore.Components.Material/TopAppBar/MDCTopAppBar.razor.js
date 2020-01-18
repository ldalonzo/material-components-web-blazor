import { MDCTopAppBar } from '@material/top-app-bar'

var MDCTopAppBarComponent = /** @class */ (function () {
  function MDCTopAppBarComponent () {
    var _this = this

    this.attachTo = (domElement) => {
      _this.topAppBar = MDCTopAppBar.attachTo(domElement)

      return true
    }

    this.setScrollTarget = (target) => {
      _this.topAppBar.setScrollTarget(target)

      return true
    }

    this.listenToNav = (dotnetHelper) => {
      _this.topAppBar.listen('MDCTopAppBar:nav', () => {
        dotnetHelper.invokeMethodAsync('OnMDCTopAppBarNav')
      })

      return true
    }
  }

  return MDCTopAppBarComponent
}())

window.MDCTopAppBarComponent = new MDCTopAppBarComponent()
