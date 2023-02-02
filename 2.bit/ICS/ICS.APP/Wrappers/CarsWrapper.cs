using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using ICS.BL.Models;
using ICS.Common.Enums;

namespace ICS.App.Wrappers
{
    public class CarsWrapper : ModelWrapper<CarsDetailModel>
    {
        public CarsWrapper(CarsDetailModel model)
            : base(model)
        {
        }
        public string? Brand
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string? CarModel
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public VehicleType Type
        {
            get => GetValue<VehicleType>();
            set => SetValue(value);
        }
        public DateTime RegistrationDate
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }
        public string? Photography
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public static implicit operator CarsWrapper(CarsDetailModel detailModel)
            => new(detailModel);

        public static implicit operator CarsDetailModel(CarsWrapper wrapper)
            => wrapper.Model;
    }
}