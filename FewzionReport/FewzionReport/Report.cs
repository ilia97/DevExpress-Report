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
            this.Detail.BeforePrint += this.Detail_BeforePrint;
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
            
            for (var j = 0; j < 2; j++)
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

                    for (int k = 0; k < j * 50; k++)
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

                scheduleReport.ShiftPlans.Add(shiftPlanReportTemplate);
            }

            var sorted = new List<Schedule_Report>()
            {
                scheduleReport
            };

            this.DataSource = sorted;
        }

        void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var schedule = (Schedule_Report)GetCurrentRow();
            XRTable table = new XRTable() { SizeF = new System.Drawing.SizeF(xrPanel2.WidthF, 25F) };
            table.BeginInit();
            XRTableRow row = new XRTableRow();
            row.KeepTogether = false;
            
            foreach (var shift in schedule.ShiftPlans)
            {
                XRTableCell cell = new XRTableCell { WidthF = xrPanel2.WidthF / schedule.ShiftPlans.Count };
                row.Cells.Add(cell);

                CreateShiftPlanTables(cell, shift, ShiftPlan_Report.GetTrackedResources(shift).ToList());
            }
            table.Rows.Add(row);
            table.EndInit();

            xrPanel2.KeepTogether = false;
            xrPanel2.Controls.Clear();
            xrPanel2.Controls.Add(table);
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
