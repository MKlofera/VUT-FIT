using ICS.App.ViewModel;

namespace ICS.App.Views
{
    /// <summary>
    /// Interaction logic for CarDetailWindow.xaml
    /// </summary>
    public partial class CarDetailWindow
    {
        public CarDetailWindow(CarDetailViewModel carDetailViewModel)
        {
            InitializeComponent();
            DataContext = carDetailViewModel;
        }
    }
}