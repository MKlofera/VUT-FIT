using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using ICS.BL.Models;

namespace ICS.App.Wrappers
{
    public class UserWrapper : ModelWrapper<UsersDetailModel>
    {
        public UserWrapper(UsersDetailModel model)
            : base(model)
        {
        }

        public string? Firstname
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string? Lastname
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string? Photography
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public ObservableCollection<CarsWrapper> Cars { get; set; } = new();

        //validace
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Firstname))
            {
                yield return new ValidationResult($"{nameof(Firstname)} is required", new[] { nameof(Firstname) });
            }

            if (string.IsNullOrWhiteSpace(Lastname))
            {
                yield return new ValidationResult($"{nameof(Lastname)} is required", new[] { nameof(Lastname) });
            }
        }

        public static implicit operator UserWrapper(UsersDetailModel detailModel)
            => new(detailModel);

        public static implicit operator UsersDetailModel(UserWrapper wrapper)
            => wrapper.Model;
    }
}