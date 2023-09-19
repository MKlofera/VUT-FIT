using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.Serialization;

namespace Pleskalizer.Web.App.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var enumField = enumValue.GetType().GetField(enumValue.ToString());
        
        var descriptionAttribute = enumField?.GetCustomAttribute<EnumMemberAttribute>();
        var description = descriptionAttribute?.Value;
        if (!string.IsNullOrWhiteSpace(description))
        {
            return description;
        }
        
        return enumValue.ToString();
    }
    
}