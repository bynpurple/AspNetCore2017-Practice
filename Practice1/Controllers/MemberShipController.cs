using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Practice1.Models;

namespace Practice1.Controllers
{
    public class MemberShipController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginInfo info)
        {
            String sId = "userId";
            String sPassword = "userPass";
            String msg = String.Empty;

            if (ModelState.IsValid)
            {
                if (info.UserId.Equals(sId) && info.Password.Equals(sPassword))
                {
                    TempData["Message"] = "로그인이 성공적으로 이루어졌습니다.";
                    return RedirectToAction("Index", "Membership");
                }
                else
                {
                    msg = "해당 아이디와 비밀번호가 일치하지 않습니다.";
                }
            }
            else
            {
                msg = "로그인 정보가 올바르지 않습니다.";
            }


            ModelState.AddModelError(string.Empty, msg);
            return View();
        }
    }
}