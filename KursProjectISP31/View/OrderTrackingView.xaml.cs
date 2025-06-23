using KursProjectISP31.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace KursProjectISP31.View
{
    public partial class OrderTrackingView : UserControl
    {
        public OrderTrackingView()
        {
            InitializeComponent();
        }

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is OrderTrackingViewModel viewModel && sender is ComboBox comboBox)
            {
                var newStatus = (comboBox.SelectedItem as ComboBoxItem)?.Tag?.ToString();
                if (newStatus != null)
                {
                    viewModel.UpdateSelectedOrderItemStatus(int.Parse(newStatus));
                }
            }
        }
    }
} 