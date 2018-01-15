export class PrintFns {

  private static printFn: () => void;
  private static subscribed: boolean;

  /** Provide function to call before print */
  static setPrintFn(callback: () => void) {
    PrintFns.printFn = callback;
    PrintFns.preparePrinting();
  }

  /** Function called before printing */
  private static beforePrint() {
    if (PrintFns.printFn) {
      PrintFns.printFn();
    }
  }

  /** Set up listeners to handle print event (ctrl-P printing) */
  private static preparePrinting() {
    if (PrintFns.subscribed) {
      return;
    }

    if (window.matchMedia) {
        const mediaQueryList = window.matchMedia('print');
        mediaQueryList.addListener(function(mql) {
            if (mql.matches) {
                PrintFns.beforePrint();
            }
        });
    }

    window.onbeforeprint = PrintFns.beforePrint;
    PrintFns.subscribed = true;
  }

  static print() {
    try {
      window.print()
    } finally {
      const printEle = document.getElementById('printSection');
      if (printEle) {
        printEle.parentElement.removeChild(printEle);
      }
    }
  }

  static printElement(ele: HTMLElement) {

    const printSelectEle = this.getPrintSection();
    const eleClone = ele.cloneNode(true);
    printSelectEle.appendChild(eleClone);
  }

  /** Find or create or find an element with an id of 'printSection'
   * printSection is styled in the css to print properly.
   */
  private static getPrintSection() {
    let printSelectEle = document.getElementById('printSection');
    if (printSelectEle) {
      printSelectEle.innerHTML = '';
    } else {
      printSelectEle = document.createElement('div');
      printSelectEle.id = 'printSection';
      document.body.appendChild(printSelectEle);
    }
    return printSelectEle;
  }

}
