using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Models
{
    public class CountriesList
    {
        public  List<SelectListItem> Countries()
        {
            List<SelectListItem> CountriesNames = new List<SelectListItem>();
            CultureInfo[] GetCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo getCulture in GetCultureInfo)
            {
                RegionInfo getregionInfo = new RegionInfo(getCulture.LCID);
                CountriesNames.Add(new SelectListItem { Text = getregionInfo.EnglishName , Value = getregionInfo.EnglishName});
            }
            return CountriesNames;
        }
    }
}