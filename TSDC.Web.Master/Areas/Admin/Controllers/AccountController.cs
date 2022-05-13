using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TSDC.ApiHelper;
using TSDC.SharedMvc.Master.Models;

namespace TSDC.Web.Master.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        #region Fields

        #endregion

        #region Ctor

        #endregion

        #region Methods
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string returnUrl = "")
        {
            var model = new AuthenticateRequest();
            model.ReturnUrl = returnUrl;
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(AuthenticateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new BaseResult<object>
                {
                    Status = false,
                    Message = "Dữ liệu chưa validate"
                });
            }

            return Ok(new BaseResult<object>
            {
                Status = true
            });
        }
        #endregion

        #region List

        #endregion

        #region Utilities

        #endregion
    }
}
