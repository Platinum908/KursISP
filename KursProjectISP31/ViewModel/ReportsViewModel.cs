using KursProjectISP31.Model;
using KursProjectISP31.Services;
using KursProjectISP31.Utills;
using System;
using System.Windows.Input;

namespace KursProjectISP31.ViewModel
{
    public class ReportsViewModel : ViewModelBase
    {
        private readonly RestaurantService _restaurantService;

        private DateTime _selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set { _selectedDate = value; OnPropertyChanged(); }
        }

        private DailyReport _report;
        public DailyReport Report
        {
            get => _report;
            set { _report = value; OnPropertyChanged(); }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }

        public ICommand GenerateReportCommand { get; }

        public ReportsViewModel()
        {
            _restaurantService = new RestaurantService();
            GenerateReportCommand = new RelayCommand(GenerateReport);
            Report = new DailyReport(); 
        }

        private void GenerateReport(object obj)
        {
            try
            {
                var reportData = _restaurantService.GetDailyReport(SelectedDate);
                if (reportData != null)
                {
                    Report = reportData;
                    Message = "Отчет успешно сформирован.";
                }
                else
                {
                    Message = "Нет данных для отчета на выбранную дату.";
                    Report = new DailyReport(); 
                }
            }
            catch (Exception ex)
            {
                Message = $"Ошибка при формировании отчета: {ex.Message}";
            }
        }
    }
} 