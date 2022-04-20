using System.Globalization;

namespace DF.RealEstate.Localization
{
    public interface IApplicationCulturesProvider
    {
        CultureInfo[] GetAllCultures();
    }
}