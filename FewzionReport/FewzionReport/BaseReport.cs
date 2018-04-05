using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FewzionReport
{
    public class BaseReport : XtraReport
    {
        private string _safetySlogan;

        protected static readonly Font StdFont13Bold = new Font("Calibri", 13f, FontStyle.Bold, GraphicsUnit.Point);
        protected static readonly Font StdFont9 = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point);

        public BaseReport()
        {
            SetSafetySlogan();
        }

        private void SetSafetySlogan()
        {
            var safetySlogans = new List<string>();
            if (safetySlogans.Count <= 0) return;
            var r = new Random();
            _safetySlogan = safetySlogans[r.Next(safetySlogans.Count)];
        }

        protected string GetSafetySlogan()
        {
            return _safetySlogan;
        }

        protected XRTable CreatePageHeader(XRTable table, bool includeSafetySlogan = true)
        {
            table.WidthF = PageWidth - Margins.Left - Margins.Right;
            table.Rows.Clear();
            if (includeSafetySlogan && !string.IsNullOrEmpty(GetSafetySlogan()))
            {
                InsertSafetySlogan(table);
            }
            return table;
        }

        protected XRTable CreatePageFooter(XRTable table, bool includePageNo = false, bool includeSafetySlogan = true)
        {
            table.WidthF = PageWidth - Margins.Left - Margins.Right;
            table.Rows.Clear();
            if (includePageNo)
            {
                InsertPageNo(table);
            }
            if (includeSafetySlogan && !string.IsNullOrEmpty(GetSafetySlogan()))
            {
                InsertSafetySlogan(table);
            }
            return table;
        }

        private void InsertSafetySlogan(XRTable table)
        {
            var row = new XRTableRow();
            row.Cells.Add(new XRTableCell { BackColor = ColorTranslator.FromHtml("#002776"), ForeColor = Color.White, Padding = new PaddingInfo(0, 0, 5, 5), Borders = BorderSide.None, Text = GetSafetySlogan(), TextAlignment = TextAlignment.MiddleCenter, Font = StdFont13Bold });
            table.Rows.Add(row);
        }

        private static void InsertPageNo(XRTable table)
        {
            var row = new XRTableRow();
            var cell = new XRTableCell();
            cell.Controls.Add(new XRPageInfo { Font = StdFont9, Format = "Page {0} of {1}", WidthF = table.WidthF, TextAlignment = TextAlignment.MiddleRight });
            row.Cells.Add(cell);
            table.Rows.Add(row);
        }
    }
}

