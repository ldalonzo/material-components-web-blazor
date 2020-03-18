import { MDCDrawer } from '@material/drawer'

var MDCDrawerComponent = /** @class */ (function () {
  function MDCDrawerComponent () {
    var _this = this
    this.componentsById = {}

    this.attachTo = (domElement) => {
      _this.componentsById[domElement.id] = MDCDrawer.attachTo(domElement)
    }

    this.toggleOpen = (domElement) => {
      const drawer = _this.componentsById[domElement.id]
      drawer.open = !drawer.open
    }
  }

  return MDCDrawerComponent
}())

window.MDCDrawerComponent = new MDCDrawerComponent()
