using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UI.Models;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = Url.IsLocalUrl(returnUrl) ? returnUrl : string.Empty;
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register(string returnUrl)
        {
            ViewBag.ReturnUrl = Url.IsLocalUrl(returnUrl) ? returnUrl : string.Empty;
            return View();
        }

        public IActionResult ForgotPassword()
        {

            return View();

        }
        public IActionResult ChangePassword()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpGet("records/{userId}")]
        public IActionResult ViewUsers(Guid userId)
        {
            ViewData["userId"] = userId;
            return View();
        }

        //[HttpGet("profile/{userId}")]
        public IActionResult ViewProfile()
        {
            //ViewData["userId"] = userId;
            return View();
        }

        public IActionResult Splash()
        {
            return View();
        }

        [HttpGet("record/{id}")]
        public IActionResult ViewUser([FromRoute] Guid id)
        {
            ViewData["id"] = id;
            return View();
        }


        [HttpGet("edit/{id}")]
        public IActionResult EditUser([FromRoute] Guid id)
        {
            ViewData["id"] = id;
            return View();
        }


        [HttpGet("change-password/{id}")]
        public IActionResult ChangePassword([FromRoute] Guid id)
        {
            ViewData["id"] = id;
            return View();
        }
    }
}
