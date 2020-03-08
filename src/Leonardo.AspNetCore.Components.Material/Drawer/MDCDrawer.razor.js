import { MDCDrawer } from '@material/drawer'

var MDCDrawerComponent = /** @class */ (function () {
  function MDCDrawerComponent () {
    var _this = this
    this.drawers = {}

    this.attachTo = (domElement) => {
      _this.drawers[domElement] = MDCDrawer.attachTo(domElement)

      return true
    }

    this.toggleOpen = (domElement) => {
      const drawer = this.drawers[domElement]
      drawer.open = !drawer.open

      return drawer.open
    }
  }

  return MDCDrawerComponent
}())

window.MDCDrawerComponent = new MDCDrawerComponent()
