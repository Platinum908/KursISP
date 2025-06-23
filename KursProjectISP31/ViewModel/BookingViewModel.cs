using KursProjectISP31.Model;
using KursProjectISP31.Services;
using KursProjectISP31.Utills;
using System;
using System.Windows.Input;

namespace KursProjectISP31.ViewModel
{
    public class BookingViewModel : ViewModelBase
    {
        private readonly RestaurantService _restaurantService;

        private Booking _newBooking;
        public Booking NewBooking
        {
            get { return _newBooking; }
            set { _newBooking = value; OnPropertyChanged(nameof(NewBooking)); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(nameof(Message)); }
        }

        public ICommand BookTableCommand { get; set; }

        public BookingViewModel()
        {
            _restaurantService = new RestaurantService();
            InitializeNewBooking();
            BookTableCommand = new RelayCommand(BookTable, CanBookTable);
        }

        private void InitializeNewBooking()
        {
            var nextId = _restaurantService.GetNextBookingId();
            NewBooking = new Booking { Id = nextId, BookingDateTime = DateTime.Now };
        }

        private bool CanBookTable(object obj)
        {
            return NewBooking.Id > 0 &&
                   NewBooking.TableId > 0 &&
                   !string.IsNullOrEmpty(NewBooking.CustomerName) &&
                   !string.IsNullOrEmpty(NewBooking.PhoneNumber);
        }

        private void BookTable(object obj)
        {
            try
            {
                var success = _restaurantService.BookTable(NewBooking);
                if (success)
                {
                    Message = "Столик успешно забронирован!";
                    InitializeNewBooking(); 
                }
                else
                {
                    Message = "Не удалось забронировать столик.";
                }
            }
            catch (Exception ex)
            {
                Message = $"Ошибка: {ex.Message}";
            }
        }
    }
} 