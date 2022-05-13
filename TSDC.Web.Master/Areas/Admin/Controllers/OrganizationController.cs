using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;

namespace TSDC.Web.Master.Areas.Admin.Controllers
{    
    [Authorize]
    [Area("Admin")]
    public class OrganizationController : Controller
    {
        #region Fields

        #endregion

        #region Ctor

        #endregion

        #region Methods
        [Breadcrumb("Đơn vị")]
        public IActionResult Index()
        {
            ViewData["TitlePage"] = "Danh sách đơn vị";
            return View();
        }
        #endregion

        #region List

        #endregion

        #region Utilities

        #endregion
    }
}
