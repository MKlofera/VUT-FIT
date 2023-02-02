using System.Resources;

namespace FoodDelivery.Common.Attributes;

public abstract class LocalizableDescriptionAttribute : Attribute
{
    private readonly string resourceName;

    public LocalizableDescriptionAttribute(string resourceName)
    {
        this.resourceName = resourceName;
    }

    protected abstract ResourceManager GetResource();

    public string? GetLocalizedDescription()
    {
        return GetResource()?.GetString(resourceName);
    }
}
