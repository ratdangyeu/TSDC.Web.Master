using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;

namespace TSDC.Web.Master.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]    
    public class HomeController : Controller
    {
        [DefaultBreadcrumb("Trang chủ")]
        public IActionResult Index()
        {
            ViewData["TitlePage"] = "Trang chủ";
            return View();
        }
    }
}
