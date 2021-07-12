using oxinhem.Controllers;
using System.Web;
using System.Web.Mvc;

namespace oxinhem
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CultureFilter(defaultCulture: "se"));
            filters.Add(new HandleErrorAttribute());
        }
    }
}
