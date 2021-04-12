//using NetCore.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using NetCore.Data.Classes;
using NetCore.Data.ViewModels;
using NetCore.Services.Data;
using NetCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCore.Services.Svcs
{
    public class UserService:IUser
    {
        private DBFirstDbContext _context;

        public UserService(DBFirstDbContext context)
        {
            _context = context;
        }

        // Data Model : Database 와 연동할 Model
        // View Model : View를 위한 Model
        #region private methods
        private IEnumerable<User> GetUserInfos()
        {
            return _context.Users.ToList();
            //return new List<User>()
            //{
            //    new User()
            //    {
            //        UserId = "user001",
            //        UserName = "김유저",
            //        UserEmail = "user001@gmail.com",
            //        Password = "123456"                    
            //    }
            //};
        }

        private User GetUserInfo(string userId, string password)
        {
            User user;

            // lambda
            //user = _context.Users.Where(u => u.UserId.Equals(userId) && u.Password.Equals(password)).FirstOrDefault();

            // FromSql
            // Table > where 절은 sql 구문 내에서 구현되는 것이 아닌 추가로 람다식을 통해 구현됨
            //user = _context.Users.FromSql("SELECT UserId, UserName, UserEmail, Password, IsMemberShipWithDrawn, JoinedUtcDate FROM dbo.[User]")
            //    .Where(u => u.UserId.Equals(userId) && u.Password.Equals(password))
            //    .FirstOrDefault();

            // View
            // View 또한 where 절은 sql 구문 내에서 구현되는 것이 아닌 추가로 람다식을 통해 구현됨
            //user = _context.Users.FromSql("SELECT UserId, UserName, UserEmail, Password, IsMemberShipWithDrawn, JoinedUtcDate FROM dbo.uvwUser")
            //    .Where(u => u.UserId.Equals(userId) && u.Password.Equals(password))
            //    .FirstOrDefault();

            // Function
            // function 내 매개변수를 넣을 수 있기 때문에 where 절 같은 경우 소스 내 람다식이 필요없음
            //user = _context.Users.FromSql($"SELECT UserId, UserName, UserEmail, Password, IsMemberShipWithDrawn, JoinedUtcDate FROM dbo.ufnUser({userId}, {password})")
            //    .FirstOrDefault();

            // Stored Procedure
            user = _context.Users.FromSql("dbo.uspCheckLoginByUserId @p0, @p1", new[] { userId, password })
                .FirstOrDefault();

            return user;
        }

        private bool checkTheUserInfo(string userId, string password)
        {
            //return GetUserInfos().Where(user => user.UserId.Equals(userId) && user.Password.Equals(password)).Any();
            return GetUserInfo(userId, password) != null ? true : false;
        }

        #endregion

        // Interface를 상속 받은 후에 명시적으로 Interface 구현
        bool IUser.MatchTheUserInfo(LoginInfo loginInfo)
        {
            return checkTheUserInfo(loginInfo.UserId, loginInfo.Password);
        }


    }
}
