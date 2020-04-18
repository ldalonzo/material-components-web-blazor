import { MDCTabBar } from '@material/tab-bar'

var MDCTabBarComponent = /** @class */ (function () {
  function MDCTabBarComponent () {
    var _this = this
    this.componentsById = {}

    this.attachTo = (domElement) => {
      _this.componentsById[domElement.id] = MDCTabBar.attachTo(domElement)
    }
  }

  return MDCTabBarComponent
}())

window.MDCTabBarComponent = new MDCTabBarComponent()
