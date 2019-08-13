using System.Web;
using System.Web.Mvc;

namespace Board_Game_Stranica_N_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
