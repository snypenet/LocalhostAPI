using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalhostAPI.Integration;

namespace LocalhostAPI.Library
{
    public class GetPrinterNames : IActionWithoutParameters
    {
        public object Execute()
        {
            var printerNames = new List<string>();
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                printerNames.Add(printer);
            }

            return printerNames;
        }
    }
}
