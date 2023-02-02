using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using ICS.BL.Models;

namespace ICS.App.Wrappers
{
    public class RidesWrapper : ModelWrapper<RidesDetailModel>
    {
        public RidesWrapper(RidesDetailModel model)
            : base(model)
        {
        }

        public string? StartDestination
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public DateTime? StartTime
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }
        public string? EndDestination
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public DateTime? EndTime
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }
        public uint? Duration
        {
            get => GetValue<uint>();
            set => SetValue(value);
        }
        public uint? AvailableSeats
        {
            get => GetValue<uint>();
            set => SetValue(value);
        }

        public static implicit operator RidesWrapper(RidesDetailModel detailModel)
            => new(detailModel);

        public static implicit operator RidesDetailModel(RidesWrapper wrapper)
            => wrapper.Model;
    }
}