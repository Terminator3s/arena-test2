using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using JoistCalculator.Core.Models;
using JoistCalculator.Core.Services;
using JoistCalculator.Core.Validation;
using Microsoft.Win32;

namespace JoistCalculator.UI.ViewModels
{
    public class ResultDisplayItem
    {
        public string Title { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly CalculationEngine _calculationEngine;
        private readonly ExcelExportService _excelExportService;

        private JoistInput _input;
        private JoistResult? _result;
        private string _statusMessage = "آماده برای ورود اطلاعات و محاسبه.";

        public JoistInput Input
        {
            get => _input;
            set { _input = value; OnPropertyChanged(); }
        }

        public JoistResult? Result
        {
            get => _result;
            set { _result = value; OnPropertyChanged(); OnPropertyChanged(nameof(HasResults)); }
        }

        public bool HasResults => Result != null;

        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ResultDisplayItem> ResultItems { get; set; } = new ObservableCollection<ResultDisplayItem>();
        public ObservableCollection<ResultDisplayItem> StatusItems { get; set; } = new ObservableCollection<ResultDisplayItem>();

        public ICommand CalculateCommand { get; }
        public ICommand ExportExcelCommand { get; }

        public MainViewModel()
        {
            _calculationEngine = new CalculationEngine();
            _excelExportService = new ExcelExportService();
            _input = new JoistInput();

            CalculateCommand = new RelayCommand(ExecuteCalculate);
            ExportExcelCommand = new RelayCommand(ExecuteExportExcel, o => HasResults);
        }

        private void ExecuteCalculate(object? parameter)
        {
            try
            {
                var validationErrors = InputValidator.Validate(Input);
                if (validationErrors.Count > 0)
                {
                    MessageBox.Show(string.Join("\n", validationErrors), "خطای اعتبارسنجی اطلاعات ورودی", MessageBoxButton.OK, MessageBoxImage.Warning);
                    StatusMessage = "خطا در اعتبارسنجی ورودی‌ها.";
                    return;
                }

                Result = _calculationEngine.Calculate(Input);
                PopulateResultLists();
                StatusMessage = "محاسبات با موفقیت انجام شد.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در انجام محاسبات: {ex.Message}", "خطای سیستمی", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusMessage = "خطا در اجرای محاسبات.";
            }
        }

        private void PopulateResultLists()
        {
            ResultItems.Clear();
            StatusItems.Clear();

            if (Result == null) return;

            ResultItems.Add(new ResultDisplayItem { Title = "مدول الاستیسیته بتن (Ec)", Value = Result.Ec.ToString("N2"), Unit = "MPa" });
            ResultItems.Add(new ResultDisplayItem { Title = "نسبت مدول الاستیسیته (n)", Value = Result.ModularRatio.ToString("N2"), Unit = "-" });
            ResultItems.Add(new ResultDisplayItem { Title = "لنگر ترک‌خوردگی (Mcr)", Value = Result.Mcr.ToString("N2"), Unit = "kN.m" });
            ResultItems.Add(new ResultDisplayItem { Title = "لنگر خمشی بار مرده بتن (MD)", Value = Result.MD.ToString("N2"), Unit = "kN.m" });
            ResultItems.Add(new ResultDisplayItem { Title = "لنگر خمشی بار مرده کف‌سازی و تیغه‌بندی", Value = Result.MSD_P.ToString("N2"), Unit = "kN.m" });
            ResultItems.Add(new ResultDisplayItem { Title = "لنگر خمشی بار زنده (ML)", Value = Result.ML.ToString("N2"), Unit = "kN.m" });
            ResultItems.Add(new ResultDisplayItem { Title = "بار نهایی طراحی (qu)", Value = Result.Qu.ToString("N2"), Unit = "kPa" });
            ResultItems.Add(new ResultDisplayItem { Title = "خیز نهایی کل سازه", Value = Result.DeflectionTotal.ToString("N2"), Unit = "mm" });

            StatusItems.Add(new ResultDisplayItem { Title = "کنترل پانچ بتن رویه", Value = Result.PunchingShearStatus, Unit = "-" });
            StatusItems.Add(new ResultDisplayItem { Title = "کنترل مقاومت خمشی تیرچه", Value = Result.FlexureStatus, Unit = "-" });
            StatusItems.Add(new ResultDisplayItem { Title = "کنترل لرزش سقف", Value = Result.VibrationStatus, Unit = "-" });
        }

        private void ExecuteExportExcel(object? parameter)
        {
            if (Result == null) return;

            try
            {
                var dialog = new SaveFileDialog
                {
                    Filter = "Excel Files (*.xlsx)|*.xlsx",
                    FileName = "JoistCalculationReport.xlsx"
                };

                if (dialog.ShowDialog() == true)
                {
                    _excelExportService.Export(dialog.FileName, Input, Result);
                    MessageBox.Show("فایل اکسل با موفقیت ذخیره شد.", "خروجی موفق", MessageBoxButton.OK, MessageBoxImage.Information);
                    StatusMessage = "فایل اکسل خروجی گرفته شد.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در ذخیره فایل اکسل: {ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusMessage = "خطا در خروجی اکسل.";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
