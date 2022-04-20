using Microsoft.AspNetCore.Mvc;
using DF.RealEstate.Web.Controllers;

namespace DF.RealEstate.Web.Public.Controllers
{
    public class AboutController : RealEstateControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}