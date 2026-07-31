using System.IO;
using ClosedXML.Excel;
using JoistCalculator.Core.Models;

namespace JoistCalculator.Core.Services
{
    public class ExcelExportService
    {
        public void Export(string filePath, JoistInput input, JoistResult result)
        {
            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("نتایج محاسبات تیرچه");

            // Setup RTL view
            ws.SheetView.RightToLeft = true;

            // Title
            ws.Cell("A1").Value = "گزارش محاسبات و طراحی تیرچه بتنی (مقررات ملی ساختمان - مبحث ۹ / ACI 209)";
            ws.Range("A1:D1").Merge();
            ws.Cell("A1").Style.Font.Bold = true;
            ws.Cell("A1").Style.Font.FontSize = 14;
            ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell("A1").Style.Fill.BackgroundColor = XLColor.LightSkyBlue;

            int row = 3;

            // Section: Inputs
            ws.Cell(row, 1).Value = "پارامترهای ورودی کاربر";
            ws.Range(row, 1, row, 3).Merge();
            ws.Cell(row, 1).Style.Font.Bold = true;
            ws.Cell(row, 1).Style.Fill.BackgroundColor = XLColor.LightGray;
            row++;

            void AddInputRow(string label, object val, string unit)
            {
                ws.Cell(row, 1).Value = label;
                ws.Cell(row, 2).Value = val != null ? val.ToString() : "";
                ws.Cell(row, 3).Value = unit;
                row++;
            }

            AddInputRow("مقاومت فشاری بتن (fc)", input.Fc, "MPa");
            AddInputRow("تنش تسلیم میلگرد طولی (fy)", input.Fy, "MPa");
            AddInputRow("تنش تسلیم میلگرد عرضی (fyt)", input.Fyt, "MPa");
            AddInputRow("طول دهانه تیرچه (L)", input.SpanLength, "m");
            AddInputRow("ارتفاع کل تیرچه (h)", input.TotalHeight, "mm");
            AddInputRow("ضخامت دال بتنی رویه (t)", input.SlabThickness, "mm");
            AddInputRow("فاصله خالص تیرچه ها (S)", input.JoistSpacing, "mm");
            AddInputRow("عرض تک تیرچه (W)", input.RibWidth, "mm");
            AddInputRow("نوع تیرچه (D: دوبل، S: تک)", input.JoistType, "-");
            AddInputRow("بار زنده (Live)", input.LiveLoad, "kPa");
            AddInputRow("بار مرده تیغه بندی (Partition)", input.PartitionLoad, "kPa");
            AddInputRow("بار مرده کف سازی (Super Dead)", input.SuperDeadLoad, "kPa");
            AddInputRow("بار زنده متمرکز (Point load)", input.PointLoad, "kN");
            AddInputRow("ابعاد بار متمرکز", input.PointLoadSize, "mm");

            row++;
            // Section: Results
            ws.Cell(row, 1).Value = "نتایج و خروجی‌های محاسباتی";
            ws.Range(row, 1, row, 3).Merge();
            ws.Cell(row, 1).Style.Font.Bold = true;
            ws.Cell(row, 1).Style.Fill.BackgroundColor = XLColor.LightGray;
            row++;

            void AddResultRow(string label, double val, string unit)
            {
                ws.Cell(row, 1).Value = label;
                ws.Cell(row, 2).Value = val;
                ws.Cell(row, 3).Value = unit;
                row++;
            }

            AddResultRow("مدول الاستیسیته بتن (Ec)", result.Ec, "MPa");
            AddResultRow("نسبت مدول الاستیسیته (n)", result.ModularRatio, "-");
            AddResultRow("لنگر ترک‌خوردگی (Mcr)", result.Mcr, "kN.m");
            AddResultRow("لنگر خمشی ناشی از بار مرده بتن (MD)", result.MD, "kN.m");
            AddResultRow("لنگر خمشی ناشی از بار مرده کف‌سازی و تیغه‌بندی (MSD+P)", result.MSD_P, "kN.m");
            AddResultRow("لنگر خمشی ناشی از بار زنده (ML)", result.ML, "kN.m");
            AddResultRow("بار مرجع نهایی طراحی (qu)", result.Qu, "kPa");
            AddResultRow("خیز نهایی کل (Deflection)", result.DeflectionTotal, "mm");

            row++;
            ws.Cell(row, 1).Value = "کنترل‌های سازه‌ای و ضوابط";
            ws.Range(row, 1, row, 3).Merge();
            ws.Cell(row, 1).Style.Font.Bold = true;
            ws.Cell(row, 1).Style.Fill.BackgroundColor = XLColor.LightGray;
            row++;

            void AddStatusRow(string label, string status)
            {
                ws.Cell(row, 1).Value = label;
                ws.Cell(row, 2).Value = status;
                row++;
            }

            AddStatusRow("کنترل پانچ بتن رویه", result.PunchingShearStatus);
            AddStatusRow("کنترل مقاومت خمشی تیرچه", result.FlexureStatus);
            AddStatusRow("کنترل لرزش سقف", result.VibrationStatus);

            ws.Columns().AdjustToContents();
            workbook.SaveAs(filePath);
        }
    }
}
