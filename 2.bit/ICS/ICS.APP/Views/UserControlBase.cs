using System.Threading.Tasks;
using ICS.App.ViewModel;
using System.Windows;
using System.Windows.Controls;
using ICS.App.ViewModel.Interfaces;

namespace ICS.App.Views
{
    public abstract class UserControlBase : UserControl
    {
        protected UserControlBase()
        {
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is IListViewModel viewModel)
            {
                await viewModel.LoadAsync();
            }
        }
    }
}