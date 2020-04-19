import { MDCTabBar } from '@material/tab-bar'

var MDCTabBarComponent = /** @class */ (function () {
  function MDCTabBarComponent () {
    var _this = this
    this.componentsById = {}

    this.attachTo = (domElement, id) => {
      _this.componentsById[id] = MDCTabBar.attachTo(domElement)
    }

    this.listenToActivated = (id, dotnetHelper) => {
      const tabBar = _this.componentsById[id]
      tabBar.listen('MDCTabBar:activated', evt => {
        dotnetHelper.invokeMethodAsync('OnTabActivated', evt.detail)
      })
    }
  }

  return MDCTabBarComponent
}())

window.MDCTabBarComponent = new MDCTabBarComponent()
