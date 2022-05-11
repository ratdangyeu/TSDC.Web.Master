using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;

namespace TSDC.Web.Master.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        [Breadcrumb("Vai trò")]
        public IActionResult Index()
        {
            ViewData["TitlePage"] = "Danh sách vai trò";
            return View();
        }
    }
}
