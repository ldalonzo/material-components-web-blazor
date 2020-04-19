import { MDCDrawer } from '@material/drawer'

var MDCDrawerComponent = /** @class */ (function () {
  function MDCDrawerComponent () {
    var _this = this
    this.componentsById = {}

    this.attachTo = (domElement, id) => {
      _this.componentsById[id] = MDCDrawer.attachTo(domElement)
    }

    this.toggleOpen = (id) => {
      const drawer = _this.componentsById[id]
      drawer.open = !drawer.open
    }
  }

  return MDCDrawerComponent
}())

window.MDCDrawerComponent = new MDCDrawerComponent()
