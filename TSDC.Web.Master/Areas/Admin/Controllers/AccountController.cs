using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using TSDC.ApiHelper;
using TSDC.ApiHelper.Enums;
using TSDC.ApiHelper.Models;
using TSDC.SharedMvc.Master.Models;

namespace TSDC.Web.Master.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        #region Fields
        private readonly ApiModel _apiModel;
        #endregion

        #region Ctor
        public AccountController(
            IOptions<ApiModel> apiModel)
        {
            _apiModel = apiModel.Value;
        }
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

        [HttpPost]
        public async Task<IActionResult> Login(AuthenticateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new BaseResult<string>
                {
                    Status = false,
                    Message = "Dữ liệu chưa validate"
                });
            }

            var res = await ApiHelper<UserModel>.ExecuteAsync("user/authenticate", null, request, Method.POST, _apiModel.Master);

            var model = res?.Data;

            if (model != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(ClaimTypes.Authentication, res.Token)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                {
                    IsPersistent = request.RememberMe
                });
            }

            return Ok(new BaseResult<string>
            {
                Status = true,
                Data = request.ReturnUrl
            });
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new BaseResult<string>
            {
                Status = true,
                Data = "/"
            });
        }

        public IActionResult Register()
        {
            var model = new UserModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new BaseResult<string>
                {
                    Status = false,
                    Message = "Dữ liệu chưa validate"
                });
            }

            var res = await ApiHelper<UserModel>.ExecuteAsync("user/create", null, model, Method.POST, _apiModel.Master);

            if (res?.Status == false)
            {
                return Ok(new BaseResult<string>
                {
                    Status = false,
                    Message = "Đăng ký thất bại"
                });
            }

            return Ok(new BaseResult<string>
            {
                Status = true,
                Message = "Đăng ký thành công",
                Data = "/"
            });
        }
        #endregion

        #region List

        #endregion

        #region Utilities

        #endregion
    }
}
