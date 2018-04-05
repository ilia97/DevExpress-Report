using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Text;
using System.Web;
using FewzionReport.Models;
using FewzionReport.Utils;
using Humanizer;
using System.Text;

namespace FewzionReport
{
    public partial class Report : BaseReport
    {            
        private IList<Integration> _integrations = new List<Integration>();
        
        private static readonly Font _stdfont10 = new Font("Calibri", 10f, FontStyle.Regular, GraphicsUnit.Point);
        private static readonly Font _stdfont14 = new Font("Calibri", 13f, FontStyle.Bold, GraphicsUnit.Point);
        
        public Report()
        {
            InitializeComponent();

            this.DataSourceDemanded += new System.EventHandler<EventArgs>(this.Report_DataSourceDemanded);
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Report_BeforePrint);
            this.GroupHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupHeader_BeforePrint);
            this.Detail.BeforePrint += this.Detail_BeforePrint;
            this.GroupFooter.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupFooter_BeforePrint);
            this.PageHeader.BeforePrint += PageHeader_BeforePrint;
        }

        void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var schedule = (Schedule_Report)GetCurrentRow();
            var headfontleft = _stdfont14;
            var headfontright = _stdfont10;
            
            var datestr = schedule.Process.Name + " - " + schedule.WeekName;
            var leftdatestr = DateTime.Now.ToString("dd MMMM yyyy hh:mm tt");
            var table = new XRTable();
            table.BeginInit();
            CreatePageHeader(table, includeSafetySlogan: false);
            var headrow = new XRTableRow();
            headrow.Cells.Add(new XRTableCell { Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 4), WidthF = (xrPanel1.WidthF + 40f) / 3, Borders = BorderSide.None, Text = datestr, TextAlignment = TextAlignment.MiddleLeft, Font = headfontleft });
            headrow.Cells.Add(new XRTableCell { Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 4), WidthF = (xrPanel1.WidthF + 40f) / 3, Borders = BorderSide.None, Text = "", TextAlignment = TextAlignment.MiddleCenter, Font = headfontleft });
            headrow.Cells.Add(new XRTableCell { Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 4), WidthF = (xrPanel1.WidthF + 40f) / 3, Borders = BorderSide.None, Text = leftdatestr, TextAlignment = TextAlignment.MiddleRight, Font = headfontright, ForeColor = Color.Gray });
            headrow.BackColor = Color.White;
            table.Rows.Add(headrow);
            table.EndInit();
            PageHeader.Controls.Clear();
            PageHeader.Controls.Add(table);

        }

        void Report_DataSourceDemanded(object sender, System.EventArgs e)
        {
            var processLocations = new List<ProcessLocation>();

            var process = new Process()
            {
                CanAcceptTasks = true,
                Active = true,
                ChockPosTable = null,
                ChockPosTagArray = null,
                CrewLeaderOccupations = new List<string>() { "occupations-51" },
                DailyActualsReportKPISummaryByDepartment = null,
                DailyActualsReportKPISummaryByProcess = false,
                DailyActualsReportKPISummaryShortCodes = new List<string>(),
                DefaultKPIs = new List<DefaultKPI>(),
                Deleted = false,
                Department = "departments-42",
                DisplayOrder = 3000,
                EnableShiftPlanAcknowledgement = false,
                EnableShiftPlanApproval = false,
                EnableShiftPlanNotesonActuals = false,
                EnableShortIntervalControl = false,
                FifthKPIToTrack = "Chal",
                FifthKPIToTrackPeriod = "Current Week",
                FirstKPIToTrack = "M-B",
                FirstKPIToTrackPeriod = "Current Week",
                FourthKPIToTrack = "OT-A",
                FourthKPIToTrackPeriod = "Current Week",
                Locations = processLocations,
                ManagesEquipment = true,
                ManagesPeople = true,
                Name = "Development 8N",
                PlannedTasks = new List<PlannedTaskOld>(),
                RuntimeTable = null,
                RuntimeTagArray = new string[] { "Rum Time Tags" },
                SecondKPIToTrack = "M-A",
                SecondKPIToTrackPeriod = "Current Week",
                ShearsTable = null,
                ShearsTagArray = new string[0],
                ShiftPlanChecklistQuestions = new List<ProcessChecklistQuestion>(),
                ShiftPlanReportAdditionalTasksLines = 1,
                ShiftPlanReportTopDelaysAndOrStoppagesLines = 0,
                ShortCode = "DEV8N3",
                ShowOn2448HourSchedule = true,
                ShowOnEquipmentSchedule = true,
                ShowOnManningReport = true,
                ShowOnPlanningBoard = true,
                ShowOnShiftSchedule = true,
                ShowOnSmartboard = true,
                ShowOnWeeklySchedule = true,
                ShowShiftPlanReport5WHY = true,
                ShowShiftPlanReportChecklist = true,
                SixthKPIToTrack = "Inc",
                SixthKPIToTrackPeriod = "Month To Date",
                StandardTasks = new List<StandardTaskOld>(),
                TTFCTable = null,
                TTFCTagArray = new string[] { "TTFC Tags" },
                TaskChecklistQuestions = new List<ProcessChecklistQuestion>(),
                ThirdKPIToTrack = "OT-B",
                ThirdKPIToTrackPeriod = "Current Week",
                TonnesMultiplier = (decimal)25.35,
                Type = "Production",
                UCTable = null,
                UCTagArray = new string[0],
                WeeklyOperatingHoursTarget = 85
            };

            var scheduleReport = new Schedule_Report()
            {
                Date = new DateTime(2018, 3, 29),
                DisplayOrder = 3000,
                Id = "Maintenance",
                Name = "DEV8N3",
                Process = process,
                ShiftPlans = new List<ShiftPlan_Report>()
            };
            
            for (var j = 0; j < 7; j++)
            {
                var kpiTemplate = new AppliedKPI()
                {
                    Date = new DateTime(2018, 3, 29),
                    DefaultTarget = 420,
                    DisplayOrder = 10000,
                    Id = "AppliedKPIs-53918",
                    Process = "processes-51",
                    Shift = "Maintenance",
                    ShortCode = "OT",
                    Target = 420,
                    TargetString = "420"
                };

                var personTemplate = new ShiftPlanPerson_Report()
                {
                    Available = true,
                    OccupationShortCode = "M"
                };

                var shiftPlanReportTemplate = new ShiftPlan_Report()
                {
                    Acknowledgements = new List<Acknowledgement_Report>(),
                    ActualsCrewLeader = null,
                    ActualsCrewLeaderName = null,
                    Checklist = new List<ShiftPlanChecklistQuestion>(),
                    Crew = "White",
                    CrewLeader = null,
                    CrewLeaderLabel = "Deputy",
                    Date = new DateTime(2018, 3, 29),
                    DefaultCrewLeader = "01 DMS, Kevin Gardner",
                    EffectiveShiftLength = 12,
                    EnableShiftPlanAcknowledgement = false,
                    EnableShiftPlanAcknowledgementBySchedule = false,
                    EnableShiftPlanAcknowledgementByShiftPlan = false,
                    Equipment = new List<ShiftPlanEquipment>(),
                    Id = "shiftplans-processes-51-2018-03-29-Maintenance",
                    KPIs = new List<AppliedKPI>(),
                    People = new List<ShiftPlanPerson_Report>(),
                    Process = "process-51",
                    ProcessName = "Development 8N",
                    ProcessType = "Production",
                    SafetyNotes = null,
                    SafetyReport = null,
                    ShadowTasksWarning = "",
                    Shift = "Maintenance",
                    ShiftLength = 12,
                    ShiftNotes = null,
                    ShiftReport = null,
                    StartDateTime = new DateTime(2018, 3, 29),
                    SuggestedWorkAndNotifications = null,
                    Tasks = new List<ShiftPlanTask_Report>(),
                    UnscheduledWorkCompleted = null
                };

                for (var i = 0; i < 92; i++)
                {
                    var description = new StringBuilder("Test ");

                    for (int k = 0; k < j * 3; k++)
                    {
                        description.Append("Test ");
                    }

                    var taskTemplate = new ShiftPlanTask_Report()
                    {
                        Description = description.ToString(),
                        Duration = 2,
                        Priority = "20P",
                        Process = "DEV8N3",
                        StartTime = new DateTime(2018, 3, 29, 5, 0, 0),
                    };

                    shiftPlanReportTemplate.Tasks.Add(taskTemplate);
                }

                for (var i = 0; i < 35; i++)
                {
                    shiftPlanReportTemplate.People.Add(personTemplate);
                }

                for (var i = 0; i < 9; i++)
                {
                    shiftPlanReportTemplate.KPIs.Add(kpiTemplate);
                }

                scheduleReport.ShiftPlans.Add(shiftPlanReportTemplate);
            }

            var sorted = new List<Schedule_Report>()
            {
                scheduleReport,
                scheduleReport,
                scheduleReport
            };

            this.DataSource = sorted;
        }

        void Report_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _integrations = new List<Integration>();
        }

        void GroupHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var schedule = (Schedule_Report)GetCurrentRow();

            XRTable table = new XRTable();
            table.SizeF = new System.Drawing.SizeF(xrPanel1.WidthF, 25F);
            table.BeginInit();
            XRTableRow row = new XRTableRow();
            foreach (var shift in schedule.ShiftPlans)
            {
                var date = shift.Date;
                XRTableCell cell = new XRTableCell
                {
                    WidthF = xrPanel1.WidthF / schedule.ShiftPlans.Count,
                    Text = String.Format("{0} {1}{2} {3} {4}", date.ToString("ddd"), date.Day, "", date.ToString("MMM"), date.Year),
                    TextAlignment = TextAlignment.MiddleCenter,
                    Font = new Font("Arial", 8, FontStyle.Regular),
                    BackColor = SystemColors.ControlLight
                };
                row.Cells.Add(cell);
            }
            table.Rows.Add(row);
            table.EndInit();

            xrPanel1.Controls.Clear();
            xrPanel1.Controls.Add(table);

            lblShift.HeightF = xrPanel1.HeightF;
            lblShift.BackColor = SystemColors.ControlLight;
        }

        void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var schedule = (Schedule_Report)GetCurrentRow();

            // Each row is a SHIFT by name (i.e. Window, Day, Night)
            // Each cell is the SHIFTPLAN which is a DATE/SHIFT combination.
            // Each Cell contains 4 tables for KPI, People, Equipment and Tasks.

            Color color = ColorTranslator.FromHtml(ShiftRules.GetShiftColour(schedule.Id));
            double[] hsv = ColorUtil.ToHsv(color);
            lblPlanName.BackColor = ColorUtil.FromHsv(hsv[0], Math.Min(hsv[1] + 0.5, 1), hsv[2]);
            lblPlanName.ForeColor = ColorUtil.ContrastColor(lblPlanName.BackColor);
            XRTable table = new XRTable() { SizeF = new System.Drawing.SizeF(xrPanel2.WidthF, 25F) };
            table.BeginInit();
            XRTableRow row = new XRTableRow();
            row.KeepTogether = false;
            
            foreach (var shift in schedule.ShiftPlans)
            {
                XRTableCell cell = new XRTableCell { WidthF = xrPanel2.WidthF / schedule.ShiftPlans.Count, BackColor = color};
                row.Cells.Add(cell);

                CreateShiftPlanTables(cell, shift, ShiftPlan_Report.GetTrackedResources(shift).ToList());
            }
            table.Rows.Add(row);
            table.EndInit();

            xrPanel2.KeepTogether = false;
            xrPanel2.Controls.Clear();
            xrPanel2.Controls.Add(table);
        }

        void GroupFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            List<ShiftPlan> _shiftPlans = new List<ShiftPlan>();
            _shiftPlans = new List<ShiftPlan>();

            IList<AppliedKPI> kpiTotals = new List<AppliedKPI>();

            if (kpiTotals.Count > 0)
            {
                lblSummary.Visible = true;
                lblSummary.Text = "KPI Summary";

                xrPanel3.Visible = true;
                CreateKPISummaryTable(xrPanel3, kpiTotals);
            }
        }

        private void CreateShiftPlanTables(XRControl parent, ShiftPlan_Report shiftPlan, IList<TrackedResource> trackedResources)
        {
            const float margin = 4F;
            const float tableHeight = 25F;
            const float offset = tableHeight + margin;

            float top = margin;
            float tableWidth = parent.WidthF - (margin * 2);

            bool isFutureDate = shiftPlan.Date.Date > DateTime.Now.Date;
            
            parent.Controls.Clear();

            XRTable table = null;

            List<AppliedKPI> kpis = shiftPlan.GetVisibleKPIs();
            if (kpis.Count > 0)
            {
                table = CreateKPIsTable(kpis, tableWidth, 5M, isFutureDate);
                table.TopF = top;
                table.LeftF = margin;
                top += offset;
                parent.Controls.Add(table);
            }

            var people = trackedResources.Where(x => x.Category == "P" && x.RequiredOrAvailable).ToList();
            if (people.Count > 0)
            {
                table = CreateTrackedResourceTable(people, "P", tableWidth);
                table.TopF = top;
                table.LeftF = margin;
                top += offset;
                parent.Controls.Add(table);
            }

            var equipment = trackedResources.Where(x => x.Category == "E" && x.RequiredOrAvailable).ToList();
            if (equipment.Count > 0)
            {
                table = CreateTrackedResourceTable(equipment, "E", tableWidth);
                table.TopF = top;
                table.LeftF = margin;
                top += offset;
                parent.Controls.Add(table);
            }
            bool summaryView = false;
            var res = Boolean.TryParse(Parameters["SummaryView"].Value.ToString(), out summaryView);
            // task rows...
            if (shiftPlan.Tasks.Count > 0)
            {
                foreach (var task in shiftPlan.Tasks.Where(task => !summaryView || task.Important))
                {
                    XRPanel panel = new XRPanel { WidthF = tableWidth, HeightF = 25F, TopF = top, LeftF = margin, CanGrow = true };
                    panel.Controls.Add(CreateTasksTable(task, tableWidth));
                    parent.Controls.Add(panel);
                    top += offset;
                }
            }
            if (summaryView)
            {

                var richText = new XRRichText { WidthF = parent.WidthF, HeightF = 25F, TopF = top + 4F, LeftF = 4F, CanGrow = true, Borders = BorderSide.None };
                parent.Controls.Add(richText);
                var html = String.Join("", shiftPlan.Tasks
                    .Where(task => !task.Important)
                    .GroupBy(task => (!String.IsNullOrEmpty(task.TypeShortCode) ? task.TypeShortCode : "Oth"))
                    .OrderBy(g => g.First().TypeOrder)
                    .Select(g => "<span style=\"color:#0c5c96;font-weight:bold;\">{0} {1}</span>&nbsp;&nbsp;&nbsp;".FormatWith(g.Count(), g.Key)));
                richText.Html = "<div style=\"font-family: Arial, Helvetica, sans-serif; font-size: 9pt;margin-left: 4px;\">{0}</div>".FormatWith(html);
            }
        }

        private void CreateKPISummaryTable(XRControl parent, IList<AppliedKPI> _kpiTotals)
        {
            const float margin = 4F;
            const float tableHeight = 25F;
            const float offset = tableHeight + margin;

            float top = margin;
            float tableWidth = parent.WidthF - (margin * 2);

            parent.Controls.Clear();

            XRTable table = null;

            //TODO: figure out shoud we prcess shifts in teh future for the KPI summary
            bool isFutureDate = false;

            if (_kpiTotals.Count > 0)
            {
                table = CreateKPIsTable(_kpiTotals, tableWidth, 20M, isFutureDate);
                table.TopF = top;
                table.LeftF = margin;
                top += offset;
                parent.Controls.Add(table);
            }
        }
        private static string NoLeadingZero(string instr)
        {
            var ret = instr;
            if (instr.Length == 5)
            {
                if (instr.Contains(":"))
                {
                    if (instr.Substring(0, 1) == "0")
                    {
                        return instr.Substring(1);
                    }
                }
            }
            return instr;
        }


        private XRTable CreateKPIsTable(IList<AppliedKPI> kpis, float tableWidth, decimal cellsPerRow, bool isFutureDate)
        {
            XRTable table = new XRTable();
            table.BeginInit();
            table.BorderWidth = 1;
            table.WidthF = tableWidth;
            table.Borders = BorderSide.All;
            table.BorderColor = Color.FromArgb(195, 197, 197);
            table.TextAlignment = TextAlignment.MiddleCenter;
            table.Font = new Font("Arial", 8, FontStyle.Regular);

            int skip = 0;
            int take = (int)Math.Ceiling(kpis.Count / Math.Ceiling(kpis.Count / cellsPerRow));
            var k = kpis.Take(take);
            while (k.Count() > 0)
            {
                skip += take;
                table.Rows.Add(CreateKPIsRow(k.ToArray(), "KPI", tableWidth, take, isFutureDate));
                table.Rows.Add(CreateKPIsRow(k.ToArray(), "Target", tableWidth, take, isFutureDate));
                table.Rows.Add(CreateKPIsRow(k.ToArray(), "Actual", tableWidth, take, isFutureDate));
                k = kpis.Skip(skip).Take(take);
            }

            table.EndInit();

            return table;
        }

        private XRTableRow CreateKPIsRow(IList<AppliedKPI> kpis, string firstColName, float tableWidth, int columns, bool isFutureDate)
        {
            float actualValue;
            float firstColWidth = 45F;
            XRTableRow row = new XRTableRow();

            XRTableCell firstCol = new XRTableCell { Text = firstColName, WidthF = firstColWidth, BackColor = Color.FromArgb(221, 238, 255) };
            if (firstColName.Contains("Target") || firstColName.Contains("Actual"))
                firstCol.BackColor = Color.FromArgb(190, 222, 254);
            row.Cells.Add(firstCol);

            foreach (var k in kpis)
            {
                if (firstColName.Contains("KPI"))
                {
                    row.Cells.Add(new XRTableCell { Text = k.ShortCode, WidthF = (tableWidth - firstColWidth) / (float)columns, BackColor = Color.FromArgb(221, 238, 255) });
                }
                else if (firstColName.Contains("Target"))
                {
                    row.Cells.Add(new XRTableCell { Text = NoLeadingZero(k.TargetString), WidthF = (tableWidth - firstColWidth) / (float)columns, BackColor = Color.FromArgb(235, 243, 251) });
                }
                else if (firstColName.Contains("Actual"))
                {
                    float.TryParse(k.ActualString, out actualValue);
                    var actualString = actualValue == 0 && isFutureDate ? string.Empty : k.ActualString;

                    Color color = String.IsNullOrEmpty(actualString) ? Color.FromArgb(235, 243, 251) : ColorTranslator.FromHtml(k.GetColor());
                    row.Cells.Add(new XRTableCell { Text = actualString, WidthF = (tableWidth - firstColWidth) / (float)columns, BackColor = color });
                }
            }
            int count = columns - kpis.Count;
            for (int i = 0; i < count; ++i)
            {
                if (firstColName.Contains("KPI"))
                {
                    row.Cells.Add(new XRTableCell { WidthF = (tableWidth - firstColWidth) / (float)columns, BackColor = Color.FromArgb(221, 238, 255) });
                }
                else
                {
                    row.Cells.Add(new XRTableCell { WidthF = (tableWidth - firstColWidth) / (float)columns, BackColor = Color.FromArgb(235, 243, 251) });
                }
            }
            return row;
        }

        private XRTable CreateTrackedResourceTable(IList<TrackedResource> resources, string category, float width)
        {
            string title;
            Color color1, color2, color3;
            if (category == "P")
            {
                title = "Pers";
                color1 = Color.FromArgb(245, 204, 252);
                color2 = Color.FromArgb(243, 174, 255);
                color3 = Color.FromArgb(255, 236, 254);
            }
            else // E
            {
                title = "Equip";
                color1 = Color.FromArgb(249, 222, 197);
                color2 = Color.FromArgb(235, 201, 170);
                color3 = Color.FromArgb(254, 242, 230);
            }

            XRTable table = new XRTable();
            table.BeginInit();
            table.BorderWidth = 1;
            table.WidthF = width;
            table.TopF = 4F;
            table.LeftF = 4F;
            table.Borders = BorderSide.All;
            table.BorderColor = Color.FromArgb(195, 197, 197);
            table.TextAlignment = TextAlignment.MiddleCenter;
            table.Font = new Font("Arial", 8, FontStyle.Regular);

            float cellWidth = (width - 45F) / resources.Count;
            Color white = color3;
            Color red = Color.FromArgb(255, 93, 71);

            XRTableRow row = new XRTableRow();
            row.BackColor = color1;
            row.Cells.AddRange(new XRTableCell[]
            {
                new XRTableCell { Text = title, WidthF = 45F }
            });

            foreach (var p in resources)
            {
                row.Cells.Add(new XRTableCell { Text = p.Type, WidthF = cellWidth });
            }

            table.Rows.Add(row);

            row = new XRTableRow();
            row.Cells.AddRange(new XRTableCell[]
            {
                new XRTableCell { Text = "Rqd", WidthF = 45F, BackColor = color2 }
            });

            foreach (var p in resources)
            {
                row.Cells.Add(new XRTableCell { Text = p.Required.ToString("0.##"), WidthF = cellWidth, BackColor = (p.Required > p.Available) ? red : white });
            }

            table.Rows.Add(row);

            row = new XRTableRow();
            row.BackColor = white;
            row.Cells.AddRange(new XRTableCell[]
            {
                new XRTableCell { Text = "Avail", WidthF = 45F, BackColor = color2 }
            });

            foreach (var p in resources)
            {
                row.Cells.Add(new XRTableCell { Text = p.Available.ToString(), WidthF = cellWidth });
            }

            table.Rows.Add(row);

            table.EndInit();

            return table;
        }

        private static readonly Color _LightBlue = Color.FromArgb(240, 255, 255);

        private XRTable CreateTasksTable(ShiftPlanTask_Report task, float tableWidth)
        {
            XRTable table = new XRTable();
            table.BeginInit();
            table.BorderWidth = 1;
            table.WidthF = tableWidth;
            table.Borders = BorderSide.All;
            table.BorderColor = Color.FromArgb(195, 197, 197);
            table.TextAlignment = TextAlignment.MiddleLeft;
            table.Font = new Font("Arial", 8, FontStyle.Regular);
            table.KeepTogether = true;
            
            XRTableRow row;
            XRTableCell cell;

            string workOrder = " [" + task.WorkOrder + "]";
            var locationsstr = String.Join(",", task.Locations.Select(x => x.Location).ToList());
            string description = !String.IsNullOrEmpty(task.WorkOrder) ? task.Description + workOrder : task.Description;
            description = description + " " + locationsstr;

            string compperccolor = "#ebf3fb";
            string completionPercentagestr = task.CompletionPercentage == null ? "" : task.CompletionPercentage.ToString();
            var completionPercentage = task.CompletionPercentage == null ? null : task.CompletionPercentage;
            if (completionPercentage != null)
            {
                if (completionPercentage >= 100)
                {// green
                    compperccolor = "#7eff70";
                }
                if (completionPercentage < 100 && completionPercentage >= 80)
                {// yelow
                    compperccolor = "#ffe561";
                }
                if (completionPercentage < 80 && completionPercentage >= 0)
                {// red
                    compperccolor = "#ff5d47";
                }
            }
            var compcolor = completionPercentage != null ? ColorTranslator.FromHtml(compperccolor) : _LightBlue;

            if (String.IsNullOrEmpty(task.TypeColor))
            {
                row = new XRTableRow
                {
                    HeightF = 25F,
                    Padding = new PaddingInfo(5, 5, 5, 5),
                    KeepTogether = true
                };

                row.Cells.Add(new XRTableCell
                {
                    Text = task.Priority,
                    WidthF = 45F,
                    TextAlignment = TextAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(238, 235, 235),
                    Font = new Font("Arial", 8, FontStyle.Bold),
                    KeepTogether = true
                });

                cell = new XRTableCell()
                {
                    KeepTogether = true
                };
                var icons = new List<string>();
                
                // Tags
                icons.AddRange(TagUtil.GetTagsForPrinting(task.Tags, new List<Tag>()));

                var descHtml = "<div style=\"padding: 0; margin: 0; font-size: 10pt; font-family: Calibri; font-weight: bold\">" + description + "&nbsp;" + string.Join("&nbsp;", icons) + "</div>";
                var descRt = new XRRichText
                {
                    Padding = new PaddingInfo(2, 2, 2, 2),
                    TopF = 2,
                    LeftF = 2,
                    WidthF = tableWidth - 135F - 4,
                    HeightF = 21,
                    Html = descHtml,
                    Borders = BorderSide.None,
                    CanGrow = true,
                    AnchorVertical = VerticalAnchorStyles.None,
                    KeepTogether = true
                };

                cell.Controls.Add(descRt);
                cell.WidthF = tableWidth - 135f;
                row.Cells.Add(cell);
                cell.BackColor = Color.FromArgb(248, 248, 248);
                //row.Cells.Add(new XRTableCell { Text = description, WidthF = tableWidth - 45F, BackColor = Color.FromArgb(248, 248, 248) });
                row.Cells.Add(new XRTableCell
                {
                    Text = task.TargetAndUnitShortCode,
                    WidthF = 45F,
                    TextAlignment = TextAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(238, 235, 235),
                    Font = new Font("Arial", 8, FontStyle.Bold),
                    KeepTogether = true
                });
                row.Cells.Add(new XRTableCell
                {
                    Text = completionPercentagestr,
                    BackColor = compcolor,
                    WidthF = 45F,
                    TextAlignment = TextAlignment.MiddleCenter,
                    Font = new Font("Arial", 8, FontStyle.Bold),
                    KeepTogether = true
                });

                table.Rows.Add(row);
            }
            else
            {
                BorderSide topBorders = BorderSide.All;
                row = new XRTableRow
                {
                    HeightF = 25F,
                    Padding = new PaddingInfo(5, 5, 5, 5),
                    KeepTogether = true
                };

                row.Cells.Add(new XRTableCell
                {
                    WidthF = 10F,
                    BackColor = ColorTranslator.FromHtml("#" + task.TypeColor),
                    Borders = topBorders,
                    KeepTogether = true
                });
                row.Cells.Add(new XRTableCell
                {
                    Text = task.Priority,
                    WidthF = 35F,
                    TextAlignment = TextAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(238, 235, 235),
                    Font = new Font("Arial", 8, FontStyle.Bold),
                    KeepTogether = true
                });

                cell = new XRTableCell()
                {
                    KeepTogether = true
                };
                var icons = new List<string>();
                if (task.Shadow)
                    icons.Add("<img src=\"/content/images/shared/shadow-task.png\" style=\"width: 16px; height: 12px; vertical-align: middle\">");
                //if (task.Priority.IndexOf("S", StringComparison.OrdinalIgnoreCase) > -1)
                //   icons.Add("<img src=\"" + Settings.GetSettings().Host.TrimEnd('/') + "/workspace/build/production/Fewzion/resources/images/shared/statutory-red.png\" style=\"width: 12px; height: 12px; vertical-align: middle\">");
                //if (task.Source == "Mainstay")
                //    icons.Add("<img src=\"" + Settings.GetSettings().Host.TrimEnd('/') + "/workspace/build/production/Fewzion/resources/images/shared/mainstay.png\" style=\"width: 12px; height: 12px; vertical-align: middle\">");
                

                // Tags
                icons.AddRange(TagUtil.GetTagsForPrinting(task.Tags, new List<Tag>()));

                var descHtml = "<div style=\"padding: 0; margin: 0; font-size: 10pt; font-family: Calibri; font-weight: bold\">" + description + "&nbsp;" + string.Join("&nbsp;", icons) + "</div>";
                var descRt = new XRRichText
                {
                    Padding = new PaddingInfo(2, 2, 2, 2),
                    TopF = 2,
                    LeftF = 2,
                    WidthF = tableWidth - 135F - 4,
                    HeightF = 21,
                    Html = descHtml,
                    Borders = BorderSide.None,
                    CanGrow = true,
                    AnchorVertical = VerticalAnchorStyles.None,
                    KeepTogether = true
                };
                cell.Controls.Add(descRt);
                cell.WidthF = tableWidth - 135f;
                cell.BackColor = Color.FromArgb(248, 248, 248);
                row.Cells.Add(cell);

                //row.Cells.Add(new XRTableCell { Text = description, WidthF = tableWidth - 45F, BackColor = Color.FromArgb(248, 248, 248) });
                row.Cells.Add(new XRTableCell
                {
                    Text = task.TargetAndUnitShortCode,
                    WidthF = 45F,
                    TextAlignment = TextAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(238, 235, 235),
                    Font = new Font("Arial", 8, FontStyle.Bold),
                    KeepTogether = true
                });
                row.Cells.Add(new XRTableCell
                {
                    Text = completionPercentagestr,
                    WidthF = 45F, TextAlignment = TextAlignment.MiddleCenter,
                    BackColor = compcolor,
                    Font = new Font("Arial", 8, FontStyle.Bold),
                    KeepTogether = true
                });
              
                table.Rows.Add(row);
            }

            table.EndInit();

            return table;
        }
    }
}
