using FoodDelivery.Common.Attributes;
using FoodDelivery.Common.Resources;
using System.Resources;

namespace FoodDelivery.Common.Enums;

public enum Allergen
{
    [AllergenDescription(nameof(AllergenResources.UndefinedDescription))]
    Undefined = 0,

    [AllergenDescription(nameof(AllergenResources.GlutenDescription))]
    Gluten = 1,

    [AllergenDescription(nameof(AllergenResources.CrustaceansDescription))]
    Crustaceans = 2,

    [AllergenDescription(nameof(AllergenResources.EggsDescription))]
    Eggs = 3,

    [AllergenDescription(nameof(AllergenResources.FishDescription))]
    Fish = 4,

    [AllergenDescription(nameof(AllergenResources.PeanutsDescription))]
    Peanuts = 5,

    [AllergenDescription(nameof(AllergenResources.SoybeansDescription))]
    Soybeans = 6,

    [AllergenDescription(nameof(AllergenResources.MilkDescription))]
    Milk = 7,

    [AllergenDescription(nameof(AllergenResources.NutsDescription))]
    Nuts = 8,

    [AllergenDescription(nameof(AllergenResources.CeleryDescription))]
    Celery = 9,

    [AllergenDescription(nameof(AllergenResources.MustardDescription))]
    Mustard = 10,

    [AllergenDescription(nameof(AllergenResources.SesameDescription))]
    Sesame = 11,

    [AllergenDescription(nameof(AllergenResources.SulphurDescription))]
    Sulphur = 12,

    [AllergenDescription(nameof(AllergenResources.LupinDescription))]
    Lupin = 13,

    [AllergenDescription(nameof(AllergenResources.MolluscsDescription))]
    Molluscs = 14
}

public class AllergenDescription : LocalizableDescriptionAttribute
{
    public AllergenDescription(string resourceName) : base(resourceName)
    {
    }

    protected override ResourceManager GetResource()
    {
        return AllergenResources.ResourceManager;
    }
}
