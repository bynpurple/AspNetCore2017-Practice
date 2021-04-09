using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCore.Data.ViewModels;
using NetCore.Services.Interfaces;
using NetCore.Services.Svcs;
using Practice1.Models;

namespace Practice1.Controllers
{
    public class MemberShipController : Controller
    {
        // 밑의 방법은 의존성 주입(Dependency Injection) 방식이 아님
        // private IUser _user = new UserService();

        // 의존성 주입 - 생성자 > startup.cs
        private IUser _user;

        public MemberShipController(IUser user)
        {
            _user = user;
        }

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
        // Data => Services => Practice1
        // Data => Services
        // Data => Practice1
        public IActionResult Login(LoginInfo info)
        {
            String msg = String.Empty;

            if (ModelState.IsValid)
            {
                // 서비스 개념 >
                // Service 분리
                // 1. 서비스의 재사용성 , 2. 모듈화를 통한 효율적인 관리
                // ViewModel > Data 프로젝트로 이동
                if (_user.MatchTheUserInfo(info))
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