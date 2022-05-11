using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;

namespace TSDC.Web.Master.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        [Breadcrumb("Người dùng")]
        public IActionResult Index()
        {
            ViewData["TitlePage"] = "Danh sách người dùng";
            return View();
        }
    }
}
