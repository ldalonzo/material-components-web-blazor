import { MDCCircularProgress } from '@material/circular-progress'

var MDCCircularProgressComponent = /** @class */ (function () {
  function MDCCircularProgressComponent () {
    var _this = this
    this.componentsById = {}

    this.attachTo = (domElement, id) => {
      _this.componentsById[id] = MDCCircularProgress.attachTo(domElement)
    }

    this.open = (id) => {
      _this.componentsById[id].open()
    }

    this.setDeterminate = (id, value) => {
      _this.componentsById[id].determinate = value
    }

    this.close = (id) => {
      _this.componentsById[id].close()
    }
  }

  return MDCCircularProgressComponent
}())

window.MDCCircularProgressComponent = new MDCCircularProgressComponent()
