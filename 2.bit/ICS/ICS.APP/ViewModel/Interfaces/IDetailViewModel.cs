using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICS.App.ViewModel.Interfaces
{
    public interface IDetailViewModel<out TDetail> : IViewModel
    {
        TDetail? Model { get; }
        Task LoadAsync(Guid id);
        Task DeleteAsync();
        Task SaveAsync();
    }
}
