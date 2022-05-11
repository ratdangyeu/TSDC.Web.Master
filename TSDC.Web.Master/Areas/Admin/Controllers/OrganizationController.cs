using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;

namespace TSDC.Web.Master.Areas.Admin.Controllers
{    
    [Area("Admin")]
    public class OrganizationController : Controller
    {
        [Breadcrumb("Đơn vị")]
        public IActionResult Index()
        {
            ViewData["TitlePage"] = "Danh sách đơn vị";
            return View();
        }
    }
}
