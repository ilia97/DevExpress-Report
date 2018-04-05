using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraReports.UI;

namespace FewzionReport
{
    class Program
    {
        static void Main(string[] args)
        {
            var stream = new MemoryStream();
            
            var reportModel = new Report();
            
            using (var report = reportModel)
            {
                // Export to pdf
                report.ExportOptions.Pdf.Compressed = true;
                report.ExportOptions.Pdf.ConvertImagesToJpeg = false;
                report.ExportOptions.Pdf.ShowPrintDialogOnOpen = true;

                report.ExportToPdf(stream);
            }
            
            using (var fileStream = File.Create("Reports\\Report.pdf"))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
            }
        }
    }
}
