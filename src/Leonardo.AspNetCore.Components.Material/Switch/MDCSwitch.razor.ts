import {MDCSwitch} from '@material/switch'

export class MDCSwitchComponent {
  public attachTo(root: Element, id: string): void {
    const switchControl = new MDCSwitch(root)
  }
}

declare global {
  interface Window { MDCSwitchComponent: MDCSwitchComponent }
}

window.MDCSwitchComponent = new MDCSwitchComponent()
