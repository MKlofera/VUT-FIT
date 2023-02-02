using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using ICS.BL.Models;
using ICS.Common.Enums;

namespace ICS.App.Wrappers
{
    public class CarpoolsWrapper : ModelWrapper<CarpoolsDetailModel>
    {
        public CarpoolsWrapper(CarpoolsDetailModel model)
            : base(model)
        {
        }

        public Guid? Ride
        {
            get => GetValue<Guid>();
            set => SetValue(value);
        }
        public Guid? CoDriver
        {
            get => GetValue<Guid>();
            set => SetValue(value);
        }

        public ObservableCollection<CarsWrapper> Rides { get; set; } = new();

        public ObservableCollection<CarsWrapper> CoDrivers { get; set; } = new();


        public static implicit operator CarpoolsWrapper(CarpoolsDetailModel detailModel)
            => new(detailModel);

        public static implicit operator CarpoolsDetailModel(CarpoolsWrapper wrapper)
            => wrapper.Model;
    }
}