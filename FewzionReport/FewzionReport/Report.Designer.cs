using FewzionReport.Models;

namespace FewzionReport
{
    partial class Report
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
            this.lblPlanName = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.GroupHeader = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();

            this.lblShift = new DevExpress.XtraReports.UI.XRLabel();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.GroupFooter = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrPanel3 = new DevExpress.XtraReports.UI.XRPanel();
            this.lblSummary = new DevExpress.XtraReports.UI.XRLabel();
            this.ProcessPlanID = new DevExpress.XtraReports.Parameters.Parameter();
            this.SummaryView = new DevExpress.XtraReports.Parameters.Parameter();
            
            this.StartDate = new DevExpress.XtraReports.Parameters.Parameter();
            this.Hidden = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel2,
            this.lblPlanName});
            this.Detail.HeightF = 25F;
            this.Detail.KeepTogether = false;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPanel2
            // 
            this.xrPanel2.LocationFloat = new DevExpress.Utils.PointFloat(50F, 0F);
            this.xrPanel2.Name = "xrPanel2";
            this.xrPanel2.SizeF = new System.Drawing.SizeF(1542F, 25F);
            // 
            // lblPlanName
            // 
            this.lblPlanName.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
            this.lblPlanName.CanGrow = false;
            this.lblPlanName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Id")});
            this.lblPlanName.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlanName.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblPlanName.Name = "lblPlanName";
            this.lblPlanName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 6, 0, 100F);
            this.lblPlanName.SizeF = new System.Drawing.SizeF(50F, 25F);
            this.lblPlanName.StylePriority.UseFont = false;
            this.lblPlanName.StylePriority.UsePadding = false;
            this.lblPlanName.Text = "lblPlanName";
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 31F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 31F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // GroupHeader
            // 
            this.GroupHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1,

            this.lblShift});
            this.GroupHeader.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Date", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader.HeightF = 50F;
            this.GroupHeader.Name = "GroupHeader";
            this.GroupHeader.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
            // 
            // xrPanel1
            // 
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(50F, 25F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.SizeF = new System.Drawing.SizeF(1542F, 25F);

            // 
            // lblShift
            // 
            this.lblShift.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.lblShift.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblShift.CanGrow = false;
            this.lblShift.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShift.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.lblShift.Name = "lblShift";
            this.lblShift.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 4, 0, 100F);
            this.lblShift.SizeF = new System.Drawing.SizeF(50F, 25F);
            this.lblShift.StylePriority.UseBackColor = false;
            this.lblShift.StylePriority.UseFont = false;
            this.lblShift.StylePriority.UsePadding = false;
            this.lblShift.Text = "Shift";
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
            this.PageFooter.HeightF = 25F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo1.Format = "Page {0} of {1}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(1490F, 0F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(100F, 25F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Schedule);
            // 
            // GroupFooter
            // 
            this.GroupFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel3,
            this.lblSummary});
            this.GroupFooter.HeightF = 47.99999F;
            this.GroupFooter.Name = "GroupFooter";
            // 
            // xrPanel3
            // 
            this.xrPanel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 22.99999F);
            this.xrPanel3.Name = "xrPanel3";
            this.xrPanel3.SizeF = new System.Drawing.SizeF(1592F, 25F);
            // 
            // lblSummary
            // 
            this.lblSummary.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblSummary.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblSummary.SizeF = new System.Drawing.SizeF(103.125F, 23F);
            this.lblSummary.StylePriority.UseFont = false;
            this.lblSummary.StylePriority.UseTextAlignment = false;
            this.lblSummary.Text = "KPI Summary";
            this.lblSummary.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
            this.lblSummary.Visible = false;
            // 
            // ProcessPlanID
            // 
            this.ProcessPlanID.Name = "ProcessPlanID";
            this.SummaryView.Name = "SummaryView";
            // 
            // StartDate
            // 
            this.StartDate.Name = "StartDate";
            this.StartDate.Type = typeof(System.DateTime);
            // 
            // Hidden
            // 
            this.Hidden.Name = "Hidden";
            // 
            // Report
            // 
            this.PageHeader.Name = "PageHeader";

            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader,
            this.PageFooter,
            this.GroupFooter,
            this.PageHeader});
            this.DataSource = this.bindingSource1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(31, 31, 31, 31);
            this.PageHeight = 1169;
            this.PageWidth = 1654;
            this.PaperKind = System.Drawing.Printing.PaperKind.A3;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.ProcessPlanID,
            this.SummaryView,
            this.StartDate,
            this.Hidden});
            this.ReportPrintOptions.PrintOnEmptyDataSource = false;
            this.Version = "14.2";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion


        #region Report Layout
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraReports.UI.XRLabel lblPlanName;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        private DevExpress.XtraReports.UI.XRPanel xrPanel2;
       
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter;
        private DevExpress.XtraReports.UI.XRPanel xrPanel3;
        private DevExpress.XtraReports.UI.XRLabel lblSummary;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        #endregion
        private DevExpress.XtraReports.Parameters.Parameter ProcessPlanID;
        private DevExpress.XtraReports.Parameters.Parameter SummaryView;
        private DevExpress.XtraReports.Parameters.Parameter StartDate;
        private DevExpress.XtraReports.UI.XRLabel lblShift;
        private DevExpress.XtraReports.Parameters.Parameter Hidden;

    }
}
