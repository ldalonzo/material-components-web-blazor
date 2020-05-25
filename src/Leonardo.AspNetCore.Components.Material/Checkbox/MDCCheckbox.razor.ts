import { MDCCheckbox } from '@material/checkbox'

class MDCCheckboxComponent {
  private readonly componentsById: Map<string, MDCCheckbox> = new Map();

  public attachTo(root: Element, id: string): void {
    const checkbox = MDCCheckbox.attachTo(root)
    this.componentsById.set(id, checkbox)
  }

  public setChecked(id: string, value: boolean): void {
    const checkbox = this.componentsById.get(id)
    if (checkbox) {
      checkbox.checked = value
    }
  }

  public setIndeterminate(id: string, value: boolean): void {
    const checkbox = this.componentsById.get(id)
    if (checkbox) {
      checkbox.indeterminate = value
    }
  }

  public dispose(id: string) : boolean {
    return this.componentsById.delete(id)
  }
}

declare global {
  interface Window { MDCCheckboxComponent: MDCCheckboxComponent }
}

window.MDCCheckboxComponent = new MDCCheckboxComponent()
