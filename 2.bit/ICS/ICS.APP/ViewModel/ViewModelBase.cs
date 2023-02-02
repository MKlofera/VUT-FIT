using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using ICS.App.ViewModel.Interfaces;

namespace ICS.App.ViewModel
{
    public abstract class ViewModelBase : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected ViewModelBase()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                LoadInDesignMode();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public virtual void LoadInDesignMode() { }
    }
}