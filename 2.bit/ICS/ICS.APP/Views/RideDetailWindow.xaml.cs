using ICS.App.ViewModel;

namespace ICS.App.Views
{
    /// <summary>
    /// Interaction logic for RideDetailWindow.xaml
    /// </summary>
    public partial class RideDetailWindow
    {
        public RideDetailWindow(RideDetailViewModel rideDetailViewModel)
        {
            InitializeComponent();
            DataContext = rideDetailViewModel;
        }
    }
}